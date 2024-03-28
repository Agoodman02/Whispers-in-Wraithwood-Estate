using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractReciever : MonoBehaviour
{
    public MonoBehaviour InteractiableScript;
    public string EventToTrigger;

    public void Interacted()
    {
        Debug.Log("interact recieved by player");
        InteractiableScript.Invoke(EventToTrigger, 0f);
    }
}
