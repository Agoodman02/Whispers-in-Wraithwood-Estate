using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//Pretty sure I got this right. If not feel free to correct it. I did not test it.
//Leave Skye to dialogue trigger.
public class TutorialInteraction : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    public InventorySpace Items;
    public Item Item;

    [Header("Interaction Objects")]

    public GameObject TutorialFlyer;
    public GameObject PoisonCup;

    //Add in scene transition to full level after player picks up poison cup.
    //Also maybe have the player forced to pick up flyer first then the cup.

    //Idk if I need this part:

    /*InputMap actions;
    public bool nearboard = false;

    private List<Item> toremove;

    void Awake()
    {
        actions = new InputMap();

        actions.Player3D.Enable();
        
        Items.clearinv();
    } */

    private void Update()
    {

    }
    public void GrabFlyer()
    {
        InventoryManager.Instance.Add(Item);
    }
}
