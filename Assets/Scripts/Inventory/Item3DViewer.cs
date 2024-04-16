using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3DViewer : MonoBehaviour
{
    
    [SerializeField] private InventoryManager inventoryManager;

    private void Start()
    {
        //need to edit manager to detect button click and return value


        //inventoryManager.OnItemSelected += InventoryManager_OnItemSelected;
    }

    private void InventoryManager_OnItemSelected(object sender, Item e)
    {
        Debug.Log(e);
    }
    
}
