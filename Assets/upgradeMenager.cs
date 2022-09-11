using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class upgradeMenager : MonoBehaviour
{

    private Bank bank;

    public Weapon[] weapons;
    public Weapon selectedWeapon;
    private int currentSelInt = 0;
   
    public TextMeshProUGUI[] upNames;
    public TextMeshProUGUI[] currentValues;
    public TextMeshProUGUI[] newValues;
    public TextMeshProUGUI[] costs;

    //private int updatedWeaponNumber;

    void UpdateShowedStats(Weapon updatedWeapon)
    {

        

        for (int i = 0; i < updatedWeapon.upgrades.Length; i++)
        {
            
            Weapon.Upgrade u = updatedWeapon.upgrades[i];
            PlayerPrefs.GetInt("uplvl" + updatedWeapon + i);
            PlayerPrefs.SetInt("uplvl" + updatedWeapon + i, u.upgradeLevel);

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
        
        selectedWeapon = weapons[currentSelInt];
        GetUpLevels();
        bank = Bank.sharedInstace;        
        UpdateShowedStats(selectedWeapon);
    }

    public void UpgradeStat(int abc)
    {
        Weapon.Upgrade u = selectedWeapon.upgrades[abc];
        int nextLevel = u.upgradeLevel + 1 >= u.upgradeStates.Length ? u.upgradeStates.Length - 1 : u.upgradeLevel + 1;
        if (u.upgradeLevel < u.upgradeStates.Length - 1)
        {
            if (bank.money >= u.upgradeCosts[nextLevel])
            {
                u.upgradeLevel += 1;
                bank.money -= u.upgradeCosts[nextLevel];

                UpdateShowedStats(selectedWeapon);
               
                bank.SetMoney();
                
            }
            else
            {
                UpdateShowedStats(selectedWeapon);
                Debug.Log("Not enough money");
            }
        }
        else
        {
            UpdateShowedStats(selectedWeapon);
            Debug.Log("Maxed out");
        }
    }

    public void ResetStats()
    {
        for (int i = 0; i < selectedWeapon.upgrades.Length; i++)
        {
            weapons[0].upgrades[i].upgradeLevel = 0;
        }
        UpdateShowedStats(selectedWeapon);
    } 

    public void ChangeWeapon(int isToRight01)
    {
        
        if (isToRight01 == 1 && currentSelInt + 1 != weapons.Length)
        {
            currentSelInt += 1;
            
        }
        if (isToRight01 == 0 && currentSelInt - 1 != -1)
        {
            currentSelInt -= 1;
            
        }

        selectedWeapon = weapons[currentSelInt];
        UpdateShowedStats(selectedWeapon);
        
    }

    public void Update()
    {
       
        
    }

}
