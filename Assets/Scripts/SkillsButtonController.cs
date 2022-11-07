using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsButtonController : MonoBehaviour
{
    private Animation animation;
    public treeMenager treeMenager;
    private int indexOfStat;
    [HideInInspector] public bool bought = false;
    public GameObject[] befors;
    public bool isOrUpgrade;

    public void GetIndex(int index)
    {
        indexOfStat = index;
    }
    public void Upgrade(float amount)
    {
        bool canBeBought = true;
        for (int i = 0; i < befors.Length; i++)
        {
            if (befors[i].GetComponent<SkillsButtonController>().bought == true)
            {
                if (isOrUpgrade) // nie dziala
                {
                    canBeBought = true;
                    break;
                }
            }
            else
            {
                canBeBought = false;
                break;
                
            }
        }
        if (canBeBought) // i exp
        {
            treeMenager.UpgradeStat(indexOfStat, amount);
            bought = true;
            Image im = GetComponentInChildren<Image>();
            im.color = new Color(0.5f, 0.5f, 0.5f);
            gameObject.GetComponent<Button>().enabled = false;
        }
        else
        {
            gameObject.GetComponentInChildren<Animation>().Play();
        }
    }
}
