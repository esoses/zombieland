using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Weapon : MonoBehaviour
{
    private Quaternion qnt = new Quaternion(0, 0, 0, 0);


    public GameObject bullet;
    public Transform firePoint;

    public float fireForce;
    public float weaponRecoil;
    public float fireRate;
    public int bulletsPerShot;    
    public bool isFullAuto;
    public int maxAmmo;
    public float reloadTime;

    public AudioSource shotAudio;
    public AudioSource reloadAudio;


    [HideInInspector] public float reloadCounter;
    [HideInInspector] public int ammoPool;
    private float semiCounter;
    private int counter;

    public void Awake()
    {
        
        semiCounter = fireRate;
        ammoPool = maxAmmo;
        reloadCounter = reloadTime;
        counter = 0;
    }

    public void Fire()
    {
        if (isFullAuto)
        {            
            if (counter % fireRate == 0 && ammoPool > 0)
            {
                
                
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
            counter++;

        }
        if (!isFullAuto)
        {

            if (semiCounter <= 0 && ammoPool > 0)
            {
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
        
        if (semiCounter > 0)
        {
            semiCounter -= Time.deltaTime;
        }
        Debug.Log(ammoPool);
        if (ammoPool == 0)
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (reloadCounter == reloadTime)
        {
            reloadAudio.Play();
        }

        reloadCounter -= Time.deltaTime;

        if (reloadCounter <= 0)
        {
            ammoPool = maxAmmo;
            reloadCounter = reloadTime;
        }
    }

    public void ForceReload()
    {
        ammoPool = 0;
    }
}




