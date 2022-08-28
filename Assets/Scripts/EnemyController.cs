using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour
{
    protected Vector2 move;
    protected Rigidbody2D enemy;
    protected GameObject player;

    public float maxHitPoints = 100;
    protected float hitPoints;
    public HealthBar hpBar;
    public GameObject parent;
    public GameObject prefab;

    protected float moveSpeed;
    public float moveSpeedRangeMin;
    public float moveSpeedRangeMax;
  
    void Start()
    {
        hitPoints = maxHitPoints;
        hpBar.SetMaxHealth(maxHitPoints);
        enemy = GetComponent<Rigidbody2D>();      
        player = GameObject.FindGameObjectWithTag("Player");
        moveSpeed = Random.Range(moveSpeedRangeMin, moveSpeedRangeMax);
    }

    private void FixedUpdate()
    {      
        if (hitPoints <= 0)
        {
            KillEnemy();
        }
        ChasePlayer();
        MoveCharacter(move);
    }

    virtual protected void MoveCharacter(Vector2 direction)
    {
        enemy.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));        
    }

    public void DamageEnemy(float damageValue)
    {       
        hitPoints -= damageValue;
        hpBar.SetHealth(hitPoints);      
    }

    private void KillEnemy()
    {      
        Destroy(parent);                     
    }

    virtual protected void ChasePlayer()
    {
        if (player != null && player.activeSelf)
        {
            Vector2 direction = player.GetComponent<Transform>().position - transform.position;            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            enemy.rotation = angle;                       
            direction.Normalize();            
            move = direction;            
        }                
    }
}
