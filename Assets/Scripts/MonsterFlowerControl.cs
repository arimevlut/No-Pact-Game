using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFlowerControl : MonstersControl
{
    [SerializeField]
    protected GameObject Monster_Bullet;
    [SerializeField]
    protected GameObject Monster_Bullet_Exit_Point;

    private float Fire_Rate = 1.5f;
    private float Next_Fire = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Shooting();
    }

    private void Shooting()
    {
        if (Time.time > Next_Fire)
        {
            Instantiate(Monster_Bullet, Monster_Bullet_Exit_Point.transform.position, Quaternion.identity);
            Next_Fire = Time.time + Fire_Rate;
        }
    }
}
