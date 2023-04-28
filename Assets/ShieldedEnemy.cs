using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedEnemy : EnemyController
{
    public enum State {RUNNING,DEFENDING}
    private State state = State.RUNNING;

    public GameObject sh1;
    public GameObject sh2;
    private float speed;

    private void Start()
    {
        enemy = gameObject.GetComponent<Rigidbody2D>();
        speed = moveSpeed;
        state = State.RUNNING;
        sh1.SetActive(false);
        sh2.SetActive(true);
    }

    protected override void MoveCharacter(Vector2 direction)
    {        
        base.MoveCharacter(direction);        
    }
    protected override void ChasePlayer()
    {
        base.ChasePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            state = State.DEFENDING;
            moveSpeed = speed - 2;
            sh1.SetActive(true);
            sh2.SetActive(false);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            state = State.RUNNING;
            moveSpeed = speed;
            sh1.SetActive(false);
            sh2.SetActive(true);
        }
    }
}
