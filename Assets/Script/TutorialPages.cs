using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPages : MonoBehaviour
{
    public int pageNumber = 0;
    public GameObject[] pages;

    public Button left, right;

    public Animator pageTurning;

    public Text pageCount;

    private void Update()
    {
        if (pageNumber <= 0)
        {
            pageNumber = 0;
            left.interactable = false;
        }        
        else {
            left.interactable = true;
        }

        if (pageNumber >= pages.Length - 1)
        {
            right.interactable = false;
        }
        else
        {
            right.interactable = true;
        }

        pageCount.text = "Page " + pageNumber + " of " + (pages.Length - 1);
    }

    public void TurnRight()
    {
        pageTurning.SetTrigger("RotateLeft");
        pages[pageNumber].SetActive(false);
        pageNumber--;
        StartCoroutine(DelayForPageTurn());
    }

    public void TurnLeft()
    {
        pageTurning.SetTrigger("RotateRight");
        pages[pageNumber].SetActive(false);
        pageNumber++;
        StartCoroutine(DelayForPageTurn());
    }

    IEnumerator DelayForPageTurn()
    {
        yield return new WaitForSeconds(1.25f);
        pages[pageNumber].SetActive(true);
    }
}
