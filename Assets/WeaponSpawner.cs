using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] weapons;

    void Awake()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            Instantiate(weapons[i], gameObject.transform);
        }
    } 

    
}
