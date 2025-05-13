using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Unit
{
    public static Player Instance;

    #region compoment
    private Rigidbody2D rigidbody2D;
    private Vector2 movement;
    public float angle;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region 구르기
    private Vector2 lastMoveDirection = Vector2.right;
    [SerializeField]
    private bool isRoll = true;
    [SerializeField]
    private float rollSpeed = 4f;
    private float rollCooltime = 0.8f;
    #endregion

    #region 총 bd
     
    public GameObject hand;
    public Transform aimingPoint;
    public int macBullet = 20;
    public int currentBullt;

    private float bulletSpeed = 20f;
    #endregion

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject );
            return;
        }
        else Instance = this;


        Init();
    }
    void Start()
    {

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
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
        maxHp = 20f;
        curHp = maxHp;
        moveSpeed = 5f;
        damage = 5f;
        currentBullt = macBullet;
        attackCoolTime = 0.5f;
        isAttack = true;
    }

    #region 움직임 
    private void ControlPlayer()
    {
        Move();
    }

    private void Idle()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isMove", false);

            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
        if(Input.GetAxisRaw("Horizontal") == -1)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("isMove", false);

            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }


    public override void Move()
    {
        Idle();
        if (isMove)
        {
            moveing = true;
            animator.SetBool("isIdle", false);
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //spriteRenderer.flipX = movement.x < 0;
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetBool("isMove", true);
            }
            else
            {
                animator.SetBool("isMove", false);
                moveing = false;
            }
            animator.SetFloat("inPutX", movement.x);
            animator.SetFloat("inPutY", movement.y);

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
            isMove = false;
            isRoll = false;
            isAttack = false;
            animator.SetBool("isRoll",true );

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
        isAttack=true;
        animator.SetBool("isRoll", false);
    }
    #endregion

    #region 공격
    
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
