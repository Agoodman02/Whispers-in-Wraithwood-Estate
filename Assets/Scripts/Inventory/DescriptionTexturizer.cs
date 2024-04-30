using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionTextureizer : MonoBehaviour
{
    public RawImage ItemInspectionWindow;
    public Text ItemDescription;

    public static DescriptionTextureizer single;

    void Awake()
    {
        if(single == null)
        {
            single = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PopulateDesc(Item i)
    {
        ItemDescription.text = i.itemDescription;
        ItemInspectionWindow.texture = i.ItemInspectionImage;
        ItemInspectionWindow.transform.gameObject.SetActive(true);
    }

    public void ClearDesc()
    {
        ItemDescription.text = null;
        ItemInspectionWindow.texture = null;
        ItemInspectionWindow.transform.gameObject.SetActive(false);
    }
}
