using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BobControl : PlayersControl
{
    protected float Attack_Current_Time;
    protected float GroundBreak_Current_Time;

    public BoxCollider2D Sword_Collider;
    public BoxCollider2D GroundBreaker_Collider;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        //Player_GetInputAttack();
        //Player_GetInputSpecialAttack();
    }

    public override void Player_Attack()
    {
        if (Time.time > Attack_Current_Time)
        {
            Attack_Current_Time = Time.time + 0.2f;
            Sword_Collider.enabled = true;
        }
    }

    public override void Bob_Special_Power()
    {
        if (Time.time > GroundBreak_Current_Time)
        {
            GroundBreak_Current_Time = Time.time + 0.2f;
            GroundBreaker_Collider.enabled = true;
        }

        
    }

    public void Player_GetInputAttack()
    {
        if (mana.MyCurrentValue > 0)
        {
            Player_IsAttack = true;
            mana.MyCurrentValue -= 10;
            Player_Attack();
        }

        Sword_Collider.enabled = false;
    }

    public void Player_GetInputSpecialAttack()
    {
        if (power.MyCurrentValue > 0)
        {
            Player_BreakGround = true;
            power.MyCurrentValue -= 10;
            Bob_Special_Power();
            
        }

        
    }

    protected override void Reset_Value()
    {
        base.Reset_Value();
        GroundBreaker_Collider.enabled = false;
    }
}
