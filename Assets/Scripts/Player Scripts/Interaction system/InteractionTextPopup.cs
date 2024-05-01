using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTextPopup : MonoBehaviour
{
    [SerializeField] Image textPopup;

    InputMap actions;

    //Displays pickup text
    public void EnableTextPopup()
    {
        textPopup.gameObject.SetActive(true);
    }

    //Hides pickup text
    public void DisableTextPopup()
    {
        textPopup.gameObject.SetActive(false);
    }
}
