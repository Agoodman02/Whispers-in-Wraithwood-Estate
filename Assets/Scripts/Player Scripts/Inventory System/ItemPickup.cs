using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    //Declare varibles
    public Item Item;
    public InventorySpace Items;

    private GameManager gameManager;

    [SerializeField] Image pickupPopup; 
    public bool allowPickup;
    //[HideInInspector] public string itemPopupName;
    //private GameObject InventoryItem;

    private void Start()
    {
        //Makes sure things are not ugly
        DisablePickupPopup();

        //gameManager = GameManager;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        //If within range
        if (allowPickup)
        {
            //And player hits "E"
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Item gets picked up
                Pickup();
            }
        }
        //Adds clue info to game manager
        foreach (Item i in Items.items)
        {
            switch (i.id)
            {
                //Picture 1
                case 1:
                    gameManager.HasOliviaEdmundPhoto = true;
                    break;
                //Bloody Pen
                case 2:
                    gameManager.HasBloodyPen = true;
                    break;
                //Hex Bag
                case 3:
                    gameManager.HasHexBag = true;
                    break;
                //Poison Cup
                case 4:
                    Debug.Log("Has Olivia's cup");
                    gameManager.HasOliviaCup = true;
                    break;
                //Corpse Pic
                case 5:
                    gameManager.FindBody = true;
                    break;
                //Spellbook
                case 6:
                    gameManager.HasSpellbook = true;
                    break;
                default:
                    break;
            }
        }
    }

    //Picks up item
    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
        DisablePickupPopup();
    }

    //Displays pickup text
    public void EnablePickupPopup()
    {
        pickupPopup.gameObject.SetActive(true);
    }

    //Hides pickup text
    public void DisablePickupPopup()
    {
        pickupPopup.gameObject.SetActive(false);
    }
}
