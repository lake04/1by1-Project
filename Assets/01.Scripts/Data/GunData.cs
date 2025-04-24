using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GunCategory
{
    MainWeapon,
    SecondaryWeapon,
    MeleeWeapon
}

[System.Serializable]
public class GunRow
{
    public int gunID;
    public string gunName;
    public string gunCategory;
    public int damage;
    public int ammoPerShot;
    public int bulletsPerShot;
    public float bulletSpread;
    public float reloadTime;
    public float fireRate;
    public float bulletSpeed;
}

[System.Serializable]
public class GunData
{
    public int gunID;
    public string gunName;
    public GunCategory gunCategory;
    public int damage;
    public int ammoPerShot;
    public int bulletsPerShot;
    public float bulletSpread;
    public float reloadTime;
    public float fireRate;
    public float bulletSpeed;
}