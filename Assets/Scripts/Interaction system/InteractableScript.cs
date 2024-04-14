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

    [Header("Evidence Board Clues")]
    public GameObject clue1;
    public GameObject clue2;
    public GameObject clue3;
    public GameObject clue4;
    public GameObject clue5;
    public GameObject clue6;

    InputMap actions;

    private List<Item> toremove;

    void Awake()
    {
        actions = new InputMap();

        actions.Player3D.Enable();
    }

    private void Update()
    {
        foreach (Item i in Items.items)
        {
            if (actions.Player3D.Interact.WasPressedThisFrame())
            {
                switch (i.id)
                {
                    case 1:
                        clue1.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    case 2:
                        clue2.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    case 3:
                        clue3.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    case 4:
                        clue4.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    case 5:
                        clue5.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    case 6:
                        clue6.SetActive(true);
                        Items.items.Remove(i);
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
