using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBirdControl : MonstersControl
{
    private Rigidbody2D Monster_Rigidbody;

    [SerializeField]
    private float RedBird_Speed;

    private bool RedBird_Turn;

    private bool IsWall;
    [SerializeField]
    private Transform Detect_Wall;
    const float Wall_Detect_Rate = 0.2f;
    [SerializeField]
    private LayerMask Wall;

    protected override void Start()
    {
        base.Start();
        Monster_Rigidbody = GetComponent<Rigidbody2D>();

        Detect_Wall = transform.Find("Detect_Wall");
    }

    protected override void Update()
    {
        base.Update();
        Detect_Wall_Func();
        RedBird_Move();
    }

    private void Detect_Wall_Func()
    {
        IsWall = Physics2D.OverlapCircle(Detect_Wall.position, Wall_Detect_Rate, Wall);
    }

    private void RedBird_Move()
    {
        if (IsWall == true)
        {
            RedBird_Speed *= -1;
            Monster_Rigidbody.velocity = new Vector2(RedBird_Speed, 0);
            RedBird_Turn = !RedBird_Turn;
            Vector2 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }
        else
            Monster_Rigidbody.velocity = new Vector2(-RedBird_Speed, 0);
    }
}
