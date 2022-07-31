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
    public int fireRate;
    public int bulletsPerShot;
    public float manaUsage = 0.14f;
    public AudioSource AudioSource;

    private int counter = 0;



    public void Fire()
    {
        if (counter % fireRate == 0)
        {
            if (AudioSource.isPlaying == false)
            {
                AudioSource.Play();
            }

            for (int i = 0; i < bulletsPerShot; i++)
            {
                float scatterOffset = Random.Range(-weaponRecoil, weaponRecoil);

                GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation *= Quaternion.Euler(0f, 0f, scatterOffset));
                projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

                firePoint.rotation = qnt;
            }
        }
        counter++;
    }
    
}




