using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private Rigidbody2D Bullet_Rigidbody;
    private PlayersControl PlayersControl_Script;

    [SerializeField]
    private float Bullet_Force;

    void Start()
    {
        Bullet_Rigidbody = GetComponent<Rigidbody2D>();
        PlayersControl_Script = FindObjectOfType<PlayersControl>();
    }

    void Update()
    {
        Bullet_Move();
    }

    private void Bullet_Move()
    {
        if (PlayersControl_Script.Player_LookLeft)
            Bullet_Rigidbody.AddForce(new Vector2(-Bullet_Force, 0), ForceMode2D.Force);
        if (!PlayersControl_Script.Player_LookLeft)
            Bullet_Rigidbody.AddForce(new Vector2(Bullet_Force, 0), ForceMode2D.Force);

        Destroy(this.gameObject, 0.75f);
    }
}
