using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceControl : PlayersControl
{
    [SerializeField]
    protected float Power_Force;
    [SerializeField]
    protected GameObject Shield_Object;
    [SerializeField]
    protected GameObject Bullet_Object;
    [SerializeField]
    protected GameObject Bullet_Exit_Point;

    protected float Attack_Current_Time;

    protected override void Start()
    {
        base.Start();
        Shield_Object.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        
        if (Player_IsFly == false)
        {
            Shield_Object.SetActive(false);
        }
    }

    public override void Alice_Special_Power()
    {
            if (Player_LookLeft)
                Player_Rigidbody.AddForce(new Vector2(-Power_Force, 0), ForceMode2D.Impulse);
            if (!Player_LookLeft)
                Player_Rigidbody.AddForce(new Vector2(Power_Force, 0), ForceMode2D.Force);
        
        Shield_Object.SetActive(true);       
    }

    public override void Player_Throwing()
    {
        if (mana.MyCurrentValue > 0)
        {
            if (Time.time > Attack_Current_Time)
            {
                Attack_Current_Time = Time.time + 0.5f;
                Instantiate(Bullet_Object, Bullet_Exit_Point.transform.position, Quaternion.identity);
            }
        }
              
    }

    public void Player_GetInputAttack()
    {
        if (mana.MyCurrentValue > 0)
        {
            Player_IsThrowing = true;
            mana.MyCurrentValue -= 10;
            Player_Throwing();
        } 
    }

    public void Player_GetInputSpecialAttack()
    {
        if (power.MyCurrentValue > 0)
        {
            Player_IsFly = true;
            power.MyCurrentValue -= 5;
            Alice_Special_Power();
        }
    }
}
