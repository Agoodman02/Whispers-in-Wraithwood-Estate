using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yarn.Unity;
public class DialogueTrigger : MonoBehaviour
{
    //Note: The pseudocode uses nodenames with "-" hyphens in them, but the actual dialogue nodes do not have any hyphens.

    //Yarn Spinner's Dialogue runner. This runs the dialogue.
    private DialogueRunner dialogueRunner;

    //Attach this DialogueTrigger script to every Character.
    //selectedCharacter should be used to store the name of the currently targeted character. 
    //Since this is attached per character (can also be used for clues) that you interact with for dialogue, you can manually input the name of the Character/Object this is attached to in the Unity interface, through this Serialized Field.
    [SerializeField] string selectedCharacter;
    // Global Variable version of currentCharacter. This should pull from the Game Manager, which has the GameManager.cs script attached to it as a component, and thus stores these global variables.
    public GameManager currentCharacter;

    // Exact names used for selectedCharacter & currentCharacter: (Make sure you're inputting these correctly!)
    // Minerva, Edmund, Max, Wraithwood_Phone, Wraithwood, Bartholomew

    //This is the variable storing the name of the target dialogue node. This tells the game (& yarn spinner) what dialogue node to run.
    public string targetNodeName;

    // Clues & Variables. These reference global variables that should be stored in the GameManager, and should be set to True/False in the relevant scripts.
    // Physical clues should be set to True once they have been picked up. (Object Interaction script; Once picked up, set to True.)
    // Verbal clues should be set to True once they have been "obtained" through dialogue. (Currently handled within DialogueTrigger.cs. Can be done either through Yarn Spinner scripts, or this DialogueTrigger.cs script. Once activated, set to True.)

    /// Variables for Clues/Flags. These are all booleans, except for the counter variables: CluesObtained & CluesOnBoard.
    // ------------------------------ Verbal Clues --------------------- //
    // ---- Player or Misc. Clues ----//
    public GameManager KnowPlayerIsPoisoned;
    // ---- Bart Clues
    public bool KnowBartBitHuman = false;
    public bool KnowBartDislikesHumans = false;
    // ---- Wraithwood Clues?
    //Player finds out front door is locked IF they try to open the front door. (Interact with front door.) Technically a verbal clue, but has a physical object source.
    public bool KnowFrontDoorLocked = false;
    public bool KnowWraithwoodIsGhost = false;
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
    //UNOFFICIAL flag; Doesn't go on evidence board.
    public bool EdmundTalkOliviaBody = false;
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
    // 10 CluesObtained is the trigger for PlayerIsSick.
    public GameManager CluesObtained;
    public GameManager CluesOnBoard;
    public GameManager PlayerIsSick;


    // Start is called before the first frame update.
    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    // This function should be run every time the player obtains a clue. This should run AFTER any dialogue for obtaining a clue is completed.
    public void CheckIfPlayerSick()
    {
        // Checks if the player has obtained 10+ clues AND has not yet played the PlayerIsSick dialogue.
        if (CluesObtained.CluesObtained >= 10 & PlayerIsSick.PlayerIsSick == false)
        {
            // Sets PlayerIsSick to true. This prevents the PlayerSick dialogue from running multiple times, and PlayerIsSick also used to trigger PlayerSick-related dialogue.
            PlayerIsSick.PlayerIsSick = true;
            dialogueRunner.StartDialogue("PlayerSick");
        }
    }

    //This function checks if the player has acquired 100% of the clues, and then triggers the final cutscene's dialogue node.
    public void TriggerFinalCutscene()
    {
        ///* Check if all of the clues are added to the evidence board.
    
        if (CluesOnBoard.CluesOnBoard == 21) 
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
        // Set the global variable "currentCharacter" to the currently selected character. "selectedCharacter" is set in the Unity hierarchy, as it is a Serialized Field.
        currentCharacter.currentCharacter = selectedCharacter;
        //Run code based on which character is currently being spoken to.
        switch(currentCharacter.currentCharacter)
        {
            //If currently selected character is Bartholomew. [BT], "Bartholomew".
            case "Bartholomew":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [BT-01] Default. Before finding the body. [FindBody == False, BTTalkPreBody == false] [Set KnowMaxRejectedByOlivia = True, BTTalkPreBody = true]
                /// - [BT04BD] - After finding Olivia’s body. [FindBody == True, BTTalkPostBody == false] [Set BTTalkPostBody = true, KnowBartDislikesHumans = True, KnowMaxSeenWithBlood = True, KnowMaxRejectedByOlivia = true]
                /// - [BT-06-OE] - After finding ‘Olivia & Edmund photo’. [HasOliviaEdmundPhoto == True, BTTalkOEPhoto == false] [Set BTTalkOEPhoto = true]
                /// - [BT-07-MX] - Know Bart has bitten a human before (heard from Max). [KnowBartBitHuman == True, BTTalkBartBitHuman == false] [Set BTTalkBartBitHuman = true]
                /// 
                ///--- Questioning Dialogue: BTStartConvo ---//
                ///* All of the dialogue options asking questions about diff characters are nested in BTStartConvo.
                ///* Jumps to BTEndConvo.
                break;
            //If currently selected character is Mr. Wraithwood, over the phone. [MR], "Wraithwood".
            case "Wraithwood_Phone":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MRSick] - Has added >= 13 clues to the board and is thus sick.
                /// - [MR-02-FD] - After finding the front door to be locked. [KnowFrontDoorLocked == True, MRTalkFrontDoor == false] [set MRTalkFrontDoor = true]
                /// - [MR-04-BD] - After finding the body. [FindBody == True, MRTalkBody == false] [Set MRTalkBody = true]
                /// - [MR-05-SB] - After finding spellbook. [HasSpellbook == True, MRTalkSpellbook == false] [Set MRTalkSpellbook = true]
                /// - [MR-06-OE] - After being told Olivia said she was married or finding ‘O + E photo’. [MRTalkOEPhoto == false & (HasOliviaEdmundPhoto == True || KnowOliviaMarried == True)]
                /// 
                ///--- Questioning Dialogue: MRPStartConvo ---//
                break;
            case "Wraithwood":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MRSick] - Has added >= 13 clues to the board and is thus sick.
                /// - [MR-17-FD] - After finding the front door to be locked. [KnowFrontDoorLocked == True, MRTalkFrontDoor == false] [set MRTalkFrontDoor = true]
                /// - [MR-14-BD] - After finding the body. [FindBody == True, MRTalkBody == false] [Set MRTalkBody = true]
                /// - [MR-15-SB] - After finding spellbook. [HasSpellbook == True, MRTalkSpellbook == false] [Set MRTalkSpellbook = true]
                /// - [MR-16-OE] - After being told Olivia said she was married or finding ‘O + E photo’. [MRTalkOEPhoto == false & (HasOliviaEdmundPhoto == True || KnowOliviaMarried == True)] [Set KnowOliviaWidow = true, MRTalkOEPhoto = true]
                /// 
                ///--- Start Questioning Dialogue: MRStartConvo ---//
                ///*Dialogue options: [MRStartConvo] - MRStartConvo includes these options, and jumps to MR08 or MR09 depending on the option chosen. [Set KnowWraithwoodIsGhost = true]
                /// * About Mr.Wraithwood [MR08]
                /// * About Others [MR09]
                break;
            //If currently selected character is Edmund. [ED], "Edmund"
            case "Edmund":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [ED-01-OL] Default. Before finding the body. [FindBody == False, EDTalkPreBody == false] [Set EDTalkPreBody = true]
                /// - [ED-02-BD] - After finding her body. [FindBody == True, EdmundTalkPostBody == false] [set EdmundTalkPostBody = true]
                /// - [ED-06-MN-MR] - Olivia Was a Witch. [KnowOliviaWitch == True, EDTalkOliviaWitch == false] [Set EDTalkOliviaWitch = true]
                /// - [ED-07-MN] - Olivia was a Necromancer. [KnowOliviaNecromancer == True, EDTalkOliviaNecromaner == false] [Set EDTalkOliviaNecromaner = true]
                /// - [ED-08-OE] - Show him the picture of Olivia & Edmund [HasOliviaEdmundPhoto == True, EDTalkOEPhoto == false] [Set EDTalkOEPhoto = true]
                /// 
                ///--- Start Questioning Dialogue: EDStartConvo ---//
                ///*Dialogue options:
                /// - About Him
                ///  * [ED-04] - His Alibi [EdmundAskedAboutOlivia == False]
                ///    - [ED-04-OL] - His Alibi, alternate dialogue for same dialogue option. [EdmundAskedAboutOlivia == False].
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
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MN-01-OL] - If talked to before finding the body. [FindBody == False, MNTalkPreBody == false] [Set MNTalkPreBody = true]
                /// - [MN-02-BD] - Approaching her after finding the body for the first time [FindBody == true, MNTalkPostBody == false] [Set MNTalkPostBody = true]
                /// - [MN-14-PSN] - Has collected >= 10 clues, and PlayerIsSick. [PlayerIsSick == true, MNTalkPlayerSick == false] [set MNTalkPlayerSick = true]
                ///   [MN-15-CP] - Has Olivia's Cup, Has collected >= 10 clues, and PlayerIsSick. [HasOliviaCup == True, PlayerIsSick == true, KnowPlayerIsPoisoned == false] [Set KnowPlayerIsPoisoned == true]
                ///   
                ///--- Start Questioning Dialogue: [MBStartConvo] ---//
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
                ///--- End Questioning Dialogue: [MNEndConvo] ---//
                break;
            case "Max":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MX-01-OL] - Asking about Olivia before the murder. [MaxTalkPreFindBody == false, FindBody == false] [Set MaxTalkPreFindBody = true, KnowOliviaMarried = true, MaxTalkedAboutMeeting = true] 
                ///  * Merged with [MX-02].
                /// - [MX-03-BD] - About the meeting, if Max has been told that Olivia is dead. [MaxKnowsOliviaDead == true] [Set MaxTalkedAboutMeeting = true.]
                /// - [MX-13-BL] - Ask about Max being seen all bloodied. [MaxTalkedAboutMeeting == true, KnowMaxRejectedByOlivia == true, KnowMaxSeenWithBlood == true, MXTalkBloodied == false] [set MXTalkBloodied = true]
                /// - [MX-08-OL] - Tell Max about the murder. [FindBody == true, MaxKnowsOliviaDead == false] [Set MaxKnowsOliviaDead = true]
                /// 
                /// - [MX-06-PSN] - If player has added 13 clues to the board, and has not found out cause of being sick. [PlayerIsSick == true, KnowPlayerIsPoisoned == false]
                ///  
                ///--- Start Questioning Dialogue: [MXStartConvo] ---//
                ///*Dialogue Options:
                /// - [MX-09-BT] - Ask about Bartholomew [MaxKnowsOliviaDead == true] [set KnowBartBitHuman = true]
                /// - [MX-10-MN] - Ask about Minerva
                /// - [MX-11-MR] - Ask about Wraithwood
                /// - [MX-12-ED] - Ask about Edmund
                ///--- End Questioning Dialogue: [MXEndConvo] ---//
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