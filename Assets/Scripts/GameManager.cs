using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This Game Manager stores global variables.
    
    // Variable for storing the name of the current selected character. Ex. currentCharacter = "Max".
    // Upon interaction with an NPC to begin dialogue, the TalkToCharacter() functoin within the DialogueTrigger.cs script will set this currentCharacter value to the selectedCharacter variable, set individually on each Character (NPC).
    public string currentCharacter;

    // Variables for Clues/Flags. These are all booleans, except for the counter variables: CluesObtained & CluesOnBoard.

    // Physical clues should be set to True once they have been picked up. (Should be done through the script that handles object interactions; Once picked up, set to True.)
    // Verbal clues should be set to True once they have been "obtained" through dialogue. (Currently handled within DialogueTrigger.cs. Can be done either through Yarn Spinner scripts, or this DialogueTrigger.cs script. Once activated, set to True.)

    // ------------------------------ Verbal Clues --------------------- //
    // ---- Player or Misc. Clues ----//
    public bool KnowPlayerIsPoisoned = true;
    // ------------------------------ General Variables ---------------- //
    // 10 CluesObtained is the trigger for PlayerIsSick.
    public int CluesObtained = 0;
    public int CluesOnBoard = 0;
    public bool PlayerIsSick = false;

}
