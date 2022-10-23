using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyTextInGame : MonoBehaviour
{
    private Animation Animation;
    

    void Start()
    {
        Animation = gameObject.GetComponent<Animation>();
        gameObject.GetComponent<TextMeshProUGUI>().text = "";
    }

    public IEnumerator ShowGainedMoney(int gainedMoney)
    {        
        gameObject.GetComponent<TextMeshProUGUI>().text = "+" + gainedMoney.ToString() + "$";
        Animation.Play();
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<TextMeshProUGUI>().text = "";
    }    
}
