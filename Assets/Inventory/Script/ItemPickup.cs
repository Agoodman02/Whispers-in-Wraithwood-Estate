using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    //Declare varibles
    public Item Item;
    [SerializeField] Image pickupPopup;

    private bool allowPickup;

    private void Start()
    {
        //Makes sure things are not ugly
        DisablePickupPopup();
    }

    private void Update()
    {
        //allowPickup = GameObject.Find("PlayerPrefab").GetComponent<PlayerPickup>().allowPickup;

        //Needs to get info from raycast to figure out when to display text and when allowed to pickup
        //Maybe from the player controller

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
    }
    
    //Shows popup and allows item to be picked up once in range
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnablePickupPopup();

            allowPickup = true;
        }
    }

    //Hides popup and does not allow item to be picked up once in range
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            DisablePickupPopup();

            allowPickup = false;
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
    void EnablePickupPopup()
    {
        pickupPopup.gameObject.SetActive(true);
    }

    //Hides pickup text
    void DisablePickupPopup()
    {
        pickupPopup.gameObject.SetActive(false);
    }
}
