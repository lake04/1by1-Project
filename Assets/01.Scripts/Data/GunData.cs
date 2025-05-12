using System.Collections.Generic;

public enum GunCategory
{
    MainWeapon,
    SecondaryWeapon,
    MeleeWeapon
}

public enum GunPartType { Barrel, Handle, Magazine, Element }

public class GunPartData
{
    public string id;
    public string name;
    public GunPartType type;
    public float damageModifier;
    public float fireRateModifier;
    public string allowedGunType;
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
    public int skillId;
    public bool canEquipParts;
    public string allowedPartTypes; // CSV 문자열로 저장 (예: "Barrel,Handle")
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
    public int skillId;
    public bool canEquipParts = true;
    public List<GunPartType> allowedPartTypes = new();
}