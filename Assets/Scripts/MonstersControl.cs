using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonstersControl : MonoBehaviour
{
    protected PlayersControl PlayersControl_Script;

    [SerializeField]
    protected float Monster_Health;

    protected virtual void Start()
    {
        PlayersControl_Script = FindObjectOfType<PlayersControl>();
    }

    protected virtual void Update()
    {
        if (Monster_Health <= 0)
        {
            Monster_Death();
        }
    }

    private void Monster_Death()
    {
        Destroy(this.gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Sword2":
                Monster_Health -= 10;
                break;

            case "Player":
                PlayersControl_Script.Player_IsHurt = true;
                PlayersControl_Script.health.MyCurrentValue -= 10;
                PlayersControl_Script.Player_Hurt();
                break;

            case "Bullet":
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                break;

            case "Shield":
                Destroy(this.gameObject);
                PlayersControl_Script.health.MyCurrentValue += 10;
                break;
        }
    }
}
