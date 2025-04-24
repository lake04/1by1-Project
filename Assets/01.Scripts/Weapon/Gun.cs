using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("�� ���� ������")]
    public float bulletSpeed = 10f;
    public float attackCoolTime = 0.3f;
    public int maxBullet = 6;
    public int currentBullet = 6;

    [Header("���� ����")]
    public Vector2 meleeSize = new Vector2(1.5f, 1.0f);
    public float meleeOffset = 1.0f;

    private bool isAttack = true;
    private Vector2 lastMoveDirection = Vector2.right;
    private Transform aimingPoint;

    void Start()
    {
        if (aimingPoint == null)
        {
            aimingPoint = Player.Instance.aimingPoint;

        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && isAttack)
        {
            if (currentBullet > 0)
                StartCoroutine(Shooting());
            else
                StartCoroutine(MeleeAttack());
        }
    }

    private void Init()
    {

    }

    private IEnumerator Shooting()
    {
        isAttack = false;

        if (currentBullet <= 0)
            yield break;

        GameObject bullet = GameManager.Instance.pool.Get(0);
        currentBullet--;

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            Vector2 dir = (aimingPoint.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
            lastMoveDirection = dir;
        }

        yield return new WaitForSeconds(attackCoolTime);
        isAttack = true;
    }

    private IEnumerator MeleeAttack()
    {
        isAttack = false;
        Vector2 attackDir = (aimingPoint.position - transform.position).normalized;
        Vector2 attackCenter = (Vector2)transform.position + attackDir * meleeOffset;
        float attackAngle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;

        Collider2D[] hits = Physics2D.OverlapBoxAll(attackCenter, meleeSize, attackAngle);

        foreach (Collider2D col in hits)
        {
            if (col.CompareTag("Enemy"))
            {
                Debug.Log("�� ����!");
            }
        }

        lastMoveDirection = attackDir;
        yield return new WaitForSeconds(attackCoolTime);
        isAttack = true;
    }

    public void EquipWeapon(GunData gun)
    {
        this.bulletSpeed = gun.bulletSpeed;
        this.attackCoolTime = 1f / gun.fireRate;
        this.maxBullet = gun.ammoPerShot;
        this.currentBullet = maxBullet;
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
}
