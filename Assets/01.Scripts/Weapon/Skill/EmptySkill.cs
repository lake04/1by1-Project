using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySkill : IGunSkill
{
    public void UseSkill() { }
    public bool CanUse() => false;
}

