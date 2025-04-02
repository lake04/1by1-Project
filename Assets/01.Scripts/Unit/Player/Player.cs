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
    [SerializeField]
    private GameObject bullet;
    private float bulletSpeed = 20f;
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {
        AimingPoint();
        Shooting();
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
    private void Shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {   
            GameObject _curBullet = GameManager.Instance.pool.Get(0);

            if (_curBullet != null)
            {
                _curBullet.transform.position = transform.position; 
                Vector2 dir = (aimingPoint.transform.position - transform.position).normalized;
                _curBullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
            }
        }
    }
    private void AimingPoint()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
               Input.mousePosition.y, -Camera.main.transform.position.z));
        aimingPoint.transform.position = point;
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
