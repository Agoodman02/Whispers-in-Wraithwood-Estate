using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    //declare varibles
    public GameObject InventoryMenu;
    private bool menuActivated;

    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;

        //get rid of later
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Closes Menu
        if(Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            CleanList();
        }
        //Opens Menu
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;  

            ListItems();
        }
    }

    //Adds Item to List/Inventory
    public void Add(Item item)
    {
        Items.Add(item);
    }

    //Removes Item from List/Inventory
    public void Remove(Item item) 
    { 
        Items.Remove(item);
    }

    //Displays item list when opening inventory
    public void ListItems()
    {
        //list inventory
        foreach(var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("Text").GetComponent<Text>();
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }

        SetInventoryItems();
    }

    //stops weird stuff from happening in inventory
    public void CleanList ()
    {
        //clean content before open
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }

    //Controls side visible section on inventory menu
    public void DisplayItem(Item item)
    {
        GameObject obj = Instantiate(InventoryItem, ItemContent);
        var itemDescription = GameObject.Find("ItemDescription").GetComponent<Text>();
        var itemView = GameObject.Find("ItemView").GetComponent<Image>();

        itemDescription.text = item.itemName;
        itemView.sprite = item.icon;
    }
    /*
    private void SelectItem(Item item)
    {
        foreach (Item items in )
        {
            Item[item].Find("Selected").gameObject.SetActive(false);
        }
        Item[item].Find("Selected").gameObject.SetActive(true);

        OnItemSelected?.Invoke(this, item);

    }
    */
}