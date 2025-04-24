using System;
using System.Collections.Generic;
using UnityEngine;
using UGS;

public class GunDataManager : MonoBehaviour
{
    public static List<GunData> GunDatas = new List<GunData>();
    void Awake()
    {
        UnityGoogleSheet.LoadAllData();

        foreach (var row in GunDataTable.Data.DataList)
        {
            GunData data = new GunData()
            {
                gunID = row.gunID,
                gunName = row.gunName,
                gunCategory = ParseCategory(row.gunCategory),
                damage = row.damage,
                ammoPerShot = row.ammoPerShot,
                bulletsPerShot = row.bulletsPerShot,
                bulletSpread = row.bulletSpread,
                reloadTime = row.reloadTime,
                fireRate = row.fireRate,
                bulletSpeed = row.bulletSpeed
            };

            GunDatas.Add(data);
        }
        GunDatabase.Instance.Initialize(GunDatas);
    }

    private GunCategory ParseCategory(string value)
    {
        if (Enum.TryParse(value, out GunCategory category))
        {
            return category;
        }

        Debug.LogWarning($"Invalid GunCategory: {value}, defaulting to SecondaryWeapon");
        return GunCategory.SecondaryWeapon; // �⺻�� ����
    }

}