using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoterEnemy : EnemyController
{
    public enum State { CHASING, SHOOTING}

    private State state = State.CHASING;

    public Weapon weapon;

    protected override void MoveCharacter(Vector2 direction)
    {
        if (state == State.CHASING)
        {
            base.MoveCharacter(direction);
        }
    }

    protected override void ChasePlayer()
    {
        if (state == State.CHASING)
        {
            base.ChasePlayer();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            state = State.SHOOTING;
            weapon.Fire();
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            weapon.AudioSource.Stop();
            state = State.CHASING;
        }
    }
}
