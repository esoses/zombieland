using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class upgradeMenager : MonoBehaviour
{

    private Bank bank;

    public bool primary;

    public Weapon[] weapons;
    public Weapon selectedWeapon;
    private int currentSelInt = 0;

    public TextMeshProUGUI weaponName;
   
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
            PlayerPrefs.GetInt("uplvl" + primary + currentSelInt + i);
            PlayerPrefs.SetInt("uplvl" + primary + currentSelInt + i, u.upgradeLevel);
            //Debug.Log("uplvl" + primary + updatedWeapon + i);

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
                if (primary)
                {
                    weapons[weaponNumber].upgrades[upgradeNumber].upgradeLevel = PlayerPrefs.GetInt("uplvl" + primary + weaponNumber + upgradeNumber);
                    Debug.Log(PlayerPrefs.GetInt("uplvl" + primary + weaponNumber + upgradeNumber));
                }
                if (!primary)
                {
                    weapons[weaponNumber].upgrades[upgradeNumber].upgradeLevel = PlayerPrefs.GetInt("uplvl" + primary + weaponNumber + upgradeNumber);
                }
                
            }
        }
    } 

    private void Start()
    {

        if (primary)
        {
            currentSelInt = PlayerPrefs.GetInt("Primary");
        }
        else
        {
            currentSelInt = PlayerPrefs.GetInt("Secondary");
        }
        selectedWeapon = weapons[currentSelInt];
        GetUpLevels();
        bank = Bank.sharedInstace;        
        UpdateShowedStats(selectedWeapon);
        ChangeWeapon(3);
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
            selectedWeapon.upgrades[i].upgradeLevel = 0;
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

        if (primary)
        {
            PlayerPrefs.SetInt("Primary", currentSelInt);
        }
        else
        {
            PlayerPrefs.SetInt("Secondary", currentSelInt);
        }


        selectedWeapon = weapons[currentSelInt];
        UpdateShowedStats(selectedWeapon);
        weaponName.text = selectedWeapon.name;
    }

    public void Update()
    {
       
        
    }

}
