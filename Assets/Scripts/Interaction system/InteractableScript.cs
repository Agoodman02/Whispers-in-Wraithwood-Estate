//holds different methods to run for interactable objects to run
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableScript : MonoBehaviour
{
    //declare varibles
    [SerializeField] InventoryManager inventoryManager;
    private List<Item> items; //should this be <Item>?

    [Header("Evidence Board Clues")]
    public GameObject clue1;
    public GameObject clue2;
    public GameObject clue3;
    public GameObject clue4;
    public GameObject clue5;
    public GameObject clue6;

    public bool allowClue1;
    public bool allowClue2;
    [HideInInspector] public bool allowClue3 = false;
    [HideInInspector] public bool allowClue4 = false;
    [HideInInspector] public bool allowClue5 = false;
    [HideInInspector] public bool allowClue6 = false;

    private void Update()
    {
        //if (items.Contains(id)) { allowClue1 = true; }  //something to check id number
        //if (items.Contains(items.id(1))) { allowClue2 = true; }
        //if (items.Contains(items.id(1))) { allowClue3 = true; }
        //if (items.Contains(items.id(1))) { allowClue4 = true; }
        //if (items.Contains(items.id(1))) { allowClue5 = true; }
        //if (items.Contains(items.id(1))) { allowClue6 = true; }
    }


    public void EnableClue1()
    {
        if (allowClue1) clue1.SetActive(true);
    }
    public void EnableClue2()
    {
        if (allowClue2) clue2.SetActive(true);
    }
    public void EnableClue3()
    {
        if (allowClue3) clue3.SetActive(true);
    }
    public void EnableClue4()
    {
        if (allowClue4) clue4 .SetActive(true);
    }
    public void EnableClue5()
    {
        if (allowClue5) clue5.SetActive(true);
    }
    public void EnableClue6()
    {
        if (allowClue6) clue6.SetActive(true);
    }
}
