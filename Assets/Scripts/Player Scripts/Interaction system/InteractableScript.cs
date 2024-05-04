//holds different methods to run for interactable objects to run
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableScript : MonoBehaviour
{
    //declare varibles
    [SerializeField] GameManager gameManager;
    [SerializeField] InventoryManager inventoryManager;
    public InventorySpace Items;
    public Item Item;

    public GameObject slideshow;

    [Header("Interaction Objects")]
    public GameObject bookShelf;
    public GameObject JewelryBoxLid;
    public GameObject closedVent;
    public GameObject openedVent;
    public GameObject bloodyPen;
    public GameObject picture1;
    public GameObject oliviaBody;

    [Header ("Candles")]
    public GameObject redCandle;
    public GameObject blueCandle;
    public GameObject greenCandle;
    public GameObject pinkCandle;
    public GameObject purpleCandle;

    [Header("Sound Effects")]
    public AudioSource ventSound;
    public AudioSource bookshelfSound;
    public AudioSource meowSound;
    public AudioSource quackSound;

    [Header("Bart Clues")]
    public GameObject clue1;
    public GameObject clue2;
    [Header("Max Clues")]
    public GameObject clue3;
    public GameObject clue4;
    [Header("Edmund Clues")]
    public GameObject clue5;
    public GameObject clue6;
    [Header("Wraithwood Clues")]
    public GameObject clue7;
    public GameObject clue8;
    public GameObject clue9;
    [Header("Minerva Clues")]
    public GameObject clue10;
    public GameObject clue11;
    [Header("Olivia Clues")]
    public GameObject clue12;
    public GameObject clue13;
    public GameObject clue14;
    public GameObject clue15;
    [Header("Physical Evidence Clues")]
    public GameObject clue16;
    public GameObject clue17;
    public GameObject clue18;
    public GameObject clue19;
    public GameObject clue20;
    public GameObject clue21;

    InputMap actions;
    [HideInInspector] public bool nearboard = false;

    private List<Item> toremove;

    void Awake()
    {
        actions = new InputMap();

        actions.Player3D.Enable();
        /*
        old busted

        //Clear inventory on start; only clears single item?
        foreach (Item i in Items.items) 
        {
            Items.items.Remove(i);
        }
        */

        //new hotness
        Items.clearinv();
    }

    private void Update()
    {
        //checks for ending
        if (actions.Player3D.Interact.WasPressedThisFrame() && nearboard)
        {
            //checks if ending
            if (gameManager.CluesOnBoard == 21)
            {
                slideshow.SetActive(true);
            }
            else
            {
                Debug.Log("Haha loser, no clues");
            }
            
            //checks for dialogue clues
            if( gameManager.KnowPlayerIsPoisoned == true)
            {
                gameManager.AddedKnowPlayerIsPoisoned = true;
                clue18.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            // ---- Bart Clues ------------------------------------------- BARTHOLOMEW ----- //
            if (gameManager.KnowBartBitHuman == true)
            {
                gameManager.AddedKnowBartBitHuman = true;
                clue2.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if (gameManager.KnowBartDislikesHumans == true)
            {
                gameManager.AddedKnowBartDislikesHumans = true;
                clue1.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            // ---- Wraithwood Clues -------------------------------------- WRAITHWOOD ---- //
            if (gameManager.KnowFrontDoorLocked == true)
            {
                gameManager.AddedKnowFrontDoorLocked = true;
                clue7.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if(gameManager.KnowWraithwoodIsGhost == true)
            {
                gameManager.AddedKnowWraithwoodIsGhost = true;
                clue9.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if (gameManager.KnowWraithwoodIsRoomBound == true)
            {
                gameManager.AddedKnowWraithwoodIsRoomBound = true;
                clue8.SetActive(true);
                gameManager.CluesOnBoard++;
            }

            // ---- Olivia Clues ------------------------------------------- OLIVIA -------- //
            if (gameManager.KnowOliviaKilled == true)
            {
                gameManager.AddedKnowOliviaKilled = true;
            }
            if (gameManager.KnowOliviaWidow == true)
            {
                gameManager.AddedKnowOliviaWidow = true;
                clue12.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if (gameManager.KnowOliviaWitch == true)
            {
                gameManager.AddedKnowOliviaWitch = true;
                clue13.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if (gameManager.KnowOliviaNecromancer == true)
            {
                gameManager.AddedKnowOliviaNecromancer = true;
                clue14.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if (gameManager.KnowOliviaRecentlyJoined == true)
            {
                gameManager.AddedKnowOliviaRecentlyJoined = true;
                clue15.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            // ---- Max Clues --------------------------------------------- MAXWELL ------- //
            if (gameManager.KnowMaxRejectedByOlivia == true)
            {
                gameManager.AddedKnowMaxRejectedByOlivia = true;
                clue3.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if (gameManager.KnowMaxSeenWithBlood == true)
            {
                gameManager.AddedKnowMaxSeenWithBlood = true;
                clue4.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            // ---- Edmund Clues ------------------------------------------------ EDMUND ---- //
            if (gameManager.KnowEdmund_Want_UndoUndead == true)
            {
                gameManager.AddedKnowEdmund_Want_UndoUndead = true;
                clue6.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if (gameManager.KnowEdmund_Hate_BeingUndead == true)
            {
                gameManager.AddedKnowEdmund_Hate_BeingUndead = true;
                clue5.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            // ---- Minerva Clues ----------------------------------------------- MINERVA --- //
            if (gameManager.KnowHasPoison == true)
            {
                gameManager.AddedKnowHasPoison = true;
                clue11.SetActive(true);
                gameManager.CluesOnBoard++;
            }
            if (gameManager.KnowMinervaDislikesOlivia == true)
            {
                gameManager.AddedKnowMinervaDislikesOlivia = true;
                clue10.SetActive(true);
                gameManager.CluesOnBoard++;
            }
        }

        foreach (Item i in Items.items)
        {
            if (actions.Player3D.Interact.WasPressedThisFrame() && nearboard)
            {
                //checks inventory for physical evidence
                switch (i.id)
                {
                    //Picture 1
                    case 1:
                        if (!clue18.activeSelf)
                        {
                            clue18.SetActive(true);
                            gameManager.AddedHasOliviaEdmundPhoto = true;
                            gameManager.CluesOnBoard++;
                        }
                        //Items.items.Remove(i);
                        break;
                    //Bloody Pen
                    case 2:
                        if (!clue17.activeSelf)
                        {
                            clue17.SetActive(true);
                            gameManager.AddedHasBloodyPen = true;
                            gameManager.CluesOnBoard++;
                        }
                        //Items.items.Remove(i);
                        break;
                    //Hex Bag
                    case 3:
                        if (!clue19.activeSelf)
                        {
                            clue19.SetActive(true);
                            gameManager.AddedHasHexBag = true;
                            gameManager.CluesOnBoard++;
                        }
                        //Items.items.Remove(i);
                        break;
                    //Poison Cup
                    case 4:
                        if (!clue21.activeSelf)
                        {
                            clue21.SetActive(true);
                            gameManager.AddedHasOliviaCup = true;
                            gameManager.CluesOnBoard++;
                        }
                        //Items.items.Remove(i);
                        break;
                    //Corpse Pic
                    case 5:
                        if (!clue16.activeSelf)
                        {
                            clue16.SetActive(true);
                            gameManager.AddedFindBody = true;
                            gameManager.CluesOnBoard++;
                        }
                        //Items.items.Remove(i);
                        break;
                    //Spellbook
                    case 6:
                        if (!clue20.activeSelf)
                        {
                            clue20.SetActive(true);
                            gameManager.AddedHasSpellbook = true;
                            gameManager.CluesOnBoard++;
                        }
                        //Items.items.Remove(i);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void MoveBookShelf()
    {
        if (pinkCandle.activeSelf & greenCandle.activeSelf & blueCandle.activeSelf & redCandle.activeSelf & purpleCandle.activeSelf)
        {
            bookShelf.SetActive(false);
            bookshelfSound.Play();
        }
    }

    public void OpenJewelryBox()
    {
        foreach(Item i in Items.items)
        {
            if (i.id == 7)
            {
                JewelryBoxLid.SetActive(false);
                picture1.SetActive(true);
                Items.items.Remove(i);
            }
        }
    }

    public void OpenVent()
    {
        foreach (Item i in Items.items)
        {
            if (i.id == 9)
            {
                closedVent.SetActive(false);
                openedVent.SetActive(true);
                bloodyPen.SetActive(true);
                ventSound.Play();
                Items.items.Remove(i);
            }
        }
    }
    
    public void InteractWithBody()
    {
        InventoryManager.Instance.Add(Item);
    }

    public void CatMeow()
    {
        meowSound.Play();
    }

    public void DuckQuack()
    {
        quackSound.Play();
    }

    public void AddCandle()
    {
        //purple candle
        foreach (Item i in Items.items)
        {
            if (i.id == 10)
            {
                purpleCandle.SetActive(true);
                Items.items.Remove(i);
            }
        }
        //green candle
        foreach (Item i in Items.items)
        {
            if (i.id == 11)
            {
                greenCandle.SetActive(true);
                Items.items.Remove(i);
            }
        }
        //pink candle
        foreach (Item i in Items.items)
        {
            if (i.id == 12)
            {
                pinkCandle.SetActive(true);
                Items.items.Remove(i);
            }
        }
        //blue candle
        foreach (Item i in Items.items)
        {
            if (i.id == 13)
            {
                blueCandle.SetActive(true);
                Items.items.Remove(i);
            }
        }
        //red candle
        foreach (Item i in Items.items)
        {
            if (i.id == 14)
            {
                redCandle.SetActive(true);
                Items.items.Remove(i);
            }
        }
        if (pinkCandle.activeSelf & greenCandle.activeSelf & blueCandle.activeSelf & redCandle.activeSelf & purpleCandle.activeSelf)
        {
            bookShelf.SetActive(false);
            bookshelfSound.Play();
        }
    }
}
