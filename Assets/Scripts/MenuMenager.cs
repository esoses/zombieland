using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMenager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("World 1");
    }
    public void LoadTree()
    {
        SceneManager.LoadScene(3);
    }
}
