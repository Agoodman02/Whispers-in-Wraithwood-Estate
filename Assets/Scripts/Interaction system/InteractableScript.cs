//holds different methods to run for interactable objects to run
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableScript : MonoBehaviour
{
    //declare varibles
    [SerializeField] InventoryManager inventoryManager;
    public InventorySpace Items;

    [Header("Interaction Objects")]
    public GameObject wardrobeBlockingDoor;
    public GameObject JewelryBoxLid;
    public GameObject janitorClosetDoor;
    public GameObject closedVent;
    public GameObject openedVent;
    public GameObject bloodyPen;
    public GameObject picture1;
    public GameObject picture2;
    public GameObject oliviaBody;

    [Header("Sound Effects")]
    public AudioSource ventSound;

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
    public bool nearboard = false;

    private List<Item> toremove;

    void Awake()
    {
        actions = new InputMap();

        actions.Player3D.Enable();

        //Clear inventory on start; only clears single item?
        foreach (Item i in Items.items) 
        {
            Items.items.Remove(i);
        }
    }

    private void Update()
    {
        foreach (Item i in Items.items)
        {
            if (actions.Player3D.Interact.WasPressedThisFrame() && nearboard)
            {
                switch (i.id)
                {
                    //Picture 1
                    case 1:
                        clue18.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    //Bloody Pen
                    case 2:
                        clue17.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    //Hex Bag
                    case 3:
                        clue19.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    //Poison Cup
                    case 4:
                        clue21.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    //Corpse Pic
                    case 5:
                        clue16.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    //Spellbook
                    case 6:
                        clue20.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    default:
                        return;
                }
            }
        }
    }

    public void MoveWardrobe()
    {
        wardrobeBlockingDoor.SetActive(false);
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

    public void OpenJanitorCloset()
    {
        foreach (Item i in Items.items)
        {
            if (i.id == 8)
            {
                //janitorClosetDoor.gameObject.GetComponents<DoorOpen>().OpenDoor(); //how to activate method from another object
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

    /*
    public void interactWithBody()
    {

    }
    */
}
