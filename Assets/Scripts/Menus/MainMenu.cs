using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource theMusic;

    public static MainMenu instance;
    
    private void Awake()
    {
        theMusic.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1); //Game Scene
    }

    public void Credits() 
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls", LoadSceneMode.Additive);
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    //Back to start menu
    public void BackButton()
    {
        Debug.Log("Button Clicked");
        SceneManager.LoadScene("MainMenu");
    }
}
