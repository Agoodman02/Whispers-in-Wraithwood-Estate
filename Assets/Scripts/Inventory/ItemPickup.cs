using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    //Declare varibles
    public Item Item;
    [SerializeField] Image pickupPopup; 
    [SerializeField] Text ItemText;

    public bool allowPickup;
    [HideInInspector] public string itemPopupName;
    private GameObject InventoryItem;

    private void Start()
    {
        //Makes sure things are not ugly
        DisablePickupPopup();
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
        ItemText.text = $"{Item.itemName}";
    }

    //Hides pickup text
    public void DisablePickupPopup()
    {
        pickupPopup.gameObject.SetActive(false);
    }
}
