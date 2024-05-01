using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnspinnerMouseControls : MonoBehaviour
{
    public void EnableMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableMouse() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
