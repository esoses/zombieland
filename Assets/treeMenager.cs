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

    public void UpgradeDamage(float amount)
    {
        damageMulti += amount;
        PlayerPrefs.SetFloat("multi1", damageMulti);

    }
    public void UpgradeHealth(float amount)
    {
        healthMulti += amount;
        PlayerPrefs.SetFloat("multi2", healthMulti);
    }
    public void UpgrademoveSpeed(float amount)
    {
        moveSpeedMulti += amount;
        PlayerPrefs.SetFloat("mult3", moveSpeedMulti);
    }
    public void UpgradeReload(float amount)
    {
        reloadMulti += amount;
        PlayerPrefs.SetFloat("multi4", reloadMulti);
    }
    public void UpgradeUnPenalty(float amount)
    {
        penaltuUnMulti += amount;
        PlayerPrefs.SetFloat("multi5", penaltuUnMulti);
    }
    public void UpgradeMoney(float amount)
    {
        moneyMulti += amount;
        PlayerPrefs.SetFloat("multi6", moneyMulti);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }
        
}
