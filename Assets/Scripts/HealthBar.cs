using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
       
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void AddRegenHealth(float hpRegen)
    {
        switch (HeroController.hpswitcher)
        {
            case 0:
                slider.value += hpRegen * 0.01f;
                break;
            case 1:
                slider.value += hpRegen * 0.02f;
                break;
            case 2:
                slider.value += hpRegen * 0.03f;
                break;
            case 3:
                slider.value += hpRegen * 0.04f;
                break;
            default:
                break;
        }
    }
}
