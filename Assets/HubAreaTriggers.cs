using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubAreaTriggers : MonoBehaviour
{
    public Animator cameraAnimation;
    public GameObject DeckCreation;

    public void SetUpDeck()
    {
        cameraAnimation.SetTrigger("ToDeck");
        StartCoroutine(WaitForDeck());
    }

    public void BuyCards()
    {
        cameraAnimation.SetTrigger("ToPurchase");
    }

    public void BackFromDeck()
    {
        cameraAnimation.SetTrigger("ToMainFromDeck");
        DeckCreation.SetActive(false);
    }

    public void BackFromCards()
    {
        cameraAnimation.SetTrigger("ToMainFromCards");
    }

    public void ToCredits()
    {
        cameraAnimation.SetTrigger("ToCredits");
    }

    public void BackFromCredits()
    {
        cameraAnimation.SetTrigger("BackFromCredits");
    }

    public void ToTutorial()
    {
        cameraAnimation.SetTrigger("ToTutorial");
    }

    public void BackFromTutorial()
    {
        cameraAnimation.SetTrigger("BackFromTutorial");
    }

    IEnumerator WaitForDeck()
    {
        yield return new WaitForSeconds(2.75f);
        DeckCreation.SetActive(true);
    }
}
