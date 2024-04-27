using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This Game Manager stores global variables.
    
    // Variable for storing the name of the current selected character. Ex. currentCharacter = "Max".
    // Upon interaction with an NPC to begin dialogue, the TalkToCharacter() functoin within the DialogueTrigger.cs script will set this currentCharacter value to the selectedCharacter variable, set individually on each Character (NPC).
    public string currentCharacter;

    // Variables for Clues/Flags. These are all booleans, except for the counter variables: CluesObtained & CluesOnBoard. "Added"-prefix bool is used for checking if this clue has been added to the evidence board or not.

    // Physical clues should be set to True once they have been picked up. (Should be done through the script that handles object interactions; Once picked up, set to True.)
    // Verbal clues should be set to True once they have been "obtained" through dialogue. (Currently handled within DialogueTrigger.cs. Can be done either through Yarn Spinner scripts, or this DialogueTrigger.cs script. Once activated, set to True.)

    // ------------------------------ Verbal Clues ----------------------------------------- //
    // ---- Player or Misc. Clues --------------------------------- PLAYER --------- //
    public bool KnowPlayerIsPoisoned = false;
    //*** Added to evidence board?
    public bool AddedKnowPlayerIsPoisoned = false;
    // ---- Bart Clues ------------------------------------------- BARTHOLOMEW ----- //
    public bool KnowBartBitHuman = false;
    public bool KnowBartDislikesHumans = false;
    //*** Added to evidence board?
    public bool AddedKnowBartBitHuman = false;
    public bool AddedKnowBartDislikesHumans = false;
    //UNOFFICIAL flags; Not on board.
    public bool BTTalkPreBody = false;
    public bool BTTalkPostBody = false;
    public bool BTTalkOEPhoto = false;
    public bool BTTalkBartBitHuman = false;

    // ---- Wraithwood Clues -------------------------------------- WRAITHWOOD ---- //
    //Player finds out front door is locked IF they try to open the front door. (Interact with front door.) Technically a verbal clue, but has a physical object source.
    public bool KnowFrontDoorLocked = false;
    public bool KnowWraithwoodIsGhost = false;
    public bool KnowWraithwoodIsRoomBound = false;
    //*** Added to evidence board?
    public bool AddedKnowFrontDoorLocked = false;
    public bool AddedKnowWraithwoodIsGhost = false;
    public bool AddedKnowWraithwoodIsRoomBound = false;
    //UNOFFICIAL flag; Doesn't go on evidence board.
    public bool MRTalkSick = false;
    public bool MRTalkFrontDoor = false;
    public bool MRTalkBody = false;
    public bool MRTalkSpellbook = false;
    public bool MRTalkOEPhoto = false;
    // ---- Olivia Clues ------------------------------------------- OLIVIA -------- //
    public bool KnowOliviaKilled = false;
    public bool AddedKnowOliviaKilled = false;
    // UNOFFICIAL verbal clue; Doesn't go on the evidence board.
    public bool KnowOliviaMarried = false;
    public bool KnowOliviaWidow = false;
    public bool KnowOliviaWitch = false;
    public bool KnowOliviaNecromancer = false;
    public bool KnowOliviaRecentlyJoined = false;
    //*** Added to evidence board?
    public bool AddedKnowOliviaWidow = false;
    public bool AddedKnowOliviaWitch = false;
    public bool AddedKnowOliviaNecromancer = false;
    public bool AddedKnowOliviaRecentlyJoined = false;
    // ---- Max Clues --------------------------------------------- MAXWELL ------- //
    public bool KnowMaxRejectedByOlivia = false;
    public bool KnowMaxSeenWithBlood = false;
    //*** Added to evidence board?
    public bool AddedKnowMaxRejectedByOlivia = false;
    public bool AddedKnowMaxSeenWithBlood = false;
    //UNOFFICIAL flags; Doesn't go on the evidence board.
    public bool MXTalkPreFindBody = false;
    public bool MXTalkedAboutMeeting = false;
    public bool MXKnowsOliviaDead = false;
    public bool MXTalkBloodied = false;
    public bool MXTalkSick = false;
    // ---- Edmund Clues ------------------------------------------------ EDMUND ---- //
    public bool KnowEdmund_Want_UndoUndead = false;
    public bool KnowEdmund_Hate_BeingUndead = false;
    //*** Added to evidence board?
    public bool AddedKnowEdmund_Want_UndoUndead = false;
    public bool AddedKnowEdmund_Hate_BeingUndead = false;
    //UNOFFICIAL flags; Doesn't go on evidence board.
    public bool EDTalkPreBody = false;
    public bool EDTalkPostBody = false;
    public bool EDTalkOliviaWitch = false;
    public bool EDTalkOliviaNecromancer = false;
    public bool EDTalkOEPhoto = false;
    // ---- Minerva Clues ----------------------------------------------- MINERVA --- //
    public bool KnowHasPoison = false;
    public bool KnowMinervaDislikesOlivia = false;
    //*** Added to evidence board?
    public bool AddedKnowHasPoison = false;
    public bool AddedKnowMinervaDislikesOlivia = false;
    //UNOFFICIAL flags; Not on evidence board.
    public bool MNTalkPreBody = false;
    public bool MNTalkPostBody = false;
    public bool MNTalkPlayerSick = false;


    // ------------------------------ Physical Clues --------------------------------------------------- //
    public bool FindBody = false;
    public bool HasOliviaEdmundPhoto = false;
    public bool HasSpellbook = false;
    public bool HasOliviaCup = false;
    public bool HasBloodyPen = false;
    public bool HasHexBag = false;
    //*** Added to evidence board?
    
    public bool AddedFindBody = false;
    public bool AddedHasOliviaEdmundPhoto = false;
    public bool AddedHasSpellbook = false;
    public bool AddedHasOliviaCup = false;
    public bool AddedHasBloodyPen = false;
    public bool AddedHasHexBag = false;

    // ------------------------------ General Variables --------------------- //
    // 10 CluesObtained is the trigger for PlayerIsSick.
    public int CluesObtained = 0;
    public int CluesOnBoard = 0;
    public bool PlayerIsSick = false;

}
