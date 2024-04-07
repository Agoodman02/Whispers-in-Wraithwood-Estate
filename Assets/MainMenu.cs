using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void playGame()
   {
    SceneManager.LoadScene("Level_0"); //Game Scene
   }

   public void credits() 
   {
      SceneManager.LoadScene("Credits");
   }

   public void quitGame() 
   {
    Application.Quit();
   }
   
}
