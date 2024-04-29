using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//Pretty sure I got this right. If not feel free to correct it. I did not test it.
public class TutorialReciever : MonoBehaviour
{
    public TutorialInteraction TutorialInteraction;
    public UnityEvent onTrigger;

    //public InteractableScript InteractableScript;

    public void Interacted()
    {
        Debug.Log("interact recieved by player");
        onTrigger.Invoke();
    }

    //Idk if I need this part:

    /*void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            InteractableScript.nearboard = true;
        }
        else
        {
            return;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            InteractableScript.nearboard = false;
        }
        else
        {
            return;
        }
    } */
}
