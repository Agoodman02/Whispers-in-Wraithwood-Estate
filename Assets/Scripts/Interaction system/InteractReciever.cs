using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractReciever : MonoBehaviour
{
    public MonoBehaviour InteractableScript;
    //public string EventToTrigger;
    
    public UnityEvent onTrigger;

    public void Interacted()
    {
        Debug.Log("interact recieved by player");
        //InteractiableScript.Invoke(EventToTrigger, 0f);
        onTrigger.Invoke();
    }
}
