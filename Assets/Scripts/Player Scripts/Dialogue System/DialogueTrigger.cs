using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yarn.Unity;
using Yarn.Markup;
using Yarn;
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
    public GameManager MXTalkBart;
    // ---- Edmund Clues
    public GameManager KnowEdmund_Want_UndoUndead;
    public GameManager KnowEdmund_Hate_BeingUndead;
    //UNOFFICIAL flags; Doesn't go on evidence board.
    public GameManager EDTalkPreBody;
    public GameManager EDTalkPostBody;
    public GameManager EDTalkOliviaWitch;
    public GameManager EDTalkOliviaNecromancer;
    public GameManager EDTalkOEPhoto;
    public GameManager EDAskedAboutOlivia;
    public GameManager EDTalk_MaxFlirtWithEdmund;
    public GameManager KnowEdmund_AskMinervaNecromancy;
    public GameManager EDTalkMinervaNecromancy;
    public GameManager EDTalk_Max2;
    // ---- Minerva Clues
    public GameManager KnowMinervaHasPoison;
    public GameManager KnowMinervaDislikesOlivia;
    //UNOFFICIAL flags; Not on evidence board.
    public GameManager MNTalkPreBody;
    public GameManager MNTalkPostBody;
    public GameManager MNTalkPlayerSick;
    public GameManager MNTalkSpellbook;
    public GameManager MNTalkHexBag;
    public GameManager MNTalkOliviaWitch;

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

    public bool IsDialogueActive = false;
    public class Dialogue{}
    public string CurrentNodeName;

    // Start is called before the first frame update.
    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }
    
    void Update()
    {
        IsDialogueActive = dialogueRunner.IsDialogueRunning;

        // Checks & sets variables for nested dialogue options that affect verbal clues or bools, based on the current running node.
        switch(dialogueRunner.CurrentNodeName)
        {
            // Wraithwood
            case "MR08A":
                if (KnowWraithwoodIsGhost.KnowWraithwoodIsGhost == false)
                {
                    KnowWraithwoodIsGhost.KnowWraithwoodIsGhost = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                break;
            case "MR10":
                if (KnowWraithwoodIsRoomBound.KnowWraithwoodIsRoomBound == false)
                {
                    KnowWraithwoodIsRoomBound.KnowWraithwoodIsRoomBound = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                break;
            case "MR09OL":
                if (KnowOliviaRecentlyJoined.KnowOliviaRecentlyJoined == false)
                {
                    KnowOliviaRecentlyJoined.KnowOliviaRecentlyJoined = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                break;
            // Edmund
            case "ED05":
                if (KnowEdmund_Want_UndoUndead.KnowEdmund_Want_UndoUndead == false)
                {
                    KnowEdmund_Want_UndoUndead.KnowEdmund_Want_UndoUndead = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                break;
            case "ED10BT":
                if (KnowBartDislikesHumans.KnowBartDislikesHumans == false)
                {
                    KnowBartDislikesHumans.KnowBartDislikesHumans = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                break;
            case "ED13MX":
                if (EDTalk_MaxFlirtWithEdmund.EDTalk_MaxFlirtWithEdmund == false)
                {
                EDTalk_MaxFlirtWithEdmund.EDTalk_MaxFlirtWithEdmund = true;
                }
                break;
            // Minerva
            case "MN06ED":
                if (KnowEdmund_AskMinervaNecromancy.KnowEdmund_AskMinervaNecromancy == false)
                {
                KnowEdmund_AskMinervaNecromancy.KnowEdmund_AskMinervaNecromancy = true;
                }
                break;
        }
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

    // --------------------------------------- [**CURRENTLY NOT IN USE***] -----------------------------------//
    //This function checks if the player has acquired 100% of the clues, and then triggers the final cutscene's dialogue node.
    public void TriggerFinalCutscene()
    {
        ///* Check if all of the clues are added to the evidence board.
        // Honestly at this point I've lost track of the exact # of clues because it keeps changing. (Discovering new implicit clues that writers did not indicate is a real, formal clue)
        if (CluesOnBoard.CluesOnBoard == 22) 
        {
            //target node name is set to the first dialogue node in Ending.yarn. FIGURE OUT HOW TO CHAIN IT + PHOTOS TOGETHER! AND/OR JUST MAKE ONE BIG DIALOGUE NODE.
            targetNodeName = "EndingPhone";

        // If the currently running node is different from the target node (nodeName), Stop running the current node and dialogue.
            if (dialogueRunner.CurrentNodeName != targetNodeName)
            {
                dialogueRunner.Stop();
            }
            // Start running the target dialogue node.
            dialogueRunner.StartDialogue(targetNodeName);
            // quit game code here. :-) should run after dialogue is completed lol
        }
    }
    // --------------------------------------- [**CURRENTLY NOT IN USE**] -------------------------------------//

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
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
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
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
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
                /// - [MR-05-SB] - After finding spellbook. [HasSpellbook == True, MRTalkSpellbook == false] [Set MRTalkSpellbook = true, KnowOliviaWitch = true]
                else if (HasSpellbook.HasSpellbook == true & MRTalkSpellbook.MRTalkSpellbook == false)
                {
                    targetNodeName = "MR05SB";
                    RunTargetNode();
                    MRTalkSpellbook.MRTalkSpellbook = true;
                    if (KnowOliviaWitch.KnowOliviaWitch == false)
                    {
                        KnowOliviaWitch.KnowOliviaWitch = true;
                        CluesObtained.CluesObtained += 1;
                        CheckIfPlayerSick();
                    }
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
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                ///--- Start Questioning Dialogue: MRStartConvo ---//
                ///*Dialogue options: - These variables are set in Update() using [dialogueRunner.CurrentNode], due to being nested nodes.
                /// * About Mr.Wraithwood
                ///  - [MR08] -  [Set KnowWraithwoodIsGhost = true]
                ///  - [MR10] - [Set KnowWraithwoodIsRoomBound = true]
                /// * About Others
                ///  - [MR09OL] - [Set KnowOliviaRecentlyJoined = true]
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
                /// - [ED-12-MN] - Only appears if the player has the “Edmund asked Minerva about Necromancy” flag. [EDTalkMinervaNecromancy = false, KnowEdmund_AskMinervaNecromancy == true]
                else if (KnowEdmund_AskMinervaNecromancy.KnowEdmund_AskMinervaNecromancy == true & EDTalkMinervaNecromancy.EDTalkMinervaNecromancy == false)
                {
                    targetNodeName = "ED12MN";
                    RunTargetNode();
                    EDTalkMinervaNecromancy.EDTalkMinervaNecromancy = true;
                }
                ///  [ED-14-MX] - If Asked about Max's flirting. [EDTalk_MaxFlirtWithEdmund == false & KnowMaxRejectedByOlivia == True] [Set EDTalk_Max2 = true]
                else if (EDTalk_MaxFlirtWithEdmund.EDTalk_MaxFlirtWithEdmund == true & KnowMaxRejectedByOlivia.KnowMaxRejectedByOlivia == true & EDTalk_Max2.EDTalk_Max2 == false)
                {
                    targetNodeName = "ED14MX";
                    RunTargetNode();
                    EDTalk_Max2.EDTalk_Max2 = true;

                }
                ///--- Start Questioning Dialogue: [EDStartConvo] ---//
                else
                { 
                    targetNodeName = "EDStartConvo";
                    RunTargetNode();
                }
                ///*Dialogue options: - These variables are set in Update() using [dialogueRunner.CurrentNode], due to being nested nodes.
                /// - About Him
                ///  * [ED-04] - His Alibi
                ///    // CURRENTLY NOT IMPLEMENTED [ED04OL]
                ///    - [ED-04-OL] - His Alibi, alternate dialogue for same dialogue option. [EDAskedAboutOlivia == False].
                ///  * [ED-05] - On Being a Skeleton [Set KnowEdmund_Want_UndoUndead = true]
                /// - About Others
                ///  * [ED-10-BT] - On Bartholomew [Set KnowBartDislikesHumans = true]
                ///  * [ED-11-MN] - On Minerva
                ///  * [ED-13-MX] - On Max [Set EDTalk_MaxFlirtWithEdmund = true]
                ///  * [ED-15-MR] - On Mr.Wraithwood
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
                /// - [MN-14-PSN] - Has collected >= 10 clues, and PlayerIsSick. [PlayerIsSick == true, MNTalkPlayerSick == false] [Set MNTalkPlayerSick = true]
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
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                ///  - [MN-09-HX] - Hex Bag (before asking about Spellbook) [MNTalkSpellbook == False, HasHexBag == true, MNTalkHexBag == false] [Set MNTalkHexBag = true]
                else if (HasHexBag.HasHexBag == true & MNTalkSpellbook == false & MNTalkHexBag.MNTalkHexBag == false)
                {
                    targetNodeName = "MN09HX";
                    RunTargetNode();
                    MNTalkHexBag.MNTalkHexBag = true;
                }
                ///  - [MN-09-HX-SB] - Hex Bag (after asking about Spellbook) [MNTalkSpellbook == True, HasHexBag == true, MNTalkHexBag == false] [set MNTalkHexBag = true]
                else if (HasHexBag.HasHexBag == true & MNTalkSpellbook.MNTalkSpellbook == true & MNTalkHexBag.MNTalkHexBag == false)
                {
                    targetNodeName = "MN09HXSB";
                    RunTargetNode();
                    MNTalkHexBag.MNTalkHexBag = true;
                }
                ///  - [MN-10-SB] - Spellbook [MNTalkSpellbook == false, HasSpellbook == true] [Set KnowOliviaWitch = True, MNTalkSpellbook = True]
                else if (HasSpellbook.HasSpellbook == true & MNTalkSpellbook == false)
                {
                    targetNodeName = "MN10SB";
                    RunTargetNode();
                    MNTalkSpellbook.MNTalkSpellbook = true;
                    if (KnowOliviaWitch.KnowOliviaWitch == false)
                    {
                        KnowOliviaWitch.KnowOliviaWitch = true;
                        CluesObtained.CluesObtained += 1;
                        CheckIfPlayerSick();
                    }
                }
                ///  - [MN-11-OL] - Olivia Is Witch (After learning Olivia is a witch from Minerva) [KnowOliviaWitch == True, MNTalkOliviaWitch == false] [Set MNTalkOliviaWitch = true, KnowOliviaNecromancer = True]
                else if (KnowOliviaWitch.KnowOliviaWitch == true & MNTalkOliviaWitch == false)
                {
                    targetNodeName = "MN11OL";
                    RunTargetNode();
                    MNTalkOliviaWitch.MNTalkOliviaWitch = true;
                    KnowOliviaNecromancer.KnowOliviaNecromancer = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                ///  - [MN-12-PSN] - Bottles From Minvera's Room writers had a clue for vial interaction but it's not a physical clue, so doubt it'd be accessible. no requirements. 
                ///     * [KnowMinervaHasPoison == false] [Set KnowMinervaHasPoison = true]
                else if (KnowMinervaHasPoison.KnowMinervaHasPoison == false)
                {
                    targetNodeName = "MN12PSN";
                    RunTargetNode();
                    KnowMinervaHasPoison.KnowMinervaHasPoison = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                ///--- Start Questioning Dialogue: [MNStartConvo] ---//
                else
                {
                    targetNodeName = "MNStartConvo";
                    RunTargetNode();
                }
                ///*Dialogue Options: - These variables are set in Update() using [dialogueRunner.CurrentNode], due to being nested nodes.
                /// - About Others
                ///  * [MN-03-OL] - Olivia
                ///  * [MN-04-MX] - Maxwell
                ///  * [MN-06-ED] - Edmund [Set KnowEdmund_AskMinerva_Necromancy = true]
                ///  * [MN-07-BT] - Bart
                ///  * [MN-13-MR] - Mr. Wraithwood
                /// - About the Meeting
                ///  * [MN-05] - About the Meeting
                ///  * [MN-08] - About Alibi since the Meeting
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
                    MXTalkedAboutMeeting.MXTalkedAboutMeeting = true;
                    KnowOliviaMarried.KnowOliviaMarried = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                /// - [MX-03-BD] - About the meeting, if Max has been told that Olivia is dead. [MXKnowsOliviaDead == true, MXTalkedAboutMeeting == false] [Set MXTalkedAboutMeeting = true, KnowMaxRejectedByOlivia = true, KnowOliviaMarried = true]
                else if (MXKnowsOliviaDead.MXKnowsOliviaDead == true & MXTalkedAboutMeeting.MXTalkedAboutMeeting == false)
                {
                    targetNodeName = "MX03BD";
                    RunTargetNode();
                    MXTalkedAboutMeeting.MXTalkedAboutMeeting = true;
                    // If player has not yet acquired this clue
                    if (KnowMaxRejectedByOlivia.KnowMaxRejectedByOlivia == false)
                    {
                        KnowMaxRejectedByOlivia.KnowMaxRejectedByOlivia = true;
                        CluesObtained.CluesObtained += 1;
                        CheckIfPlayerSick();
                    }
                    if (KnowOliviaMarried.KnowOliviaMarried == false)
                    {
                        KnowOliviaMarried.KnowOliviaMarried = true;
                        CluesObtained.CluesObtained += 1;
                        CheckIfPlayerSick();
                    }
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
                /// - [MX-09-BT] - Ask about Bartholomew [MaxKnowsOliviaDead == true, MXTalkBart == false] [set KnowBartBitHuman = true, MXTalkBart = true]
                else if (MXKnowsOliviaDead.MXKnowsOliviaDead == true & MXTalkBart == false)
                {
                    targetNodeName = "MX09BT";
                    RunTargetNode();
                    MXTalkBart.MXTalkBart = true;
                    KnowBartBitHuman.KnowBartBitHuman = true;
                    CluesObtained.CluesObtained += 1;
                    CheckIfPlayerSick();
                }
                ///--- Start Questioning Dialogue: [MXStartConvo] ---//
                else
                {
                    targetNodeName = "MXStartConvo";
                    RunTargetNode();
                }
                ///*Dialogue Options: - These variables are set in Update() using [dialogueRunner.CurrentNode], due to being nested nodes.
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