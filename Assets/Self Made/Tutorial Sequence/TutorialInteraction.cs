using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//Pretty sure I got this right. If not feel free to correct it. I did not test it.
public class TutorialInteraction : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    public InventorySpace Items;
    public Item Item;

    [Header("Interaction Objects")]

    public GameObject PoisonCup;
    //Is that the name?
    //public GameObject tutorialflyer;


    //Idk if I need this part:

    /*InputMap actions;
    public bool nearboard = false;

    private List<Item> toremove;

    void Awake()
    {


        actions = new InputMap();

        actions.Player3D.Enable();
        

        //new hotness
        Items.clearinv();
    } */

    private void Update()
    {

    }
    public void GrabPoster()
    {
        InventoryManager.Instance.Add(Item);
    }
}
