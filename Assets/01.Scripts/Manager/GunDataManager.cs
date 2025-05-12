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
                damage = row.damage,
                ammoPerShot = row.ammoPerShot,
                bulletsPerShot = row.bulletsPerShot,
                bulletSpread = row.bulletSpread,
                reloadTime = row.reloadTime,
                fireRate = row.fireRate,
                bulletSpeed = row.bulletSpeed,
                skillId = row.skillId,
                canEquipParts = SetBool(row.canEquipParts),
                allowedPartTypes = ParsePartTypes(row.allowedPartTypes)
                
        }
        ;
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

    private List<GunPartType> ParsePartTypes(string csv)
    {
        List<GunPartType> result = new();
        if (string.IsNullOrEmpty(csv)) return result;

        string[] tokens = csv.Split(',');
        foreach (var token in tokens)
        {
            if (Enum.TryParse(token.Trim(), out GunPartType type))
            {
                result.Add(type);
            }
            else
            {
                Debug.LogWarning($"Invalid part type: {token}");
            }
        }
        return result;
    }
}