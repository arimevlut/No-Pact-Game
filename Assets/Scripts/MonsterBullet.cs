using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    Rigidbody2D Monster_Bullet_Rigidbody;

    [SerializeField]
    protected float Bullet_Speed;

    void Start()
    {
        Monster_Bullet_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Monster_Bullet_Rigidbody.AddForce(new Vector2(-Bullet_Speed, 0), ForceMode2D.Force);
        Destroy(this.gameObject, 0.75f);
    }
}
