using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
   

   private void Update() 
   {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {   
                //Debug.Log(collider);
                if(collider.TryGetComponent(out NPCInteractable npcInteractable)) //change to name of NPC
                    {
                        npcInteractable.StartConversation();
                    } 
            }
        }
   }

    //UI
   public NPCInteractable GetNPCInteractableObject() 
   {
       float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            Debug.Log(collider);
           if(collider.TryGetComponent(out NPCInteractable npcInteractable))
           {
                return npcInteractable;
           }
        }
        return null;
   }

}
