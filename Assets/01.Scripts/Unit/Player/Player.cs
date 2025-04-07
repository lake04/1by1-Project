using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Unit
{
    #region compoment
    private Rigidbody2D rigidbody2D;
    private Vector2 movement;
    #endregion

    #region 구르기 관련
    private Vector2 lastMoveDirection = Vector2.right;
    private bool isRoll = true;
    private float rollSpeed = 2f;
    private float rollCooltime = 0.6f;
    #endregion

    #region 총
    [SerializeField]
    private GameObject aimingPoint;
    private Transform mosusePoint;
    public int macBullet = 20;
    public int currentBullt;

    private float bulletSpeed = 20f;
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        AimingPoint();
        ConrolAttack();
        Roll();
    }

    private void FixedUpdate()
    {
        ControlPlayer();
    }

    private void Init()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        maxHp = 20f;
        curHp = maxHp;
        moveSpeed = 5f;
        damage = 5f;
        currentBullt = macBullet;
        attackCoolTime = 0.5f;
        isAttack = true;
    }

    #region 플레이어 컨틀롤러    
    private void ControlPlayer()
    {
        Move();
    }

    public override void Move()
    {
        if (isMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement != Vector2.zero) 
            {
                lastMoveDirection = movement.normalized;
            }

            rigidbody2D.velocity = movement.normalized * moveSpeed;
        }
    }

    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isRoll)
        {
            Debug.Log("구르기");

            isMove = false;
            isRoll = false;

            Vector2 rollDirection = (movement != Vector2.zero) ? movement.normalized : lastMoveDirection;

            rigidbody2D.velocity = rollDirection * rollSpeed;

            StartCoroutine(EndRoll());
        }
    }
    private IEnumerator EndRoll()
    {
        yield return new WaitForSeconds(rollCooltime); 
        isMove = true;
        isRoll = true;
    }
    #endregion

    #region 총 관련

    private void ConrolAttack()
    {
        if (Input.GetMouseButton(0) && isAttack == true)
        {
            if(currentBullt>0 )
            {
                StartCoroutine(Shooting());
            }
            else
            {
                StartCoroutine(MeleeAttack());
            }
        }
    }
    private IEnumerator Shooting()
    {
        GameObject _curBullet = GameManager.Instance.pool.Get(0);
        currentBullt--;
        isAttack = false;

        if (_curBullet != null)
        {
            _curBullet.transform.position = transform.position;
            Vector2 dir = (aimingPoint.transform.position - transform.position).normalized;
            _curBullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
        }

        yield return new WaitForSeconds(attackCoolTime);
        isAttack = true;
    }

    
    private IEnumerator MeleeAttack()
    {
        isAttack = false;
        Vector2 attackDirection = ((Vector2)aimingPoint.transform.position - (Vector2)transform.position).normalized;

        float attackOffsetDistance = 1.0f;  
        Vector2 attackCenter = (Vector2)transform.position + attackDirection * attackOffsetDistance;

        Vector2 attackSize = new Vector2(1.5f, 1.0f);

        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackCenter, attackSize, attackAngle);

        Debug.DrawLine(transform.position, attackCenter, Color.red, 1.0f);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("적 공격!" );
            }
        }

        yield return new WaitForSeconds(attackCoolTime);
        isAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 attackDirection = lastMoveDirection;
        float attackOffsetDistance = 1.0f;
        Vector2 attackCenter = (Vector2)transform.position + attackDirection * attackOffsetDistance;
        Vector2 attackSize = new Vector2(1.5f, 1.0f);
        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        // Gizmos의 Matrix를 이용해 회전 적용
        Gizmos.matrix = Matrix4x4.TRS(attackCenter, Quaternion.Euler(0, 0, attackAngle), Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, attackSize);
    }

    private void AimingPoint()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));
        aimingPoint.transform.position = point;
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
