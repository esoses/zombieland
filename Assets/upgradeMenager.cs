using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class upgradeMenager : MonoBehaviour
{

    private Bank bank;

    public Weapon[] weapons;

   
    public TextMeshProUGUI[] upNames;
    public TextMeshProUGUI[] currentValues;
    public TextMeshProUGUI[] newValues;
    public TextMeshProUGUI[] costs;

    private int updatedWeaponNumber;

    void UpdateShowedStats(int updatedWeaponNumber)
    {
        
        this.updatedWeaponNumber = updatedWeaponNumber;

        for (int i = 0; i < weapons[updatedWeaponNumber].upgrades.Length; i++)
        {
            
            Weapon.Upgrade u = weapons[updatedWeaponNumber].upgrades[i];
            PlayerPrefs.GetInt("uplvl" + updatedWeaponNumber + i);
            PlayerPrefs.SetInt("uplvl" + updatedWeaponNumber + i, u.upgradeLevel);

            int nextLevel = u.upgradeLevel + 1 >= u.upgradeStates.Length ? u.upgradeStates.Length - 1 : u.upgradeLevel + 1;

            upNames[i].text = u.upName;
            costs[i].text = u.upgradeCosts[nextLevel].ToString() + "$";
            
            
            
                
            currentValues[i].text = u.upgradeStates[nextLevel - 1].ToString();            
            newValues[i].text = u.upgradeStates[nextLevel].ToString();                    
                                   
        }       
    }

    private void GetUpLevels()
    {
        for (int weaponNumber = 0; weaponNumber < weapons.Length; weaponNumber++)
        {
            for (int upgradeNumber = 0; upgradeNumber < weapons[weaponNumber].upgrades.Length; upgradeNumber++)
            {
                weapons[weaponNumber].upgrades[upgradeNumber].upgradeLevel = PlayerPrefs.GetInt("uplvl" + weaponNumber + upgradeNumber);
            }
        }
    } 

    private void Start()
    {
        GetUpLevels();
        bank = Bank.sharedInstace;
        
        UpdateShowedStats(0);
    }

    public void UpgradeStat(int abc)
    {
        Weapon.Upgrade u = weapons[updatedWeaponNumber].upgrades[abc];
        int nextLevel = u.upgradeLevel + 1 >= u.upgradeStates.Length ? u.upgradeStates.Length - 1 : u.upgradeLevel + 1;
        if (u.upgradeLevel < u.upgradeStates.Length - 1)
        {
            if (bank.money >= u.upgradeCosts[nextLevel])
            {
                u.upgradeLevel += 1;
                bank.money -= u.upgradeCosts[nextLevel];

                UpdateShowedStats(updatedWeaponNumber);
               
                bank.SetMoney();
                
            }
            else
            {
                UpdateShowedStats(updatedWeaponNumber);
                Debug.Log("Not enough money");
            }
        }
        else
        {
            UpdateShowedStats(updatedWeaponNumber);
            Debug.Log("Maxed out");
        }
    }

    public void ResetStats()
    {
        for (int i = 0; i < weapons[updatedWeaponNumber].upgrades.Length; i++)
        {
            weapons[0].upgrades[i].upgradeLevel = 0;
        }
        UpdateShowedStats(updatedWeaponNumber);
    } 

    

    public void Update()
    {
       
        
    }

}
