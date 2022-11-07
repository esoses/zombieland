using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] primaryWeapons;
    public GameObject[] secondaryWeapons;
    private int primaryWeaponNumber;
    private int secondaryWeaponNumber;


    void Awake()
    {
        primaryWeaponNumber = PlayerPrefs.GetInt("Primary");
        secondaryWeaponNumber = PlayerPrefs.GetInt("Secondary");

        
       
        Instantiate(primaryWeapons[primaryWeaponNumber], gameObject.transform);
        Instantiate(secondaryWeapons[secondaryWeaponNumber], gameObject.transform);
        Instantiate(primaryWeapons[3], gameObject.transform);

    } 

    
}
