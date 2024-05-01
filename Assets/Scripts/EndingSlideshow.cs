using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSlideshow : MonoBehaviour
{
    [Header("Slides")]
    public GameObject slide1;
    public GameObject slide2;
    public GameObject slide3;
    public GameObject slide4;
    public GameObject slide5;
    public GameObject slide6;

    [Header("Canvases")]
    public GameObject slideshowCanvas;
    public GameObject playerUI;

    public void Awake()
    {
        //make sure things aren't ugly
        slideshowCanvas.SetActive(false);
    }

    public void openPage1()
    {
        slide1.SetActive(true);
        slide2.SetActive(false);
    }

    public void openPage2()
    {
        slide1.SetActive(false);
        slide2.SetActive(true);
        slide3.SetActive(false);
    }

    public void openPage3()
    {
        slide3.SetActive(true);
        slide2.SetActive(false);
        slide4.SetActive(false);
    }
    public void openPage4()
    {
        slide4.SetActive(true);
        slide3.SetActive(false);
        slide5.SetActive(false);
    }
    public void openPage5()
    {
        slide5.SetActive(true);
        slide4.SetActive(false);
        slide6.SetActive(false);
    }
    public void openPage6()
    {
        slide6.SetActive(true);
        slide5.SetActive(false);
    }

    public void startSlideshow()
    {
        slideshowCanvas.SetActive(true);
    }
}
