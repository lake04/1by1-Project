using System.Collections.Generic;
using System;
using UGS;
using UnityEngine;

public class GunDataManager : MonoBehaviour
{
    public static List<GunData> GunDatas = new List<GunData>();

    void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        UnityGoogleSheet.LoadAllData();

        foreach (var row in GunDataTable.Data.DataList)
        {
            GunData data = new GunData
            {
                gunID = row.gunID,
                gunName = row.gunName,
                gunCategory = ParseCategory(row.gunCategory),
                gunKind = GetGunKind(row.gunKind),
                damage = row.damage,
                ammoPerShot = row.ammoPerShot,
                bulletsPerShot = row.bulletsPerShot,
                bulletSpread = row.bulletSpread,
                reloadTime = row.reloadTime,
                fireRate = row.fireRate,
                bulletSpeed = row.bulletSpeed,
                skillId = row.skillId,
                canEquipParts = SetBool(row.canEquipParts),
                parts = row.parts,
                maxAmmo = row.maxAmmo,
            };
            GunDatas.Add(data);
        }

        GunDatabase.Instance.Initialize(GunDatas);
    }

    private bool SetBool(int _num)
    {
        if (_num == 1)
        {
            return true;
        }
        else return  false;
    }

    private GunCategory ParseCategory(string value)
    {
        return Enum.TryParse(value, out GunCategory category) ? category : GunCategory.SecondaryWeapon;
    }
    //public GunKind gunKind

    private GunKind GetGunKind(int n)
    {
        switch(n)
        {
            case 0: return GunKind.pistol;

            case 1: return GunKind.shotgun;

            case 2: return GunKind.sniperRifle;

            default: return GunKind.pistol;
        }
    }

    
}