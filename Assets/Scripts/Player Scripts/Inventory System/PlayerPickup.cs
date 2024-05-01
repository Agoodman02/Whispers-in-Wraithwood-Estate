using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{
    float pickupRange = 100f;
    //public LayerMask mask;

    [HideInInspector] public bool allowPickup;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        //raycast hits things
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, pickupRange)) 
        {
            //if the thing is an item it will tell another script that the item is able to be picked up
            if (hit.collider.gameObject.CompareTag("Item"))
            {
                
            }
            Debug.Log("There is something in front of the object!");
        }

        /*
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange) && hit.collider.gameObject.CompareTag("Player"))
        {

            allowPickup = true;
            Debug.Log("Looking");
        }
        else
        {
            allowPickup = false;
        }
        */
    }
}
