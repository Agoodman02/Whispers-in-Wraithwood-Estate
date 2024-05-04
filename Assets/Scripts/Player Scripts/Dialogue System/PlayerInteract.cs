using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;
   PlayerControler player;

   UnityEngine.Vector3 minervaCoor;
   UnityEngine.Vector3 minervaRot;
   UnityEngine.Vector3 maxwellCoor;
   UnityEngine.Vector3 maxwellRot;
   UnityEngine.Vector3 bartCoor;
   UnityEngine.Vector3 bartRot;
   UnityEngine.Vector3 edmundCoor;
   UnityEngine.Vector3 edmundRot;
   UnityEngine.Vector3 wraithwoodCoor;
   UnityEngine.Vector3 wraithwoodRot;
    UnityEngine.Vector3 wraithwoodPhoneCoor;
    UnityEngine.Vector3 wraithwoodPhoneRot;
    Rigidbody m_Rigidbody;

   public bool enterCollider= false;


   private void Start() 
   {
        //If NPCs get moved, these cords must change
        //Find cords by placing player prefab and front and copy tranform values
        minervaCoor = new UnityEngine.Vector3(-24.02899f, 0.0f, 20.2725f);
        minervaRot = new UnityEngine.Vector3(0f,0);

        maxwellCoor = new UnityEngine.Vector3(-13.09594f, 0.0f, 9.612762f);
        maxwellRot = new UnityEngine.Vector3(0f,0);

        bartCoor = new UnityEngine.Vector3(24.82226f, 0.0f, 35.75872f);
        bartRot = new UnityEngine.Vector3(0f,0);

        edmundCoor = new UnityEngine.Vector3(10.79212f, 0.006152828f, 9.04614f);
        edmundRot = new UnityEngine.Vector3(0f,0);

        wraithwoodCoor = new UnityEngine.Vector3(-25.50302f, 0f, 32.30211f);
        wraithwoodRot = new UnityEngine.Vector3(0f,0);

        wraithwoodPhoneCoor = new UnityEngine.Vector3(-1.17062f, 0f, 10.12839f);
        wraithwoodPhoneRot = new UnityEngine.Vector3(7f, 0);

        m_Rigidbody = GetComponent<Rigidbody>();
   }

   private void Awake()
   {
        player = PlayerControler.player;
        dialogueTrigger = GetComponent<DialogueTrigger>();
   }

   private void Update() 
   {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {   
                if (enterCollider == true)
                {
                    /*//Unlocks mouse
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;*/
                    if (collider.gameObject.name == "Bartholomew_Vampire") // BartMain.yarn
                        {
                            //collider.TryGetComponent<NPCInteractable>(out NPCInteractable Bartholomew_Vampire);
                            Debug.Log("Interact with Bart");
                            //player.TeleportPlayer(bartCoor, bartRot, false);
                            dialogueTrigger.TalkToCharacter();
                        } 
                    if (collider.gameObject.name == "Edmund_Skeleton") // EdmundMain.yarn
                        {
                            //collider.TryGetComponent<DialogueTrigger>(out DialogueTrigger Edmund_Skeleton);  //does this need to be renamed?
                            Debug.Log("Interact with Edmund");
                        //player.TeleportPlayer(edmundCoor, edmundRot, false);
                            dialogueTrigger.TalkToCharacter();
                    } 
                    if (collider.gameObject.name == "Max_Werewolf") // MaxwellMain.yarn
                        {
                            //collider.TryGetComponent<NPCInteractable>(out NPCInteractable Max_Werewolf);
                            Debug.Log("Interact with Maxwell");
                            //player.TeleportPlayer(maxwellCoor, maxwellRot, false);
                            dialogueTrigger.TalkToCharacter();
                    } 
                    if (collider.gameObject.name == "Minerva_Witch") //Minerva Main
                        {
                            //collider.TryGetComponent<NPCInteractable>(out NPCInteractable Minerva_Witch);
                            Debug.Log("Interact with Minerva");
                            //player.TeleportPlayer(minervaCoor, minervaRot, false);
                            dialogueTrigger.TalkToCharacter();
                    } 
                    if (collider.gameObject.name == "Mr.Wraithwood") //Wraithwood Main
                        {
                            //collider.TryGetComponent<NPCInteractable>(out NPCInteractable MrWraithwood);
                            Debug.Log("Interact with Wraithwood");
                            //player.TeleportPlayer(wraithwoodCoor, wraithwoodRot, false);
                            dialogueTrigger.TalkToCharacter();
                    }
                    if (collider.gameObject.name == "SM_Phone (Wraithwood Phone)") //Wraithwood Main
                    {
                        //collider.TryGetComponent<NPCInteractable>(out NPCInteractable MrWraithwood);
                        Debug.Log("Interact with Wraithwood Phone");
                        //player.TeleportPlayer(wraithwoodPhoneCoor, wraithwoodPhoneRot, false);
                        dialogueTrigger.TalkToCharacter();
                    }
                }
            }
            /*//player.DoCameraControl = true; //put inside if statement; bool controlled by DialogueTrigger
            if(dialogueTrigger.IsDialogueActive == false)
            {
                player.DoCameraControl = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }*/
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
   
   private void OnTriggerEnter(Collider other) 
   {
        enterCollider= true;
   }

   private void OnTriggerExit(Collider other)
   {
        enterCollider= false;
   }

}
