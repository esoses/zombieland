using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    public static Bank sharedInstace = null;
    public int money;    

    void Awake()
    {
        if (sharedInstace != null && sharedInstace != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstace = this;
        }
    }

    private void Start()
    {
        
        DontDestroyOnLoad(gameObject);       
        money = GetMoney();               
    }

   

    

    public void SetMoney()
    {
        PlayerPrefs.SetInt("moneyKey", money);
    }
    private int GetMoney()
    {
        return PlayerPrefs.GetInt("moneyKey");
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            money += 10;
           
        }
        
    }
}
