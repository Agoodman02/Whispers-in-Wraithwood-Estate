using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    //Removes item from inventory list
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    //Adds item to inventory list
    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
