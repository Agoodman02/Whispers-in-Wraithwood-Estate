using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void BackButton()
    {
        Debug.Log("Button Clicked");
        SceneManager.LoadScene("MainMenu");
    }
}
