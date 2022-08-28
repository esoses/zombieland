using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
    }

    public void AddRegenMana(float manaRegen)
    {        
        switch (HeroController.switcher)
        {
            case 0:
                slider.value += manaRegen * 0.03f;
                break;
            case 1:
                slider.value += manaRegen * 0.04f;
                break;
            case 2:
                slider.value += manaRegen * 0.06f;
                break;
            case 3:
                slider.value += manaRegen * 0.07f;
                break;
            default:
                break;
        }
    }
}

