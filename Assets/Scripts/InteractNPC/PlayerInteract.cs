using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
   PlayerControler playerControler;
   PlayerControler player;

   private void Update() 
   {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {   
                Debug.Log(collider);
                if(collider.TryGetComponent(out NPCInteractable Bartholomew_Vampire)) //Bart Main
                    {
                        //playerControler.TeleportPlayer(Vector3, Vector3,false);
                        Bartholomew_Vampire.StartConversation();
                    } 
                if(collider.TryGetComponent(out NPCInteractable Edmund_Skeleton)) //Edmund Main
                    {
                        //playerControler.TeleportPlayer(Vector3, Vector3, false);
                        Edmund_Skeleton .StartConversation();
                    } 
                if(collider.TryGetComponent(out NPCInteractable Max_Werewolf)) //Maxwell Main
                    {
                        //playerControler.TeleportPlayer(Vector3, Vector3,false);
                        Max_Werewolf.StartConversation();
                    } 
                if(collider.TryGetComponent(out NPCInteractable Minerva_Witch)) //Minerva Main
                    {
                        //playerControler.TeleportPlayer(Vector3, Vector3,false);
                        Minerva_Witch.StartConversation();
                    } 
                if(collider.TryGetComponent(out NPCInteractable MrWraithwood)) //Wraithwood Main
                    {
                        //playerControler.TeleportPlayer(Vector3, Vector3, false);
                        MrWraithwood.StartConversation();
                    } 
            }
        }
    playerControler.DoCameraControl = true;
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
