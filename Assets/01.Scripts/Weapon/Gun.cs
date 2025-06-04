using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public enum Element
{
    None,
    Fire,
    Water,
    Ground,
    Ice,
    Electricity
}

public class Gun : MonoBehaviour
{
    public int gunID;
    public string gunName;
    public List<Item> equippedParts = new List<Item>();
    public Item[] partSlots = new Item[3];

    public GunCategory gunCategory;
    public GunKind gunKind;
    public Element element = Element.None;

    [Header("스탯")]
    public int damage;
    public int ammoPerShot;
    public int bulletsPerShot;
    public float bulletSpread;
    public float reloadTime;
    public float fireRate;
    public float bulletSpeed;
    public int equipParts;

    [Header("탄창 관련")]
    public int maxAmmo;
    public int curAmmo;
    [SerializeField] private bool isReloading = false;
    public List<Item> parts = new();

    public bool isEquipped = false;
    public Vector2 meleeSize = new Vector2(1.5f, 1.0f);
    public float meleeOffset = 1.0f;

    private Vector2 lastMoveDirection = Vector2.right;
    private Transform aimingPoint;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private Player player;
    [SerializeField] private Animation animation;
    [SerializeField] private AnimatorOverrideController clip;

    public IGunSkill skillModule;

    private float fireDelay;
    private float baseDamage;
    private float baseFireRate;

    private static readonly Dictionary<int, Type> skills = new()
    {
        { 0, typeof(DeathDrawSkill) },
    };

    void Start()
    {
        player = GetComponentInParent<Player>();
        animation = GetComponent<Animation>();
        if (aimingPoint == null)
            aimingPoint = Player.Instance.aimingPoint;
    }

    private void OnEnable() {}

    void Update()
    {
        if (!isEquipped) return;
        InputVoid();
    }

    public void Init(GunData data)
    {
        gunID = data.gunID;
        gunName = data.gunName;
        gunCategory = data.gunCategory;
        gunKind = data.gunKind;

        baseDamage = data.damage;
        baseFireRate = data.fireRate;

        ammoPerShot = data.ammoPerShot;
        bulletsPerShot = data.bulletsPerShot;
        bulletSpread = data.bulletSpread;
        reloadTime = data.reloadTime;
        bulletSpeed = data.bulletSpeed;
        maxAmmo = data.maxAmmo;
        curAmmo = maxAmmo;

        ApplyPartsStats();
        SkillSettings(gunID);
    }

    private void ApplyPartsStats()
    {
        float totalDamageModifier = 1f;
        float totalFireRateModifier = 1f;

        foreach (var part in parts)
        {
            totalDamageModifier += part.GetDamageModifier();
            totalFireRateModifier += part.GetFireRateModifier();
        }

        damage = Mathf.RoundToInt(baseDamage * totalDamageModifier);
        fireRate = baseFireRate * totalFireRateModifier;
        fireDelay = 1f / fireRate;
    }

    private void SkillSettings(int id)
    {
        if (skills.TryGetValue(id, out var skillType))
            skillModule = (IGunSkill)gameObject.AddComponent(skillType);
        else
            skillModule = new EmptySkill();
    }

    private IEnumerator Fire()
    {
        Debug.Log("총 발사");
        Player.Instance.isAttack = false;
        Player.Instance.hand.GetComponent<Animator>().SetBool("isFire", true);

        for (int i = 0; i < bulletsPerShot; i++)
        {
            Sprite bulletSprite = BulletManager.Instance.GetBulletSprite(gunKind, element);
            GameObject bullet = GameManager.Instance.pool.Get(0);
            if (bullet == null) continue;

            bullet.GetComponent<Bullet>().damage = damage;
            bullet.GetComponent<Bullet>().bulletElement = element;
            bullet.GetComponent<Bullet>().SetSprite(bulletSprite);

            Vector2 dir = (aimingPoint.position - transform.position).normalized;
            float angleOffset = UnityEngine.Random.Range(-bulletSpread, bulletSpread);
            dir = Quaternion.Euler(0, 0, angleOffset) * dir;

            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;

            lastMoveDirection = dir;
        }

        curAmmo -= ammoPerShot;
        yield return new WaitForSeconds(fireDelay);
        Player.Instance.isAttack = true;
        Player.Instance.hand.GetComponent<Animator>().SetBool("isFire", false);
    }

    private IEnumerator MeleeAttack()
    {
        Player.Instance.isAttack = false;
        Vector2 attackDir = (aimingPoint.position - transform.position).normalized;
        Vector2 attackCenter = (Vector2)transform.position + attackDir * meleeOffset;
        float attackAngle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;

        Collider2D[] hits = Physics2D.OverlapBoxAll(attackCenter, meleeSize, attackAngle);

        foreach (Collider2D collider in hits)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        lastMoveDirection = attackDir;
        yield return new WaitForSeconds(fireDelay);
        Player.Instance.isAttack = true;
    }

    private void InputVoid()
    {
        if (Input.GetMouseButton(0) && Player.Instance.isAttack && !isReloading)
        {
            if (curAmmo >= ammoPerShot)
                StartCoroutine(Fire());
            else
                StartCoroutine(MeleeAttack());
        }

        if (Input.GetMouseButtonDown(1) && skillModule != null && skillModule.CanUse())
        {
            skillModule.UseSkill();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        if (curAmmo == maxAmmo) yield break;

        isReloading = true;
        Debug.Log("재장전 시작");
        GunManager.Instance.curBullet -= (maxAmmo - curAmmo);

        yield return new WaitForSeconds(reloadTime / 10);
        curAmmo = maxAmmo;
        UiManager.instance.UpdateUI();
        isReloading = false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 dir = lastMoveDirection;
        Vector2 center = (Vector2)transform.position + dir * meleeOffset;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.Euler(0, 0, angle), Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, meleeSize);
    }

    public void EquipPart(int slotIndex, Item item)
    {
        partSlots[slotIndex] = item;
        ApplyPartsStats();
    }

    public void UnequipPart(int slotIndex)
    {
        partSlots[slotIndex] = null;
        ApplyPartsStats();
        //if (parts.Remove(part))
        //{
        //    ApplyPartsStats();
        //}
    }
}
