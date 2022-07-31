using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 move = new Vector2();
    public float moveSpeed = 4;

    public Weapon weapon;
    
    public float maxMana = 100;
    public float mana;
    public float manaRegen = 1f;
    public ManaBar mn;
    public float maxHealth = 100;
    public float health;
    public float hpRegen = 1f;
    public HealthBar hp;

    private int counter = 0;

    public float damageTakenPerFrame = 1;
    
    public static int switcher;
    public static int hpswitcher;

    void Start()
    {
        mana = maxMana;
        health = maxHealth;

        rb2d = GetComponent<Rigidbody2D>();
        hp.SetMaxHealth(health);
        mn.SetMaxMana(mana);

        Application.targetFrameRate = 60;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Damaging Wall")
        {
            health -= damageTakenPerFrame;
            hp.SetHealth(health);
        }
    }

    public void PlayerFire()
    { 
        if (Input.GetMouseButton(0))
        {
            if (mana > 0)
            {
                weapon.Fire();
                mana -= weapon.manaUsage;
            }
        }
        if (Input.GetMouseButton(0) == false)
        {
            weapon.AudioSource.Stop();
        }
    }    

    public void Update()
    {
        if (EscameMenu.GameIsPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("World 1");
            }
            mn.SetMana(mana);
            FaceMouse();
            MoveHero();

            counter++;
            if (counter % weapon.fireRate == 0)
            {
                PlayerFire();

            }

            if (mana < maxMana)
            {
               
                if (mana < maxMana / 4)
                {
                    mana += manaRegen * 0.03f;
                    switcher = 0;
                }
                else if (mana < maxMana / 4)
                {
                    mana += manaRegen * 0.04f;
                    switcher = 1;
                }
                else if (mana < maxMana / 3)
                {
                    mana += manaRegen * 0.06f;
                    switcher = 2;
                }
                else
                {
                    mana += manaRegen * 0.07f;
                    switcher = 3;
                }
                mn.AddRegenMana(manaRegen);
            }

            if (health < maxHealth)
            {
                
                if (health < maxHealth / 4)
                {
                    health += hpRegen * 0.01f;
                    hpswitcher = 0;
                }
                else if (health < maxHealth / 4)
                {
                    health += hpRegen * 0.02f;
                    hpswitcher = 1;
                }
                else if (health < maxHealth / 3)
                {
                    health += hpRegen * 0.03f;
                    hpswitcher = 2;
                }
                else
                {
                    health += hpRegen * 0.04f;
                    hpswitcher = 3;
                }
                hp.AddRegenHealth(hpRegen);
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void MoveHero()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        move.Normalize();
        rb2d.velocity = move * moveSpeed;
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        transform.up = direction;
    }
}
