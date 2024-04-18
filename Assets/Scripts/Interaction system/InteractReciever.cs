using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractReciever : MonoBehaviour
{
    public InteractableScript InteractableScript;
    //public string EventToTrigger;
    
    public UnityEvent onTrigger;

    public void Interacted()
    {
        Debug.Log("interact recieved by player");
        //InteractiableScript.Invoke(EventToTrigger, 0f);
        onTrigger.Invoke();
    }

    void OnTriggerEnter(Collider other)
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
    }
}
