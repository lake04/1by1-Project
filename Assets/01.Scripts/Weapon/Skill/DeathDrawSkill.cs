using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDrawSkill : MonoBehaviour,IGunSkill
{
    private float cooldown = 10f;
    private float lastUsedTime = -999f;

    public void UseSkill()
    {
        if (!CanUse()) return;
    }

    public bool CanUse()
    {
        return Time.time >= lastUsedTime + cooldown;
    }
}
