using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubAreaTriggers : MonoBehaviour
{
    public Animator cameraAnimation;
    public GameObject DeckCreation;
    public GameObject packPurchase;

    GameManager gameManager;

    UpdateCardsOwned updateCardsOwned;

    public GameObject settingsMenu;
    public Slider animationSpeed, difficultyPoints;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetUpDeck()
    {
        cameraAnimation.SetTrigger("ToDeck");
        StartCoroutine(WaitForDeck());
    }

    public void BuyCards()
    {
        cameraAnimation.SetTrigger("ToPurchase");
        StartCoroutine(WaitForDeck2());
    }

    public void BackFromDeck()
    {
        cameraAnimation.SetTrigger("ToMainFromDeck");
        DeckCreation.SetActive(false);
        packPurchase.SetActive(false);

      //  gameManager.SaveGame();
    }

    public void BackFromCards()
    {
        cameraAnimation.SetTrigger("ToMainFromCards");
        DeckCreation.SetActive(false);
        packPurchase.SetActive(false);

      //  gameManager.SaveGame();
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
        yield return new WaitForSeconds(.25f);
        updateCardsOwned = GameObject.Find("Canvas-DeckCreation").GetComponent<UpdateCardsOwned>();
        updateCardsOwned.RefreshList();
    }

    IEnumerator WaitForDeck2()
    {
        yield return new WaitForSeconds(2.75f);
        packPurchase.SetActive(true);
    }

    public void Settings()
    {
        cameraAnimation.SetTrigger("ToSettings");
        StartCoroutine(ToSettings());
    }

    IEnumerator ToSettings()
    {
        yield return new WaitForSeconds(2.7f);
        settingsMenu.SetActive(true);
    }

    public void BackFromSettings()
    {
        //Save animation Speed
        //Save Opponent Difficulty

        settingsMenu.SetActive(false);

        cameraAnimation.SetTrigger("BackFromSettings");
    }
}
