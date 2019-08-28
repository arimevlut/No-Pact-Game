using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public abstract class PlayersControl : MonoBehaviour
{
    protected Rigidbody2D Player_Rigidbody;

    BobControl BobControl_Script;
    SpikeMonster SpikeMonster_Script;

    [SerializeField]
    protected float Player_Speed;
    [SerializeField]
    protected float Player_JumpForce;
    public float Push_Force;

    public bool Player_LookLeft;
    protected bool Player_IsThrowing;
    protected bool Player_IsFly;
    protected bool Player_IsAttack;
    public bool Player_IsHurt;
    protected bool Player_BreakGround;
    protected bool Player_IsDead;

    protected float Current_Time;

    #region Player_Animations
    public enum STATE
    {
        idle,
        run,
        throwing,
        attack,
        groundbreak,
        dead
    }

    private STATE _state = STATE.idle;

    public STATE state
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;

            switch (value)
            {
                case STATE.idle:
                    Player_Animation.SetFloat("VelocityX", 0);
                    break;
                case STATE.run:
                    Player_Animation.SetFloat("VelocityX", Mathf.Abs(Player_Rigidbody.velocity.x));
                    break;
                case STATE.throwing:
                    Player_Animation.SetTrigger("Throwing");
                    break;
                case STATE.attack:
                    Player_Animation.SetTrigger("Attack");
                    break;
                case STATE.groundbreak:
                    Player_Animation.SetTrigger("GroundBreak");
                    break;
                case STATE.dead:
                    Player_Animation.SetTrigger("Dead");
                    break;
            }
        }
    }

    protected Animator Player_Animation;
    #endregion

    #region Ground_Control_Variables
    [SerializeField]
    protected bool isGround;

    [SerializeField]
    protected Transform Detect_Ground;

    [SerializeField]
    protected LayerMask Ground;

    const float Detect_Ground_Rate = 0.2f;
    #endregion

    #region Player_Bars_Control
    //health bar graphic
    //[SerializeField]
    public BarsControl health;

    //mana bar graphic
    [SerializeField]
    protected BarsControl mana;

    //power bar graphic
    [SerializeField]
    protected BarsControl power;

    private float initHealth = 100;
    private float initMana = 100;
    private float initPower = 100;
    #endregion

    protected virtual void Start()
    {
        Player_Rigidbody = GetComponent<Rigidbody2D>();
        Player_Animation = GetComponent<Animator>();
        BobControl_Script = FindObjectOfType<BobControl>();
        SpikeMonster_Script = FindObjectOfType<SpikeMonster>();

        Detect_Ground = transform.Find("GroundDetect");

        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);
        power.Initialize(initPower, initPower);

        Player_IsThrowing = false;
        Player_IsFly = false;
        Player_IsAttack = false;
        Player_IsHurt = false;
        Player_BreakGround = false;
        Player_IsDead = false;
    }

    protected virtual void Update()
    {
        Player_Move();
        Update_State();
        Control_Ground();
        Increase_Power();
        Player_GetInput();
        Reset_Value();

        if (health.MyCurrentValue <= 0)
        {
            Player_Dead();
            Destroy(this.gameObject, 1f);
        }

        if (mana.MyCurrentValue <= 0)
        {
            mana.MyCurrentValue = 0;
        }
    }

    //for both
    private void Player_GetInput()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGround)
        {
            Player_Jump();
        }
    }

    //for both
    public void Player_Move()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        Player_Rigidbody.velocity = new Vector2(h * Player_Speed, Player_Rigidbody.velocity.y);
        Player_Turn(h);
    }

    //for both
    public void Player_Jump()
    {
        Player_Rigidbody.velocity = new Vector2(0, Player_JumpForce);
    }

    //for alice
    public virtual void Player_Throwing()
    {
        Player_IsThrowing = true;
    }

    //for bob
    public virtual void Player_Attack()
    {
        Player_IsAttack = true;
    }

    //for alice
    public virtual void Alice_Special_Power()
    {
        Player_IsFly = true;
    }

    //for bob
    public virtual void Bob_Special_Power()
    {
        Player_BreakGround = true;
    }

    //for both
    public void Player_Hurt()
    {
        if (Player_IsHurt)
        {
            if (Player_LookLeft)
                Player_Rigidbody.AddForce(new Vector2(Push_Force, 0), ForceMode2D.Force);
            else if (!Player_LookLeft)
                Player_Rigidbody.AddForce(new Vector2(-Push_Force, 0), ForceMode2D.Force);

            Player_IsHurt = false;
        }
    }

    //for both
    private void Player_Dead()
    {
        Player_IsDead = true;
    }

    //for both
    private void Control_Ground()
    {
        isGround = Physics2D.OverlapCircle(Detect_Ground.position, Detect_Ground_Rate, Ground);
        Ground = LayerMask.GetMask("Ground");
    }

    //for both
    private void Player_Turn(float h)
    {
        if (h > 0 && Player_LookLeft || h < 0 && !Player_LookLeft)
        {
            Player_LookLeft = !Player_LookLeft;
            Vector2 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }

    //for both
    private void Update_State()
    {
        if (Player_Rigidbody.velocity.x == 0)
        {
            state = STATE.idle;
        }
        if (Mathf.Abs(Player_Rigidbody.velocity.x) > 0f)
        {
            state = STATE.run;
        }
        if (Player_IsThrowing && mana.MyCurrentValue > 0)
        {
            state = STATE.throwing;
        }
        if (Player_IsAttack)
        {
            state = STATE.attack;
        }
        if (Player_BreakGround)
        {
            state = STATE.groundbreak;
        }
        if (Player_IsDead)
        {
            state = STATE.dead;
        }
    }

    //for both
    protected virtual void Reset_Value()
    {
        Player_IsThrowing = false;
        Player_IsFly = false;
        Player_IsAttack = false;
        Player_BreakGround = false;
    }

    //for both
    private void Increase_Power()
    {
        Current_Time += Time.deltaTime;
        if (Current_Time > 3f)
        {
            Current_Time = 0;
            power.MyCurrentValue += 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "health")
        {
            health.MyCurrentValue += 10;
            Destroy(other.gameObject);
        }
        if (other.tag == "mana")
        {
            mana.MyCurrentValue += 10;
            Destroy(other.gameObject);
        }
        if (other.tag == "Monster_Bullet")
        {
            Destroy(other.gameObject);
            Player_IsHurt = true;
            health.MyCurrentValue -= 20;
            Player_Hurt();
        }
    }

}
