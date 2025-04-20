using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunCategory
{
    SecondaryWeapon,
    MainWeapon,
    MelleWeapon
}

[System.Serializable]
public class GunDatas
{
    public int gunID;
    public string gunName;
    public GunCategory gunCategory;
    public int damage;
    public int ammoPerShot;
    public int bulletsPerShot;
    public int bulletSpread;
    public int reloadTime;
    public int fireRate;
    public int bulletSpeed;
}
