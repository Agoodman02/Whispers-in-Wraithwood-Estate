using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class EndingSlideshow : MonoBehaviour
{
    //public DialogueTrigger dialogueRunner;
    public DialogueRunner dialogueRunner;

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
        dialogueRunner.StartDialogue("Slide1");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void openPage2()
    {
        slide1.SetActive(false);
        slide2.SetActive(true);
        slide3.SetActive(false);
        dialogueRunner.StartDialogue("Slide2");
    }

    public void openPage3()
    {
        slide3.SetActive(true);
        slide2.SetActive(false);
        slide4.SetActive(false);
        dialogueRunner.StartDialogue("Slide3");
    }
    public void openPage4()
    {
        slide4.SetActive(true);
        slide3.SetActive(false);
        slide5.SetActive(false);
        dialogueRunner.StartDialogue("Slide4");
    }
    public void openPage5()
    {
        slide5.SetActive(true);
        slide4.SetActive(false);
        slide6.SetActive(false);
        dialogueRunner.StartDialogue("Slide5");
    }
    public void openPage6()
    {
        slide6.SetActive(true);
        slide5.SetActive(false);
    }

    public void startSlideshow()
    {
        slideshowCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
