//Controlls pause menu, put on GameManager
//Please note that this is from another project so it has some extra script commented out
//Some things are commented out since they are not in use at the moment

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    //public GameObject settingsMenu;
    //public GameObject controlsMenu;
    //public GameObject screenUI;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Opens Pause  menu and stops game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //Pause Menu
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        //screenUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    /*
    //Settings Menu
    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
    }
    */

    //Resume Game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        //screenUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //Start Menu
    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

    /*
    public void ControlsMenu()
    {
        controlsMenu.SetActive(true);
    }
    */
}
