using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isForAttackinEnemies;
    public float bulletDamage;
    private Rigidbody2D rb2d;   
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();        
    }

    private void Awake()
    {
        Destroy(gameObject, Random.Range(0.5f, 1.5f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !collision.isTrigger)
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(bulletDamage);                                  
            Destroy(gameObject, 1);            
        }
        if (collision.tag == "Player" && !collision.isTrigger)
        {
            collision.gameObject.GetComponent<HeroController>().DamagePlayer(bulletDamage);
            Destroy(gameObject);
        }
    }
}
