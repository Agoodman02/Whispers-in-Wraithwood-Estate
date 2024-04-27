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
    // ---- Wraithwood Clues -------------------------------------- WRAITHWOOD ---- //
    //Player finds out front door is locked IF they try to open the front door. (Interact with front door.) Technically a verbal clue, but has a physical object source.
    public bool KnowFrontDoorLocked = false;
    public bool KnowWraithwoodIsGhost = false;
    public bool KnowWraithwoodIsRoomBound = false;
    //*** Added to evidence board?
    public bool AddedKnowFrontDoorLocked = false;
    public bool AddedKnowWraithwoodIsGhost = false;
    public bool AddedKnowWraithwoodIsRoomBound = false;
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
    //UNOFFICIAL verbal clue/flag; Doesn't go on the evidence board.
    public bool MaxTalkedAboutMeeting = false;
    //UNOFFICIAL verbal clue/flag; Doesn't go on the evidence board.
    public bool MaxKnowsOliviaDead = false;
    //UNOFFICIAL flag; Doesn't go on the evidence board.
    public bool MaxTalkPreFindBody = false;
    // ---- Edmund Clues ------------------------------------------------ EDMUND ---- //
    public bool KnowEdmund_Want_UndoUndead = false;
    public bool KnowEdmund_Hate_BeingUndead = false;
    //*** Added to evidence board?
    public bool AddedKnowEdmund_Want_UndoUndead = false;
    public bool AddedKnowEdmund_Hate_BeingUndead = false;
    //UNOFFICIAL flag; Doesn't go on evidence board.
    public bool EdmundTalkOliviaBody = false;
    // ---- Minerva Clues ----------------------------------------------- MINERVA --- //
    public bool KnowHasPoison = false;
    public bool KnowMinervaDislikesOlivia = false;
    //*** Added to evidence board?
    public bool AddedKnowHasPoison = false;
    public bool AddedKnowMinervaDislikesOlivia = false;

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
