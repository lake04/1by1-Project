using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float maxHp;
    public float curHp;
    public float damage;
    public float moveSpeed;
    public bool isMove = true;
    public bool moveing = false;
    public bool isAttack = true;
    public float attackCoolTime;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float _damage)
    {
        if(curHp<=0)
        {
            Dead();
        }
        else
        {
            Debug.Log("공격!!");
            curHp -= _damage;
            UiManager.instance.UpdateUI();
        }
    }
    public virtual void Move()
    {
            
    }
    public virtual void Dead()
    {

    }
}
