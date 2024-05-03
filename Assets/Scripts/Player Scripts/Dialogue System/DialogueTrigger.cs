using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Yarn.Unity;
public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool IsDialogueActive = false;
    //Note: The pseudocode uses nodenames with "-" hyphens in them, but the actual dialogue nodes do not have any hyphens.

    //Yarn Spinner's Dialogue runner. This runs the dialogue.
    private DialogueRunner dialogueRunner;

    //Runs the ending slideshow
    EndingSlideshow slideshow;

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
    public GameManager KnowBartBitHuman;
    public GameManager KnowBartDislikesHumans;
    //UNOFFICIAL flags; Not on board.
    public GameManager BTTalkPreBody;
    public GameManager BTTalkPostBody;
    public GameManager BTTalkOEPhoto;
    public GameManager BTTalkBartBitHuman;
    // ---- Wraithwood Clues
    //Player finds out front door is locked IF they try to open the front door. (Interact with front door.) Technically a verbal clue, but has a physical object source.
    public GameManager KnowFrontDoorLocked;
    public GameManager KnowWraithwoodIsGhost;
    public GameManager KnowWraithwoodIsRoomBound;
    //UNOFFICIAL flag; Doesn't go on evidence board.
    public GameManager MRTalkSick;
    public GameManager MRTalkFrontDoor;
    public GameManager MRTalkBody;
    public GameManager MRTalkSpellbook;
    public GameManager MRTalkOEPhoto;
    // ---- Olivia Clues
    public GameManager KnowOliviaKilled;
    // UNOFFICIAL verbal clue; Doesn't go on the evidence board.
    public GameManager KnowOliviaMarried;
    public GameManager KnowOliviaWidow;
    public GameManager KnowOliviaWitch;
    public GameManager KnowOliviaNecromancer;
    public GameManager KnowOliviaRecentlyJoined;
    // ---- Max Clues
    public GameManager KnowMaxRejectedByOlivia;
    public GameManager KnowMaxSeenWithBlood;
    //UNOFFICIAL flags; Doesn't go on the evidence board.
    public GameManager MXTalkPreFindBody;
    public GameManager MXTalkedAboutMeeting;
    public GameManager MXKnowsOliviaDead;
    public GameManager MXTalkBloodied;
    public GameManager MXTalkSick;
    // ---- Edmund Clues
    public GameManager KnowEdmund_Want_UndoUndead;
    public GameManager KnowEdmund_Hate_BeingUndead;
    //UNOFFICIAL flags; Doesn't go on evidence board.
    public GameManager EDTalkPreBody;
    public GameManager EDTalkPostBody;
    public GameManager EDTalkOliviaWitch;
    public GameManager EDTalkOliviaNecromancer;
    public GameManager EDTalkOEPhoto;
    // ---- Minerva Clues
    public GameManager KnowHasPoison;
    public GameManager KnowMinervaDislikesOlivia;
    //UNOFFICIAL flags; Not on evidence board.
    public GameManager MNTalkPreBody;
    public GameManager MNTalkPostBody;
    public GameManager MNTalkPlayerSick;

    // ------------------------------ Physical Clues ------------------- //
    public GameManager FindBody;
    public GameManager HasOliviaEdmundPhoto;
    public GameManager HasSpellbook;
    public GameManager HasOliviaCup;
    public GameManager HasBloodyPen;
    public GameManager HasHexBag;


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
            targetNodeName = "Ending";

            slideshow.startSlideshow();
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;

        // If the currently running node is different from the target node (nodeName), Stop running the current node and dialogue.
            if (dialogueRunner.CurrentNodeName != targetNodeName)
            {
                dialogueRunner.Stop();
            }
            // Start running the target dialogue node.
            dialogueRunner.StartDialogue(targetNodeName);
        }
    }

    public void RunTargetNode()
    {
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

    //This function goes through all of the triggers and conditions to fetch the right dialogue node for Characters.
    public void TalkToCharacter()
    {
        Debug.Log("Player character dialogue");
        IsDialogueActive = true;
        // Set the global variable "currentCharacter" to the currently selected character. "selectedCharacter" is set in the Unity hierarchy, as it is a Serialized Field.
        currentCharacter.currentCharacter = selectedCharacter;
        //Run code based on which character is currently being spoken to.
        switch(currentCharacter.currentCharacter)
        {
            //If currently selected character is Bartholomew. [BT], "Bartholomew".
            case "Bartholomew":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [BT-01] Default. Before finding the body. [FindBody == False, BTTalkPreBody == false] [Set KnowMaxRejectedByOlivia = True, BTTalkPreBody = true]
                if (FindBody.FindBody == false & BTTalkPreBody.BTTalkPreBody == false)
                {
                    targetNodeName = "BT01";
                    RunTargetNode();
                    KnowMaxRejectedByOlivia.KnowMaxRejectedByOlivia = true;
                    BTTalkPreBody.BTTalkPreBody = true;
                }
                /// - [BT04BD] - After finding Olivia’s body. [FindBody == True, BTTalkPostBody == false] [Set BTTalkPostBody = true, KnowBartDislikesHumans = True, KnowMaxSeenWithBlood = True, KnowMaxRejectedByOlivia = true]
                else if (FindBody.FindBody == true & BTTalkPostBody.BTTalkPostBody == false)
                {
                    targetNodeName = "BT04BD";
                    RunTargetNode();
                    BTTalkPostBody.BTTalkPostBody = true;
                    KnowBartDislikesHumans.KnowBartDislikesHumans = true;
                    KnowMaxSeenWithBlood.KnowMaxSeenWithBlood = true;
                    KnowMaxRejectedByOlivia.KnowMaxRejectedByOlivia = true;
                }
                /// - [BT-06-OE] - After finding ‘Olivia & Edmund photo’. [HasOliviaEdmundPhoto == True, BTTalkOEPhoto == false] [Set BTTalkOEPhoto = true]
                else if (HasOliviaEdmundPhoto.HasOliviaEdmundPhoto == true & BTTalkOEPhoto.BTTalkOEPhoto == false)
                {
                    targetNodeName = "BT06OE";
                    RunTargetNode();
                    BTTalkOEPhoto.BTTalkOEPhoto = true;
                }
                /// - [BT-07-MX] - Know Bart has bitten a human before (heard from Max). [KnowBartBitHuman == True, BTTalkBartBitHuman == false] [Set BTTalkBartBitHuman = true]
                else if (KnowBartBitHuman.KnowBartBitHuman == true & BTTalkBartBitHuman.BTTalkBartBitHuman == false)
                {
                    targetNodeName = "BT07MX";
                    RunTargetNode();
                    BTTalkBartBitHuman.BTTalkBartBitHuman = true;
                }
                /// 
                ///--- Questioning Dialogue: BTStartConvo ---//
                ///* All of the dialogue options asking questions about diff characters are nested in BTStartConvo.
                ///* Jumps to BTEndConvo.
                else
                {
                    targetNodeName = "BTStartConvo";
                    RunTargetNode();
                }
                break;
            //If currently selected character is Mr. Wraithwood, over the phone. [MR], "Wraithwood".
            case "Wraithwood_Phone":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MRSick] - Has added >= 10 clues to the board and is thus sick. [PlayerIsSick == true & MRTalkSick == false] [Set MRTalkSick = true]
                if (PlayerIsSick.PlayerIsSick == true & MRTalkSick.MRTalkSick == false)
                {
                    targetNodeName = "MRSick";
                    RunTargetNode();
                    MRTalkSick.MRTalkSick = true;
                }
                /// - [MR-02-FD] - After finding the front door to be locked. [KnowFrontDoorLocked == True, MRTalkFrontDoor == false] [set MRTalkFrontDoor = true]
                else if (KnowFrontDoorLocked.KnowFrontDoorLocked == true & MRTalkFrontDoor.MRTalkFrontDoor == false)
                {
                    targetNodeName = "MR02FD";
                    RunTargetNode();
                    MRTalkFrontDoor.MRTalkFrontDoor = true;
                }
                /// - [MR-04-BD] - After finding the body. [FindBody == True, MRTalkBody == false] [Set MRTalkBody = true]
                else if (FindBody.FindBody == true & MRTalkBody.MRTalkBody == false)
                {
                    targetNodeName = "MR04BD";
                    RunTargetNode();
                    MRTalkBody.MRTalkBody = true;
                }
                /// - [MR-05-SB] - After finding spellbook. [HasSpellbook == True, MRTalkSpellbook == false] [Set MRTalkSpellbook = true]
                else if (HasSpellbook.HasSpellbook == true & MRTalkSpellbook.MRTalkSpellbook == false)
                {
                    targetNodeName = "MR05SB";
                    RunTargetNode();
                    MRTalkSpellbook.MRTalkSpellbook = true;
                }
                /// - [MR-06-OE] - After being told Olivia said she was married or finding ‘O + E photo’. [MRTalkOEPhoto == false & (HasOliviaEdmundPhoto == True || KnowOliviaMarried == True)] [Set MRTalkOEPhoto = true]
                else if (MRTalkOEPhoto.MRTalkOEPhoto == false & (HasOliviaEdmundPhoto.HasOliviaEdmundPhoto == true | KnowOliviaMarried.KnowOliviaMarried == true))
                {
                    targetNodeName = "MR06OE";
                    RunTargetNode();
                    MRTalkOEPhoto.MRTalkOEPhoto = true;
                }
                ///--- Questioning Dialogue: MRPStartConvo ---//
                else
                {
                    targetNodeName = "MRPStartConvo";
                    RunTargetNode();
                }
                break;
            case "Wraithwood":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [MRSick] - Has added >= 10 clues to the board and is thus sick. [PlayerIsSick == true & MRTalkSick == false] [Set MRTalkSick = true]
                if (PlayerIsSick.PlayerIsSick == true & MRTalkSick.MRTalkSick == false)
                {
                    targetNodeName = "MRSick";
                    RunTargetNode();
                    MRTalkSick.MRTalkSick = true;
                }
                /// - [MR-17-FD] - After finding the front door to be locked. [KnowFrontDoorLocked == True, MRTalkFrontDoor == false] [set MRTalkFrontDoor = true]
                else if (KnowFrontDoorLocked.KnowFrontDoorLocked == true & MRTalkFrontDoor.MRTalkFrontDoor == false)
                {
                    targetNodeName = "MR17FD";
                    RunTargetNode();
                    MRTalkFrontDoor.MRTalkFrontDoor = true;
                }
                /// - [MR-14-BD] - After finding the body. [FindBody == True, MRTalkBody == false] [Set MRTalkBody = true]
                else if (FindBody.FindBody == true & MRTalkBody.MRTalkBody == false)
                {
                    targetNodeName = "MR14BD";
                    RunTargetNode();
                    MRTalkBody.MRTalkBody = true;
                }
                /// - [MR-15-SB] - After finding spellbook. [HasSpellbook == True, MRTalkSpellbook == false] [Set MRTalkSpellbook = true]
                else if (HasSpellbook.HasSpellbook == true & MRTalkSpellbook.MRTalkSpellbook == false)
                {
                    targetNodeName = "MR15SB";
                    RunTargetNode();
                    MRTalkSpellbook.MRTalkSpellbook = true;
                }
                /// - [MR-16-OE] - After being told Olivia said she was married or finding ‘O + E photo’. [MRTalkOEPhoto == false & (HasOliviaEdmundPhoto == True || KnowOliviaMarried == True)] [Set KnowOliviaWidow = true, MRTalkOEPhoto = true]
                else if (MRTalkOEPhoto.MRTalkOEPhoto == false & (HasOliviaEdmundPhoto.HasOliviaEdmundPhoto == true | KnowOliviaMarried.KnowOliviaMarried == true))
                {
                    targetNodeName = "MR16OE";
                    RunTargetNode();
                    KnowOliviaWidow.KnowOliviaWidow = true;
                    MRTalkOEPhoto.MRTalkOEPhoto = true;
                }
                ///--- Start Questioning Dialogue: MRStartConvo ---//
                ///*Dialogue options: [MRStartConvo] - MRStartConvo includes these options, and jumps to MR08 or MR09 depending on the option chosen. [Set KnowWraithwoodIsGhost = true]
                /// * About Mr.Wraithwood [MR08]
                /// * About Others [MR09]
                else
                {
                    targetNodeName = "MRStartConvo";
                    RunTargetNode();
                }
                break;
            //If currently selected character is Edmund. [ED], "Edmund"
            case "Edmund":
                ///*Dialogue w/ NO options -- PRIORITY OVER Questioning Dialogue:
                /// - [ED-01-OL] Default. Before finding the body. [FindBody == False, EDTalkPreBody == false] [Set EDTalkPreBody = true]
                if (FindBody.FindBody == false & EDTalkPreBody.EDTalkPreBody == false)
                {
                    targetNodeName = "ED01OL";
                    RunTargetNode();
                    EDTalkPreBody.EDTalkPreBody = true;
                }
                /// - [ED-02-BD] - After finding her body. [FindBody == True, EDTalkPostBody == false] [set EDTalkPostBody = true]
                else if (FindBody.FindBody == true & EDTalkPostBody.EDTalkPostBody == false)
                {
                    targetNodeName = "ED02BD";
                    RunTargetNode();
                    EDTalkPostBody.EDTalkPostBody = true;
                }
                /// - [ED-06-MN-MR] - Olivia Was a Witch. [KnowOliviaWitch == True, EDTalkOliviaWitch == false] [Set EDTalkOliviaWitch = true]
                else if (KnowOliviaWitch.KnowOliviaWitch == true & EDTalkOliviaWitch.EDTalkOliviaWitch == false)
                {
                    targetNodeName = "ED06MNMR";
                    RunTargetNode();
                    EDTalkOliviaWitch.EDTalkOliviaWitch = true;
                }
                /// - [ED-07-MN] - Olivia was a Necromancer. [KnowOliviaNecromancer == True, EDTalkOliviaNecromancer == false] [Set EDTalkOliviaNecromancer = true]
                else if (KnowOliviaNecromancer.KnowOliviaNecromancer == true & EDTalkOliviaNecromancer.EDTalkOliviaNecromancer == false)
                {
                    targetNodeName = "ED07MN";
                    RunTargetNode();
                    EDTalkOliviaNecromancer.EDTalkOliviaNecromancer = true;
                }
                /// - [ED-08-OE] - Show him the picture of Olivia & Edmund [HasOliviaEdmundPhoto == True, EDTalkOEPhoto == false] [Set EDTalkOEPhoto = true]
                else if (HasOliviaEdmundPhoto.HasOliviaEdmundPhoto == true & EDTalkOEPhoto.EDTalkOEPhoto == false)
                {
                    targetNodeName = "ED08OE";
                    RunTargetNode();
                    EDTalkOEPhoto.EDTalkOEPhoto = true;
                }
                /// 
                ///--- Start Questioning Dialogue: EDStartConvo ---//
                else
                { 
                    //***CURRENTLY: ALL NESTED DIALOGUE OPTION VARIABLES (FLAGS) ARE NOT FUNCTIONAL YET. NEEDS VARIABLE STORAGE MANAGEMENT SYSTEM FIRST.
                    //***CURRENTLY: ALL DIALOGUE OPTIONS ARE ALSO NOT FUNCTIONAL YET. MNStartConvo does NOT run all of the following dialogue options listed below.
                    targetNodeName = "EDStartConvo";
                    RunTargetNode();
                }
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
                if (FindBody.FindBody == false & MNTalkPreBody == false)
                {
                    targetNodeName = "MN01OL";
                    RunTargetNode();
                    MNTalkPreBody.MNTalkPreBody = true;
                }
                /// - [MN-02-BD] - Approaching her after finding the body for the first time [FindBody == true, MNTalkPostBody == false] [Set MNTalkPostBody = true]
                else if (FindBody.FindBody == true & MNTalkPostBody.MNTalkPostBody == false)
                {
                    targetNodeName = "MN02BD";
                    RunTargetNode();
                    MNTalkPostBody.MNTalkPostBody = true;
                }
                /// - [MN-14-PSN] - Has collected >= 10 clues, and PlayerIsSick. [PlayerIsSick == true, MNTalkPlayerSick == false] [set MNTalkPlayerSick = true]
                else if (PlayerIsSick.PlayerIsSick == true & MNTalkPlayerSick == false)
                {
                    targetNodeName = "MN14PSN";
                    RunTargetNode();
                    MNTalkPlayerSick.MNTalkPlayerSick = true;
                }
                ///   [MN-15-CP] - Has Olivia's Cup, Has collected >= 10 clues, and PlayerIsSick. [HasOliviaCup == True, PlayerIsSick == true, KnowPlayerIsPoisoned == false] [Set KnowPlayerIsPoisoned == true]
                else if (HasOliviaCup.HasOliviaCup == true & PlayerIsSick.PlayerIsSick == true & KnowPlayerIsPoisoned.KnowPlayerIsPoisoned == false)
                {
                    targetNodeName = "MN15CP";
                    RunTargetNode();
                    KnowPlayerIsPoisoned.KnowPlayerIsPoisoned = true;
                }
                ///   
                ///--- Start Questioning Dialogue: [MNStartConvo] ---//
                else
                {
                    //***CURRENTLY: ALL NESTED DIALOGUE OPTION VARIABLES (FLAGS) ARE NOT FUNCTIONAL YET. NEEDS VARIABLE STORAGE MANAGEMENT SYSTEM FIRST.
                    //***CURRENTLY: ALL DIALOGUE OPTIONS ARE ALSO NOT FUNCTIONAL YET. MNStartConvo does NOT run all of the following dialogue options listed below.
                    targetNodeName = "MNStartConvo";
                    RunTargetNode();
                }
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
                /// - [MX-01-OL] - Asking about Olivia before the murder. [MXTalkPreFindBody == false, FindBody == false] [Set MXTalkPreFindBody = true, KnowOliviaMarried = true, MXTalkedAboutMeeting = true] 
                ///  * Merged with [MX-02].
                if (MXTalkPreFindBody.MXTalkPreFindBody == false & FindBody.FindBody == false)
                {
                    targetNodeName = "MX01OL";
                    RunTargetNode();
                    MXTalkPreFindBody.MXTalkPreFindBody = true;
                    KnowOliviaMarried.KnowOliviaMarried = true;
                    MXTalkedAboutMeeting.MXTalkedAboutMeeting = true;
                }
                /// - [MX-03-BD] - About the meeting, if Max has been told that Olivia is dead. [MXKnowsOliviaDead == true] [Set MXTalkedAboutMeeting = true.]
                else if (MXKnowsOliviaDead.MXKnowsOliviaDead == true)
                {
                    targetNodeName = "MX03BD";
                    RunTargetNode();
                    MXTalkedAboutMeeting.MXTalkedAboutMeeting = true;
                }
                /// - [MX-13-BL] - Ask about Max being seen all bloodied. [MaxTalkedAboutMeeting == true, KnowMaxRejectedByOlivia == true, KnowMaxSeenWithBlood == true, MXTalkBloodied == false] [set MXTalkBloodied = true]
                else if (MXTalkedAboutMeeting.MXTalkedAboutMeeting == true & KnowMaxRejectedByOlivia.KnowMaxRejectedByOlivia == true & KnowMaxSeenWithBlood.KnowMaxSeenWithBlood == true & MXTalkBloodied == false)
                {
                    targetNodeName = "MX13BL";
                    RunTargetNode();
                    MXTalkBloodied.MXTalkBloodied = true;
                }
                /// - [MX-08-OL] - Tell Max about the murder. [FindBody == true, MXKnowsOliviaDead == false] [Set MXKnowsOliviaDead = true]
                else if (FindBody.FindBody == true & MXKnowsOliviaDead == false)
                {
                    targetNodeName = "MX08OL";
                    RunTargetNode();
                    MXKnowsOliviaDead.MXKnowsOliviaDead = true;
                }
                /// - [MX-06-PSN] - If player has added 13 clues to the board, and has not found out cause of being sick. [PlayerIsSick == true, KnowPlayerIsPoisoned == false, MXTalkSick == false] [set MXTalkSick = true]
                else if (PlayerIsSick.PlayerIsSick == true & KnowPlayerIsPoisoned.KnowPlayerIsPoisoned == false & MXTalkSick.MXTalkSick == false)
                {
                    targetNodeName = "MX06PSN";
                    RunTargetNode();
                    MXTalkSick.MXTalkSick = true;
                }
                ///--- Start Questioning Dialogue: [MXStartConvo] ---//
                else
                {
                    //***CURRENTLY: ALL NESTED DIALOGUE OPTION VARIABLES (FLAGS) ARE NOT FUNCTIONAL YET. NEEDS VARIABLE STORAGE MANAGEMENT SYSTEM FIRST.
                    //***CURRENTLY: ALL DIALOGUE OPTIONS ARE ALSO NOT FUNCTIONAL YET. MNStartConvo does NOT run all of the following dialogue options listed below.
                    targetNodeName = "MXStartConvo";
                    RunTargetNode();
                }
                ///*Dialogue Options:
                /// - [MX-09-BT] - Ask about Bartholomew [MaxKnowsOliviaDead == true] [set KnowBartBitHuman = true]
                /// - [MX-10-MN] - Ask about Minerva
                /// - [MX-11-MR] - Ask about Wraithwood
                /// - [MX-12-ED] - Ask about Edmund
                ///--- End Questioning Dialogue: [MXEndConvo] ---//
                break;
        }
        //------ End of Character Trigger Filtering; targetNodeName is Set accordingly. Target dialogue nodes are run within each switch case itself upon successful trigger. -------//
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