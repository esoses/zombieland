using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class treeMenager : MonoBehaviour
{
    //multiplayers:

    private float damageMulti = 1; //1
    private float healthMulti = 1; //2 
    private float moveSpeedMulti = 1; //3
    private float reloadMulti = 1; //4
    private float penaltuUnMulti = 1; //5
    private float moneyMulti = 1; //6
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GetUpgrades();
    }

    private void GetUpgrades()
    {
        for (int i = 1; i < 7; i++)
        {
            PlayerPrefs.GetFloat("multi" + i);
        }
        
    }
    public void UpgradeStat(int i, float amount)
    {
        if (i == 1)
        {
            damageMulti += amount;
            PlayerPrefs.SetFloat("multi1", damageMulti);

        }
        if (i == 2)
        {
            healthMulti += amount;
            PlayerPrefs.SetFloat("multi2", healthMulti);
        }
        if (i == 3)
        {
            moveSpeedMulti += amount;
            PlayerPrefs.SetFloat("mult3", moveSpeedMulti);
        }
        if (i == 4)
        {
            reloadMulti -= amount;
            PlayerPrefs.SetFloat("multi4", reloadMulti);
        }
        if (i == 5)
        {
            penaltuUnMulti += amount;
            PlayerPrefs.SetFloat("multi5", penaltuUnMulti);
        }
        if (i == 6)
        {
            moneyMulti += amount;
            PlayerPrefs.SetFloat("multi6", moneyMulti);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Pre Game Menu");
    }
        
}
