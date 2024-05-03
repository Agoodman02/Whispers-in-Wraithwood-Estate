using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource theMusic;

    //singleton instance
    public static MainMenu Instance = null;

    //initalize the singleton instance
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(theMusic);

        DontDestroyOnLoad(theMusic);
    }

    //Load game
    public void Tutorial()
    {
        SceneManager.LoadScene(1);
        Destroy(theMusic);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    //loads credits
    public void Credits() 
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }

    //loads controls
    public void Controls()
    {
        SceneManager.LoadScene("Controls", LoadSceneMode.Additive);
    }

    //Loads settings
    public void Settings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    //Closes game
    public void QuitGame() 
    {
        Application.Quit();
    }

    //Closes controls
    public void CloseControls()
    {
        SceneManager.UnloadSceneAsync("Controls");
    }

    //closes settings
    public void CloseSettings()
    {
        SceneManager.UnloadSceneAsync("Settings");
    }

    //closes credits
    public void CloseCredits()
    {
        SceneManager.UnloadSceneAsync("Credits");
    }
}
