using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class NPCInteractable : MonoBehaviour
{
   private DialogueRunner dialogueRunner;
   public string conversationStartNode;
   void Start()
   {
      dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
   }

  public void StartConversation() 
  {
   dialogueRunner.StartDialogue(conversationStartNode);
  }

  private void EndConversation()
  {

  }

  public void DisableConversation()
  {

  }
}
