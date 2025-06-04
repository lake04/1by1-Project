using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItempType
{
    part, element

}
public interface IPartModifier
{
    float GetDamageModifier();
    float GetFireRateModifier();
}


[System.Serializable]
public class Item : IPartModifier
{
    public ItempType type;
    public Element element;
    public string id;
    public string name;
    public float damageModifier;
    public float fireRateModifier;

    public float GetDamageModifier() => damageModifier;
    public float GetFireRateModifier() => fireRateModifier;
}


