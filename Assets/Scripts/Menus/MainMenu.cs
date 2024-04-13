using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive); //Game Scene
        mainMenu.SetActive(false);
    }

    public void Credits() 
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    //Start Menu
    public void StartMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }
}
