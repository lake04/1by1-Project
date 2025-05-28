using System.Collections.Generic;

public enum GunCategory
{
    MainWeapon,
    SecondaryWeapon,
    MeleeWeapon
}

public enum GunKind
{
    pistol,
    shotgun,
    sniperRifle,
}

public class GunPartData
{
    public string id;
    public string name;
    public float damageModifier;
    public float fireRateModifier;
}

[System.Serializable]
public class GunRow
{
    public int gunID;
    public string gunName;
    public string gunCategory;
    public string gunKind;
    public int damage;
    public int ammoPerShot;
    public int bulletsPerShot;
    public float bulletSpread;
    public float reloadTime;
    public float fireRate;
    public float bulletSpeed;
    public int skillId;
    public bool canEquipParts;
    public int parts;
    public int maxAmmo;
    public int curAmmo;
}

[System.Serializable]
public class GunData
{
    public int gunID;
    public string gunName;
    public GunCategory gunCategory;
    public GunKind gunKind;
    public int damage;
    public int ammoPerShot;
    public int bulletsPerShot;
    public float bulletSpread;
    public float reloadTime;
    public float fireRate;
    public float bulletSpeed;
    public int skillId;
    public bool canEquipParts = true;
    public int parts;
    public int maxAmmo;
    public int curAmmo;
}