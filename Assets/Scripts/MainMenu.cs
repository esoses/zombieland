using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private int highestScoreShow;
    public TextMeshProUGUI maxDisplay;

    private void Start()
    {
        


        highestScoreShow = PlayerPrefs.GetInt("Highest Score");
        

        if (highestScoreShow / 15 > 0)
        {
            maxDisplay.text = "Highest Wave: " + (highestScoreShow / 15).ToString("f0") + "x " + (highestScoreShow % 15);
        }
        else
        {
            maxDisplay.text = "Highest Wave: " + highestScoreShow;
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Screen.fullScreen == true)
            {
                Screen.fullScreen = false;
            }
            else
            {
                Screen.fullScreen = true;
            }
        }
    }



    public void PlayGame()
    {       
        SceneManager.LoadScene("Pre Game Menu");
    }
    public void QuitGame()
    {

        
        Application.Quit();
    }
}
