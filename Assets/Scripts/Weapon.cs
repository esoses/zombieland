using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    [System.Serializable]
    public class Upgrade
    {
        public string upName;
        public int upgradeNumber;
        public float[] upgradeStates;
        public int[] upgradeCosts;
        public int upgradeLevel;
    }
    

    public Upgrade[] upgrades;

    private Quaternion qnt = new Quaternion(0, 0, 0, 0);

    public GameObject bullet;
    public Transform firePoint;

    public float damage; //1
    public float fireForce; //2 
    public float weaponRecoil; //3
    public float fireRate; //4
    public int bulletsPerShot; //5
    public int maxAmmo; //6
    public float reloadTime; //7 
    public float movementPenaltyInPercent; //8
    public int enemiesPierced;
    public float timeUntilBulletDisappears;

    public bool isFullAuto;
    public bool dpesPierceInfinietly;
    public bool hasAnimation;

    private float realReloadTime;
    public AudioSource shotAudio;
    public AudioSource reloadAudio;
    public Animation animation;


    [HideInInspector] public float reloadCounter;
    [HideInInspector] public int ammoPool;
    private float semiCounter;
    private int counter;

    public void UpdateStats(Upgrade upgrade)
    {
        if (upgrade.upgradeNumber == 1)
        {            
            damage = upgrade.upgradeStates[upgrade.upgradeLevel];            
            bullet.GetComponent<Bullet>().damage = damage;            
        }
        if (upgrade.upgradeNumber == 2)
        {
            fireForce = upgrade.upgradeStates[upgrade.upgradeLevel];
        }
        if (upgrade.upgradeNumber == 3)
        {
            weaponRecoil = upgrade.upgradeStates[upgrade.upgradeLevel];
        }
        if (upgrade.upgradeNumber == 4)
        {
            fireRate = upgrade.upgradeStates[upgrade.upgradeLevel];
        }
        if (upgrade.upgradeNumber == 5)
        {
            bulletsPerShot = (int)upgrade.upgradeStates[upgrade.upgradeLevel];
        }
        if (upgrade.upgradeNumber == 6)
        {
            maxAmmo = (int)upgrade.upgradeStates[upgrade.upgradeLevel];
        }
        if (upgrade.upgradeNumber == 7)
        {
            reloadTime = upgrade.upgradeStates[upgrade.upgradeLevel];
        }
        if (upgrade.upgradeNumber == 8)
        {
            movementPenaltyInPercent = upgrade.upgradeStates[upgrade.upgradeLevel];
        }
    }


    public void Start()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            UpdateStats(upgrades[i]);
        }
        realReloadTime = reloadTime * (PlayerPrefs.GetFloat("multi4", 1));
        //Debug.Log(realReloadTime);
        bullet.GetComponent<Bullet>().damage = damage * PlayerPrefs.GetFloat("multi1", 1);
        
        if(hasAnimation)
        {
            animation = gameObject.GetComponent<Animation>();
        }
        semiCounter = fireRate;
        ammoPool = maxAmmo;
        reloadCounter = realReloadTime;
        counter = 0;
    }

    public void Fire()
    {
        if (isFullAuto)
        {            
            if (counter % fireRate == 0 && ammoPool > 0)
            {
                
                
                shotAudio.Play();
                if (hasAnimation) animation.Play();

                for (int i = 0; i < bulletsPerShot; i++)
                {
                    float scatterOffset = Random.Range(-weaponRecoil, weaponRecoil);

                    GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation *= Quaternion.Euler(0f, 0f, scatterOffset));
                    projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

                    firePoint.rotation = qnt;
                }

                ammoPool -= 1;
            }
            counter++;

        }
        if (!isFullAuto)
        {

            if (semiCounter <= 0 && ammoPool > 0)
            {
                if (hasAnimation)  animation.Play();
                semiCounter = fireRate;
                shotAudio.Play();

                for (int i = 0; i < bulletsPerShot; i++)
                {
                    float scatterOffset = Random.Range(-weaponRecoil, weaponRecoil);

                    GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation *= Quaternion.Euler(0f, 0f, scatterOffset));
                    projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

                    firePoint.rotation = qnt;
                }

                ammoPool -= 1;
            }
        }
    }
    

    private void Update()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {          
            UpdateStats(upgrades[i]);
        }

        if (semiCounter > 0)
        {
            semiCounter -= Time.deltaTime;
        }
        
        if (ammoPool == 0)
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (reloadCounter == realReloadTime)
        {
            reloadAudio.Play();
        }

        reloadCounter -= Time.deltaTime;

        if (reloadCounter <= 0)
        {
            ammoPool = maxAmmo;
            reloadCounter = realReloadTime;
        }
    }

    public void ForceReload()
    {
        ammoPool = 0;
    }
}




