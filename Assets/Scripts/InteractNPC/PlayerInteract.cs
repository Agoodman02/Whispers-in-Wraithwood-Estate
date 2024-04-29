using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
   
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
   Rigidbody m_Rigidbody;

   public bool enterCollider= false;


   private void Start() 
   {
        minervaCoor = new UnityEngine.Vector3(-24.49f,0.0f,23.88f);
        minervaRot = new UnityEngine.Vector3(0f,0);

        maxwellCoor = new UnityEngine.Vector3(-12.01f,0.0f,10.46f);
        maxwellRot = new UnityEngine.Vector3(0f,0);

        bartCoor = new UnityEngine.Vector3(23.84f,0.0f,33.63f);
        bartRot = new UnityEngine.Vector3(0f,0);

        edmundCoor = new UnityEngine.Vector3(0f,0f,0f);
        edmundRot = new UnityEngine.Vector3(0f,0);

        wraithwoodCoor = new UnityEngine.Vector3(0f,0f,0f);
        wraithwoodRot = new UnityEngine.Vector3(0f,0);

        m_Rigidbody = GetComponent<Rigidbody>();
   }

   private void Awake()
   {
        player = PlayerControler.player;
   }

   private void Update() 
   {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {   
                Debug.Log(collider.gameObject.name);
                if (enterCollider == true)
                {
                    //player.DoCameraControl = false;
                    //if(collider.TryGetComponent(out NPCInteractable Bartholomew_Vampire)) //Bart Main
                    if (collider.gameObject.name == "Bartholomew_Vampire") // BartMain.yarn
                        {
                            collider.TryGetComponent<NPCInteractable>(out NPCInteractable Bartholomew_Vampire);
                            Debug.Log("Interact with Bart");
                            player.TeleportPlayer(bartCoor, bartRot, false);
                            Bartholomew_Vampire.StartConversation();
                        } 
                    if (collider.gameObject.name == "Edmund_Skeleton") // EdmundMain.yarn
                        {
                            collider.TryGetComponent<NPCInteractable>(out NPCInteractable Edmund_Skeleton);
                            Debug.Log("Interact with Edmund");
                            player.TeleportPlayer(edmundCoor, edmundRot, false);
                            Edmund_Skeleton.StartConversation();
                        } 
                    if (collider.gameObject.name == "Max_Werewolf") // MaxwellMain.yarn
                        {
                            collider.TryGetComponent<NPCInteractable>(out NPCInteractable Max_Werewolf);
                            Debug.Log("Interact with Maxwell");
                            player.TeleportPlayer(maxwellCoor, maxwellRot, false);
                            Max_Werewolf.StartConversation();
                        } 
                    if (collider.gameObject.name == "Minerva_Witch") //Minerva Main
                        {
                            collider.TryGetComponent<NPCInteractable>(out NPCInteractable Minerva_Witch);
                            Debug.Log("Interact with Minerva");
                            player.TeleportPlayer(minervaCoor, minervaRot, false);
                            Minerva_Witch.StartConversation();
                        } 
                    if (collider.gameObject.name == "Mr.Wraithwood") //Wraithwood Main
                        {
                            collider.TryGetComponent<NPCInteractable>(out NPCInteractable MrWraithwood);
                            Debug.Log("Interact with Wraithwood");
                            player.TeleportPlayer(wraithwoodCoor, wraithwoodRot, false);
                            MrWraithwood.StartConversation();
                        } 
                }
            }
            player.DoCameraControl = true;
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
        Debug.Log("By character");
        enterCollider= true;
   }

   private void OnTriggerExit(Collider other)
   {
        Debug.Log("Exit character");
        enterCollider= false;
   }

}
