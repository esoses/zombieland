using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HeroController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 move = new Vector2();
    public float moveSpeed = 4;
    private float realmoveSpeed;
    public float viewRange;

    int selectedWeapon;
    private Weapon weapon;
    public GameObject[] weapons;    
    
    public float maxHealth = 100;
    public float health;
    public float hpRegen = 1f;
    public HealthBar hp;

 
    public TextMeshProUGUI dieDisplay;
    public TextMeshProUGUI ammoNow;
    public TextMeshProUGUI ammoMax;

    public float damageTakenPerFrame = 1;
    
    public static int switcher;
    public static int hpswitcher;

    public EscameMenu EscameMenu;

    void Start()
    {
        maxHealth = 100 * PlayerPrefs.GetFloat("multi2" , 1);
        realmoveSpeed = moveSpeed * PlayerPrefs.GetFloat("multi3", 1);
        
        weapons = GameObject.FindGameObjectsWithTag("Weapon");

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);            
            weapon = weapons[0].GetComponent<Weapon>();
            weapon.gameObject.SetActive(true);
        }      
        
        dieDisplay.text = "";

        health = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        hp.SetMaxHealth(health);

        Application.targetFrameRate = 60;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "Enemy" && collision.isTrigger == false) || collision.tag == "Damaging Wall")
        {            
            health -= damageTakenPerFrame;
            hp.SetHealth(health);
        }
    }

    public void DamagePlayer(float bulletDamage)
    {
        health -= bulletDamage;
        hp.SetHealth(health);
    }

    public void PlayerFire()
    {
        if (weapon.isFullAuto == true)
        {
            if (Input.GetMouseButton(0))
            {                               
                weapon.Fire();                                               
            }            
        }
        if (weapon.isFullAuto == false)
        {
            if (Input.GetMouseButtonDown(0))
            {               
                weapon.Fire();                
            }
        }
    }    

    void SelectWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = weapons[0].GetComponent<Weapon>();
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);               
            }
            weapons[0].SetActive(true);
            if (weapon.ammoPool == 0)
            {
                weapon.reloadCounter = weapon.reloadTime;
                weapon.reloadAudio.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = weapons[1].GetComponent<Weapon>();
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[1].SetActive(true);
            if (weapon.ammoPool == 0)
            {
                weapon.reloadCounter = weapon.reloadTime;
                weapon.reloadAudio.Play();
            }
        }
       
    }

    public void Update()
    {
       
        if (EscameMenu.GameIsPaused == true)
        {
            weapon.reloadAudio.Pause();
        }
        if (EscameMenu.GameIsPaused == false)
        {
            weapon.reloadAudio.UnPause();
            SelectWeapon();

            ammoMax.text = weapon.maxAmmo.ToString();
            ammoNow.text = weapon.ammoPool.ToString();

            FaceMouse();
            MoveHero();                       
            PlayerFire();
            if (Input.GetKeyDown(KeyCode.R))
            {
                weapon.ForceReload();
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
                Invoke("MenuAfterDeath", 2);
                dieDisplay.text = "You Died";
                gameObject.SetActive(false);                 
            }
        }
    }

    void MenuAfterDeath()
    {
        
        SceneManager.LoadScene("Main Menu");
    }
    

    void MoveHero()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        
        move.Normalize();
        rb2d.velocity = move * realmoveSpeed * (1 - weapon.movementPenaltyInPercent / 100);
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
