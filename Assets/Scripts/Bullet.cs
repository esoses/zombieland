using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isForAttackinEnemies;
    public float bulletDamage;
    private Rigidbody2D rb2d;
    public float timeToDestroyAfterHit;
    public float timeUntilBulletDisapears;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();        
    }

    private void Awake()
    {
        //Destroy(gameObject, Random.Range(0.5f, 1.5f));
        Destroy(gameObject, timeUntilBulletDisapears);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !collision.isTrigger)
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(bulletDamage);                                  
            Destroy(gameObject, timeToDestroyAfterHit);            
        }
        if (collision.tag == "Player" && !collision.isTrigger && !isForAttackinEnemies)
        {
            collision.gameObject.GetComponent<HeroController>().DamagePlayer(bulletDamage);
            Destroy(gameObject);
        }
    }
}
