using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Player : Unit
{
    public static Player Instance;

    #region compoment
    private Rigidbody2D rigidbody2D;
    private Vector2 movement;
    private Vector2 dir;
    public float angle;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region 구르기
    private Vector2 lastMoveDirection = Vector2.right;
    [SerializeField]
    private bool isRoll = true;
    [SerializeField]
    private float rollSpeed = 8f;
    private float rollCooltime = 1f;
    public float rollAnimTime = 0;
    #endregion

    #region 총 
     
    public GameObject hand;
    public Transform aimingPoint;
    public int macBullet = 20;
    public int currentBullt;

    private float bulletSpeed = 20f;

    float handAngle;
    Vector2 target, mouse;
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
        HandAngle();
        FlipSpriteByMouse();
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

        target = transform.position;

      
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
            //animator.SetBool("isMove", false);
            //animator.SetBool("isIdle", false);
            OnAnim(0);

        }
        if(Input.GetAxisRaw("Horizontal") == -1)
        {
            spriteRenderer.flipX = true;
            //animator.SetBool("isMove", false);
            OnAnim(0);
        }
    }


    public override void Move()
    {
        Idle();
     
        if (isMove)
        {
            moveing = true;
            //animator.SetBool("isIdle", false);
            OnAnim(1);
            //hand.SetActive(false);
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //spriteRenderer.flipX = movement.x < 0;
            if (movement.x != 0 || movement.y != 0)
            {
                //animator.SetBool("isMove", true);
                OnAnim(1);
            }
            else
            {
                OnAnim(0);
                //animator.SetBool("isMove", false);
                moveing = false;
                //hand.SetActive(true);
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

            OnAnim(2);
           
            Vector2 rollDirection = (movement != Vector2.zero) ? movement.normalized : lastMoveDirection;

            rigidbody2D.velocity = rollDirection * rollSpeed;
            rollAnimTime = 0;
            RuntimeAnimatorController controller = animator.runtimeAnimatorController;
            for (int i = 0; i < controller.animationClips.Length; i++)
            {
                rollAnimTime = i*0.1f;
            }
            StartCoroutine(RollAnim());

        }
    }
    private IEnumerator RollAnim()
    {
        yield return new WaitForSeconds(rollAnimTime);
        isMove = true;
        isAttack=true;
        OnAnim(0);
        StartCoroutine(EndRoll());

    }
    private IEnumerator EndRoll()
    {
        yield return new WaitForSeconds(rollCooltime); 
        isRoll = true;
        //animator.SetBool("isRoll", false);
        
    }
    #endregion

    #region 공격
    
    private void AimingPoint()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z)));
        aimingPoint.transform.position = point;
    }
    private void HandAngle()
    {
        target = hand.transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mouse - target;
        handAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        hand.transform.rotation = Quaternion.Euler(0, 0, handAngle);

        if (mouse.x < transform.position.x)
        {
            hand.transform.localScale = new Vector3(1, -1, 1); 
        }
        else
        {
            hand.transform.localScale = new Vector3(1, 1, 1); 
        }
    }


    private void FlipSpriteByMouse()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            spriteRenderer.flipX = mouse.x < transform.position.x;
        }
    }

    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void OnAnim(int _animN)
    {
        animator.SetInteger("PlayerSet", _animN);
        
    }

}
