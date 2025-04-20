using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Unit
{
    public static Player Instance;

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
    public Transform aimingPoint;
    public int macBullet = 20;
    public int currentBullt;

    private float bulletSpeed = 20f;
    #endregion

    private void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        AimingPoint();
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
