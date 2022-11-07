using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isForAttackinEnemies;
    public Weapon gun;   
    private Rigidbody2D rb2d;
    public float timeToDestroyAfterHit;
    public float timeUntilBulletDisapears;
    public float damage;
    private int pierceCounter;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timeUntilBulletDisapears = gun.timeUntilBulletDisappears;
        Destroy(gameObject, timeUntilBulletDisapears);
        pierceCounter = gun.enemiesPierced;

    }

    

    private void Update()
    {
        //Debug.Log(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !collision.isTrigger)
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(damage);       
            if(gun.dpesPierceInfinietly == false && pierceCounter > 0)
            {
                pierceCounter--;
            }
            else if (gun.dpesPierceInfinietly == false)
            {
                Destroy(gameObject, timeToDestroyAfterHit);   
            }
            
                     
        }
        if (collision.tag == "Player" && !collision.isTrigger && !isForAttackinEnemies)
        {
            collision.gameObject.GetComponent<HeroController>().DamagePlayer(damage);
            Destroy(gameObject);
        }
    }
}
