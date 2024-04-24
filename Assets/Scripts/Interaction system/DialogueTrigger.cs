using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yarn.Unity;
public class DialogueTrigger : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    //Note: The pseudocode uses nodenames with "-" hyphens in them, but the actual dialogue nodes do not have any hyphens.

>>>>>>> Stashed changes
    //Yarn Spinner's Dialogue runner. This runs the dialogue.
    private DialogueRunner dialogueRunner;

    //Attach this DialogueTrigger script to every Character.
    //currentCharacter should be used to store the name of the currently targeted character. 
    //Since this is attached per character (can also be used for clues) that you interact with for dialogue, you can manually input the name of the Character/Object this is attached to in the Unity interface, through this Serialized Field.
    [SerializeField] string currentCharacter;

    //Exact names used for currentCharacter: (Make sure you're inputting these correctly)
    // Minerva, Edmund, Max, Wraithwood_Phone, Wraithwood, Bartholomew

    //This is the variable storing the name of the target dialogue node. This tells the game (& yarn spinner) what dialogue node to run.
    public string targetNodeName;

    // Clues & Variables. These reference global variables that should be stored in the GameManager, and should be set to True/False in the relevant scripts.
    // Physical clues should be set to True once they have been picked up. (Object Interaction script; Once picked up, set to True.)
    // Verbal clues should be set to True once they have been "obtained" through dialogue. (Either Yarn Spinner script, or DialogueTrigger script. Once actiavated, set to True.)

    /// Placeholder Variables for Clues/Flags
    // ------------------------------ Verbal Clues --------------------- //
    // ---- Player or Misc. Clues ----//
    public bool KnowPlayerIsPoisoned = false;
    // ---- Bart Clues
    public bool KnowBartBitHuman = false;
    public bool KnowBartDislikesHumans = false;
    // ---- Wraithwood Clues?
    //Player finds out front door is locked IF they try to open the front door. (Interact with front door.) Technically a verbal clue, but has a physical object source.
    public bool KnowFrontDoorLocked = false;
    public bool WraithwoodIsGhost = false;
    // ---- Olivia Clues
    public bool KnowOliviaKilled = false;
    // UNOFFICIAL verbal clue; Doesn't go on the evidence board.
    public bool KnowOliviaMarried = false;
    public bool KnowOliviaWidow = false;
    public bool KnowOliviaWitch = false;
    public bool KnowOliviaNecromancer = false;
    public bool KnowOliviaRecentlyJoined = false;
    // ---- Max Clues
    public bool KnowMaxRejectedByOlivia = false;
    public bool KnowMaxSeenWithBlood = false;
    //UNOFFICIAL verbal clue/flag; Doesn't go on the evidence board.
    public bool MaxTalkedAboutMeeting = false;
    //UNOFFICIAL verbal clue/flag; Doesn't go on the evidence board.
    public bool MaxKnowsOliviaDead = false;
    //UNOFFICIAL flag; Doesn't go on the evidence board.
    public bool MaxTalkPreFindBody = false;
    // ---- Edmund Clues
    public bool KnowEdmund_Want_UndoUndead = false;
    public bool KnowEdmund_Hate_BeingUndead = false;
    // ---- Minerva Clues
    public bool KnowHasPoison = false;
    public bool KnowMinervaDislikesOlivia = false;

    // ------------------------------ Physical Clues ------------------- //
    public bool FindBody = false;
    public bool HasOliviaEdmundPhoto = false;
    public bool HasSpellbook = false;
    public bool HasOliviaCup = false;
    public bool HasBloodyPen = false;
    public bool HasHexBag = false;


    // ------------------------------ General Variables ---------------- //
    //There are a total of 21 clues.
    // 33% of total clues = 7
    // 60% of total clues = 13
    // 3 clues remaining out of total clues = 18
    public int CluesObtained = 0;
    public int CluesOnBoard = 0;


    //This function checks if the player has acquired 100% of the clues, and then triggers the final cutscene's dialogue node.
    public void TriggerFinalCutscene()
    {
        ///* Check if all of the clues are added to the evidence board.
    
        if (CluesOnBoard == 21) 
        {
            targetNodeName = "Insert Final Cutscene NodeName Here";

        // If the currently running node is different from the target node (nodeName), Stop running the current node and dialogue.
            if (dialogueRunner.CurrentNodeName != targetNodeName)
            {
                dialogueRunner.Stop();
            }
            // Start running the target dialogue node.
            dialogueRunner.StartDialogue(targetNodeName);
        }
    }

    //This function goes through all of the triggers and conditions to fetch the right dialogue node for Characters.
    public void TalkToCharacter()
    {
        //Run code based on which character is currently being spoken to.
        switch(currentCharacter)
        {
            //If currently selected character is Bartholomew. [BT], "Bartholomew".
            case "Bartholomew":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [BT-01] Default. Before finding the body. [FindBody == False] [Set KnowMaxRejectedByOlivia = True]
                /// - [BT-02] Default Repeat dialogue after [BT-01] has been run.
                /// - [BT-03-BD] - After finding Olivia’s body. [FindBody == True]
                /// - [BT-04-OL] - Olivia's Been Killed [KnowOliviaKilled == True] [Set KnowBartDislikesHumans = True]
                /// - [BT-05-OL] - Continues after [BT-05-OL]. [Set KnowMaxSeenWithBlood = True]. Might just merge these two into one dialogue node instead of playing them back-to-back.
                /// - [BT-06-OE] - After finding ‘Olivia & Edmund photo’. [HasOliviaEdmundPhoto == True]
                /// - [BT-07-MX] - Know Bart has bitten a huamn before (heard from Max). [KnowBartBitHuman == True]
                /// 
                ///--- Start Questioning Dialogue: "PLAYER: So what do you know about..." ---//
                ///*Dialogue Options:
                /// - Ask about Characters
                ///  * [BT-08-ED] - Edmund
                ///  * [BT-09-MN] - Minerva
                ///  * [BT-10-MX] - Max
                break;
            //If currently selected character is Mr. Wraithwood, over the phone. [MR], "Wraithwood".
            case "Wraithwood_Phone":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MR-02-FD] - After finding the front door to be locked. [KnowFrontDoorLocked == True]
                /// - [MR-04-BD] - After finding the body. [FindBody == True]
                /// - [MR-05-SB] - After finding spellbook. [HasSpellbook == True]
                /// - [MR-06-OE] - After being told Olivia said she was married or finding ‘O + E photo’. [HasOliviaEdmundPhoto == True || KnowOliviaMarried == True]
                /// 
                ///--- Start Questioning Dialogue: "PLAYER: I've got some questions, Mr. Wraithwood..." ---//
                ///*Dialogue options:
                /// - [MR-01-OL] PLAYER: What do you know about Olivia?
                /// - [MR-03] PLAYER: What do you know of the guests here?
                /// - [MR-07] PLAYER: Why won’t you be more helpful?
                break;
            case "Wraithwood":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MR-14-BD] - After finding the body. [FindBody == True]
                /// - [MR-15-SB] - After finding spellbook. [HasSpellbook == True]
                /// - [MR-16-OE] - After being told Olivia said she was married or finding ‘O + E photo’. [HasOliviaEdmundPhoto == True || KnowOliviaMarried == True] [Set KnowOliviaWidow = true]
                /// - [MR-17-FD] - After finding the front door to be locked. [KnowFrontDoorLocked == True]
                /// 
                ///--- Start Questioning Dialogue: "PLAYER: I've got some questions, Mr. Wraithwood..." ---//
<<<<<<< Updated upstream
                ///*Dialogue options:
                /// - About Mr.Wraithwood
                ///  * [MR-08] PLAYER: What are you?
                ///  * [MR-10] PLAYER: Why won’t you be more helpful?
                ///  * [MR-11] PLAYER: What’s it like being a ghost?
                /// - About Others
=======
                ///*Dialogue options: [MR00] - MR00 includes these options, and jumps to MR08 or MR09 depending on the option chosen. *****Dialogue Trigger only needs to trigger MR00.
                /// - About Mr.Wraithwood [MR08]
                ///  * [MR-08] PLAYER: What are you?
                ///  * [MR-10] PLAYER: Why won’t you be more helpful?
                ///  * [MR-11] PLAYER: What’s it like being a ghost?
                /// - About Others [MR09]
>>>>>>> Stashed changes
                ///  * [MR-12] PLAYER: What do you know of the guests here?
                ///  * [MR-09-OL] PLAYER: What do you know about Olivia?
                ///  * [MR-13-OL] PLAYER: Did Olivia know you were a ghost?
                break;
            //If currently selected character is Edmund. [ED], "Edmund"
            case "Edmund":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [ED-01-OL] Default. Before finding the body. [FindBody == False]
                /// - [ED-02-BD] - After finding her body. [FindBody == True]
                /// - [ED-03-OL] - After revealing she’s been murdered. [KnowOliviaKilled == True]
                /// - [???????] - Olivia’s drink. [????????] (Waiting for response from narrative.)
                /// - [ED-06-MN-MR] - Olivia Was a Witch. [KnowOliviaWitch == True]
                /// - [ED-07-MN] - Olivia was a Necromancer. [KnowOliviaNecromancer == True]
                /// - [ED-08-OE] - Show him the picture of Olivia & Edmund [HasOliviaEdmundPhoto == True]
                /// 
                ///--- Start Questioning Dialogue: "PLAYER: I've got some questions, Mr. Wraithwood..." ---//
                ///*Dialogue options:
                /// - About Him
                ///  * [ED-04] - His Alibi [EdmundAskedAboutOlivia == False]
                ///  * [ED-04-OL] - His Alibi, alternate dialogue for same dialogue option. [EdmundAskedAboutOlivia == False].
                ///  * [ED-05] - On Being a Skeleton
                /// - About Others
                ///  * [ED-09-OL] - About Olivia
                ///  * [ED-10-BT] - On Bartholomew
                ///  * [ED-11-MN] - On Minerva
                ///  * [ED-13-MX] - On Max
                ///    - [ED-14-MX] - If Asked about Max's flirting. [KnowMaxRejectedByOlivia == True]
                break;
            //If currently selected character is Minerva. [MN], "Minerva".
            case "Minerva":
                ///*Starting dialogue: MNStartConvo
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MN-01-OL] - If talked to before finding the body. [FindBody == False]
                /// - [MN-02-BD] - Approaching her after finding the body for the first time
                /// - [MN-14-PSN] - Has added >= 13 clues to the board. [CluesOnBoard >= 13]
                ///   * NO Cup = Conversation Ends.
                ///   * [MN-15-CP] - Has Olivia's Cup [HasOliviaCup == True, KnowPlayerIsPoisoned == false] [Set KnowPlayerIsPoisoned == true]
                ///   
                ///--- Start Questioning Dialogue: "PLAYER: Can I ask you a few questions, Minerva?" ---//
                ///*Dialogue Options:
                /// - About Others
                ///  * [MN-03-OL] - Olivia
                ///  * [MN-04-MX] - Maxwell
                ///  * [MN-06-ED] - Edmund
                ///  * [MN-07-BT] - Bart
                ///  * [MN-13-MR] - Mr. Wraithwood
                /// - About Clues [Dialogue options are only available if the related clue has been found/obtained.]
                ///  * [MN-05] - About the Meeting
                ///  * [MN-08] - About Alibi since the Meeting
                ///  * [MN-09-HX] - Hex Bag (before asking about Spellbook) [AskedSpellbook == False]
                ///  * [MN-09-HX-SB] - Hex Bag (after asking about Spellbook) [AskedSpellBook == True]
                ///  * [MN-10-SB] - Spellbook [AskedSpellbook == True] [Set KnowOliviaWitch = True]
                ///  * [MN-11-OL] - Olivia Is Witch (After learning Olivia is a witch from Minerva) [KnowOliviaWitch == True] [Set KnowOliviaNecromancer = True]
                ///  * [MN-12-PSN] - Bottles From Minvera's Room
                ///--- End Questioning Dialogue: "PLAYER: I think that’s it… Thanks for your time." ---//
                break;
            case "Max":
                ///*Starting dialogue: MXStartConvo
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MX-01-OL] - Asking about Olivia before the murder. [MaxTalkPreFindBody == false] [Set MaxTalkPreFindBody = true]
                /// - [MX-03-BD] - About the meeting, if Max has been told that Olivia is dead. [MaxKnowsOliviaDead == true] [Set MaxTalkedAboutMeeting = true, even if it was already true.]
                /// - [MX-02] - About the meeting. [Set KnowOliviaMarried = true] [Set MaxTalkedAboutMeeting = true]
                /// - [MX-13-BL] - Ask about Max being seen all bloodied. [MaxTalkedAboutMeeting == true, KnowMaxRejectedByOlivia == true, KnowMaxSeenWithBlood == true]
                /// - [MX-08-OL] - Tell Max about the murder. [FindBody == true] [Set MaxKnowsOliviaDead = true]
                /// 
                /// - [MX-04-BL] - Ask more about Olivia after talking about the meeting, NO KnowMaxSeenWithBlood. [MaxTalkedAboutMeeting == true, KnowMaxSeenWithBlood == false]
                /// - [MX-05] - Ask more about Olivia after talking about the meeting, YES KnowMaxSeenWithBlood. [MaxTalkedAboutMeeting == true, KnowMaxSeenWithBlood == false]
                /// - [MX-06-PSN] - If player has added 13 clues to the board, and has not found out cause of being sick. [CluesAddedToBoard >= 13, KnowPlayerIsPoisoned == false]
                ///  * [MX-07-PSN] - If the player hasn’t talked to Minerva about being poisoned yet. Just merge this with MX-06-PSN tbh.
                ///  
                ///--- Start Questioning Dialogue:  "MAXWELL: Aagh! This night couldn’t get any worse… Woah! It’s you. Uh, hey! How can I help ya?" ---//
                ///*Dialogue Options:
                /// - [MX-09-BT] - Ask about Bartholomew [MaxKnowsOliviaDead == true] [set KnowBartBitHuman = true]
                /// - [MX-10-MN] - Ask about Minerva
                /// - [MX-11-MR] - Ask about Wraithwood
                /// - [MX-12-ED] - Ask about Edmund
                ///--- End Questioning Dialogue: "PLAYER: That’s all for now. Thanks for helping me out. | MAX: No problem! Come back anytime!" ---//
                break;
        }
        //------ End of Character Trigger Filtering; targetNodeName is Set accordingly. -------//
        
        //---------------- Start of Run targetNodeName Dialogue Code ---------------------//
        //If the currently running node is different from the target node (nodeName), Stop running the current node and dialogue.
        if (dialogueRunner.CurrentNodeName != targetNodeName)
        {
            dialogueRunner.Stop();
        }
        //Start running the target dialogue node.
        dialogueRunner.StartDialogue(targetNodeName);
        //---------------- End of Run targetNodeName Dialogue Code -----------------------//
    }


    // [Currently NON-FUNCTIONAL] This function can be used to run dialogue based on what object is being interacted with.
    public void TalkToObject()
    {
    // If the currently running node is different from the target node (nodeName), Stop running the current node and dialogue.
        if (dialogueRunner.CurrentNodeName != targetNodeName)
        {
            dialogueRunner.Stop();
        }
        // Start running the target dialogue node.
        dialogueRunner.StartDialogue(targetNodeName);
    }
}