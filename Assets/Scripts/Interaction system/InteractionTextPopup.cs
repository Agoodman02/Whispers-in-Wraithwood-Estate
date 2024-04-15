using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTextPopup : MonoBehaviour
{
    [SerializeField] Image textPopup;

    private void Start()
    {
        //Makes sure things are not ugly
        DisableTextPopup();
    }

    //Shows popup and allows item to be picked up once in range
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnableTextPopup();
        }
    }

    //Hides popup and does not allow item to be picked up once in range
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DisableTextPopup();
        }
    }

    //Displays pickup text
    void EnableTextPopup()
    {
        textPopup.gameObject.SetActive(true);
    }

    //Hides pickup text
    void DisableTextPopup()
    {
        textPopup.gameObject.SetActive(false);
    }
}
