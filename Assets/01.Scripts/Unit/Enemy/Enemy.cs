using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Enemy : Unit
{
    [SerializeField]
    [Range(0f, 10f)]
    private float searchRange;

    [SerializeField]
    [Range(0f, 10f)]
    private float attacthRange;

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, searchRange);
    }
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        Move();
        Attack();
    }

    public  void Init()
    {
        this.attackCoolTime = 4f;
        this.moveSpeed = 3f;
        this.damage = 2f;
        this.maxHp = 10f;
        this.curHp = maxHp;
    }
   

    public override void Move()
    {
        float dist = Vector3.Distance(transform.position, Player.Instance.transform.position);

        if (dist < searchRange && isMove == true)
        {
            Debug.Log("이동");
            Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (dist < attacthRange && isAttack == true)
            {
                StartCoroutine(Attack());
            }

        }
        
    }

    private  IEnumerator Attack()
    {
        Debug.Log("공격");
        isAttack = false;
        isMove = false;
        Player.Instance.TakeDamage(damage);
        yield return new WaitForSeconds(2f);
        isMove = true;
        yield return new WaitForSeconds(attackCoolTime);
        isAttack = true;
    }

}
