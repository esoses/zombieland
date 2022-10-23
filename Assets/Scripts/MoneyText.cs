using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{

    Bank bank;
    private TextMeshProUGUI text;

    private void Start()
    {
        
        bank = Bank.sharedInstace;
        text = transform.GetComponent<TextMeshProUGUI>();
        
    }

    private void Update()
    {
        text.text = bank.money.ToString() + "$";
    }



}
