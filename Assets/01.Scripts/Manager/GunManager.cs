using System;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public static GunManager Instance { get; private set; }

    public Dictionary<GunCategory, GunData> equippedWeapons = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        equippedWeapons[GunCategory.MainWeapon] = null;
        equippedWeapons[GunCategory.SecondaryWeapon] = null;
        equippedWeapons[GunCategory.MeleeWeapon] = null;
    }

    public void EquipWeapon(GunData newGun)
    {
        GunCategory category = newGun.gunCategory;

        equippedWeapons[category] = newGun;

        Debug.Log($"장착된 {category} 무기: {newGun.gunName}");
    }

    private void SelsetGun()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            GunData gun = equippedWeapons[GunCategory.MainWeapon];
        }
    }
   
}
