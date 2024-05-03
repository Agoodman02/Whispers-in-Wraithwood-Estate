using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Current Inventory", menuName = "Item/Create New Inventory")]
public class InventorySpace : ScriptableObject
{
    [SerializeField] public List<Item> items;

    public void clearinv()
    {
        items.Clear();
    }
}
