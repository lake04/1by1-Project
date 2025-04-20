using System;
using System.Collections.Generic;
using UnityEngine;
using UGS;

public class GunDataManager : MonoBehaviour
{
    public static List<GunDatas> GunDatas = new List<GunDatas>();
    void Awake()
    {
        UnityGoogleSheet.LoadAllData();

        foreach (var row in GunData.Data.DataList)
        {
            GunDatas data = new GunDatas()
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
    }

    private GunCategory ParseCategory(string value)
    {
        if (Enum.TryParse(value, out GunCategory category))
        {
            return category;
        }

        Debug.LogWarning($"Invalid GunCategory: {value}, defaulting to SecondaryWeapon");
        return GunCategory.SecondaryWeapon; // 기본값 설정
    }

    public static List<GunDatas> GetRandomGuns(int count)
    {
        List<GunDatas> source = new List<GunDatas>(GunDatas);
        List<GunDatas> result = new List<GunDatas>();

        for (int i = 0; i < count; i++)
        {
            if (source.Count == 0) break;

            int rand = UnityEngine.Random.Range(0, source.Count);
            result.Add(source[rand]);
            source.RemoveAt(rand);
        }

        return result;
    }
}