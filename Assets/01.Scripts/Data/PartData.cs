using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/AttachmentData")]
public class PartData : ScriptableObject
{
    public string attachmentName;
    public Sprite icon;
    public float effectValue; 
}
