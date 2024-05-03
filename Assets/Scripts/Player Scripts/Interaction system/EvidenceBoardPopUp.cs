using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceBoardPopUp : MonoBehaviour
{
    [SerializeField] Image textPopup;

    private void Start()
    {
        //Makes sure things are not ugly
        DisableTextPopup();
    }

    //Shows popup once in range
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("By Board");
        if (other.tag == "Player")
        {
            EnableTextPopup();
        }
    }

    //Hides popup once in range
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DisableTextPopup();
        }
    }

    //Displays popup text
    void EnableTextPopup()
    {
        textPopup.gameObject.SetActive(true);
    }

    //Hides popup text
    void DisableTextPopup()
    {
        textPopup.gameObject.SetActive(false);
    }

}
