using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOld : MonoBehaviour
{
   public void playGame()
   {
    SceneManager.LoadScene("Opening"); //Game Scene
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
