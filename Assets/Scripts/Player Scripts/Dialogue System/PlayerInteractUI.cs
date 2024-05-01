using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
[SerializeField] private GameObject containergameObject;
[SerializeField] private PlayerInteract playerInteract;

private void Update()
{
    if (playerInteract.GetNPCInteractableObject() != null)
    {
        Show();
    }
    else 
    {
        Hide();
    }
}

private void Show() 
{
    containergameObject.SetActive(true);
}

private void Hide()
{
containergameObject.SetActive(false);
}
}
