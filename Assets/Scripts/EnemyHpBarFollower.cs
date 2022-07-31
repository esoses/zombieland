using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarFollower : MonoBehaviour
{
    public Transform enemyPosToFollow;    
    Vector3 pos;

    private void Awake()
    {
        pos = new Vector3(enemyPosToFollow.position.x, enemyPosToFollow.position.y + 0.5f);       
    }

    private void FixedUpdate()
    {
        if (enemyPosToFollow != null)
        {
            pos.x = enemyPosToFollow.position.x;
            pos.y = enemyPosToFollow.position.y + 0.5f;
            transform.position = pos;
        }        
    }
}