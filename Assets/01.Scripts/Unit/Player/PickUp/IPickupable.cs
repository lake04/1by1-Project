using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    void OnPickup();
}

public class GunPickup : MonoBehaviour, IPickupable
{
    public GunData data;

    public void OnPickup()
    {
        Debug.Log("먹어짐");
        GunManager.Instance.EquipWeapon(data);
        Destroy(gameObject);
    }
}

//public class PartPickup : MonoBehaviour, IPickupable
//{
//    public PartData partData;

//    public void OnPickup()
//    {
//        Inventory.Instance.AddPart(partData);
//        Destroy(gameObject);
//    }
//}

