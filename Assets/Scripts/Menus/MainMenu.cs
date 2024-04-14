using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); //Game Scene
    }

    public void Credits() 
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    //Start Menu
    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
