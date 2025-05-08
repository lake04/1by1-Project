using System;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager Instance;

    [Header("총기 생성 관련")]
    [SerializeField] private GameObject gunParentObject; // 플레이어 손 위치
    [SerializeField] private GameObject gun; // 총기 프리팹

    [Header("탄약")]
    public int maxBullet = 100;
    public int curBullet;

    [SerializeField] public Dictionary<GunCategory, Sprite> slots = new();

    [Header("무기 슬롯")]
    [SerializeField] private Dictionary<GunCategory, GunData> equippedWeapons = new();
    [SerializeField] private GunCategory currentWeaponSlot = GunCategory.MainWeapon;
    [SerializeField] private GameObject currentGunSprite;

  

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        curBullet = maxBullet;

        equippedWeapons[GunCategory.MainWeapon] = null;
        equippedWeapons[GunCategory.SecondaryWeapon] = null;
        equippedWeapons[GunCategory.MeleeWeapon] = null;
    }

    private void Start()
    {
        CreateGun(GunDatabase.Instance.allGunData[0], transform.position);
    }

    private void Update()
    {
        SelectGunInput();
   
    }

    public void EquipWeapon(GunData _newGun)
    {
        if (_newGun == null)
        {
            Debug.LogWarning("장착할 무기가 없습니다.");
            return;
        }

        GunCategory category = _newGun.gunCategory;
        equippedWeapons[category] = _newGun;
        slots[category] = GunDatabase.Instance.sprites[_newGun.gunID];

        Debug.Log($"장착 완료: {category} 슬롯에 {_newGun.gunName}");

        if (currentWeaponSlot == category)
        {
            UpdateCurrentGun();
        }
    }

    private void SelectGunInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(GunCategory.MainWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(GunCategory.SecondaryWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(GunCategory.MeleeWeapon);
        }
    }

    private void ChangeWeapon(GunCategory _category)
    {
        if (!equippedWeapons.ContainsKey(_category) || equippedWeapons[_category] == null)
        {
            Debug.LogWarning($"{_category} 슬롯에 무기가 없습니다.");
            return;
        }

        currentWeaponSlot = _category;
        UpdateCurrentGun();

        Debug.Log($"{_category} 무기로 변경했습니다!");
    }

    private void UpdateCurrentGun()
    {
        GunData currentGun = equippedWeapons[currentWeaponSlot];

        if (currentGun == null)
        {
            Debug.LogWarning("현재 선택된 슬롯에 무기가 없습니다.");
            return;
        }

        // 기존 무기 제거
        if (currentGunSprite != null)
            Destroy(currentGunSprite);

        // 무기 생성 → 플레이어 손에 붙이기
        GameObject gunObj = Instantiate(gun, gunParentObject.transform);
        gunObj.name = currentGun.gunName;
        gunObj.transform.localPosition = Vector3.zero; // 위치 초기화

        // 스프라이트 설정
        SpriteRenderer spriteRenderer = gunObj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && currentGun.gunID < GunDatabase.Instance.sprites.Count)
        {
            spriteRenderer.sprite = GunDatabase.Instance.sprites[currentGun.gunID];
        }

        // Gun 컴포넌트 초기화
        // 장착 무기 생성 시
        Gun gunComponent = gunObj.GetComponent<Gun>();
        if (gunComponent == null)
            gunComponent = gunObj.AddComponent<Gun>();

        gunComponent.Init(currentGun);
        gunComponent.isEquipped = true; 


        currentGunSprite = gunObj;

        Debug.Log($"현재 장착 무기: {currentGun.gunName}");    
    }

    public GunData GetCurrentGun()
    {
        return equippedWeapons.ContainsKey(currentWeaponSlot) ? equippedWeapons[currentWeaponSlot] : null;
    }

    public void CreateGun(GunData _gunData, Vector3 spawnPosition)
    {
        GameObject _gun = Instantiate(gun, spawnPosition, Quaternion.identity);
        _gun.name = _gunData.gunName;
        _gun.AddComponent<BoxCollider2D>();
        _gun.GetComponent<BoxCollider2D>().enabled = true;

        SpriteRenderer spriteRenderer = _gun.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && _gunData.gunID < GunDatabase.Instance.sprites.Count)
        {
            spriteRenderer.sprite = GunDatabase.Instance.sprites[_gunData.gunID];
        }

        Gun gunComponent = _gun.GetComponent<Gun>();
        if (gunComponent == null)
            gunComponent = _gun.AddComponent<Gun>();

        gunComponent.Init(_gunData);

        GunPickup pickup = _gun.GetComponent<GunPickup>();
        if (pickup == null)
            pickup = _gun.AddComponent<GunPickup>();

        pickup.data = _gunData;
    }
}
