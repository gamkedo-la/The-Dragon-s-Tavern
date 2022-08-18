using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPacks : MonoBehaviour
{
    public Text currencyValue;
    public int buy1Cost, buy3Cost;

    public Animator cardPull;

    public Button backButton, buyButton;
    public GameObject buttonBuy;

    GameManager gameManager;

    private void Start()
    {
        StartCoroutine(Wait());

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.MonsterCardsToBePulled.Count <= 0 && gameManager.SpellCardsToBePulled.Count <= 0)
        {
            buttonBuy.SetActive(false);
        }
    }

    public void Buy1()
    {
        if (GameManager.currency >= buy1Cost)
        {
            GameManager.currency -= buy1Cost;

            cardPull.SetTrigger("PullPack");

            // print("Buy 1 card");
            currencyValue.text = GameManager.currency.ToString();

            backButton.interactable = false;
            buyButton.interactable = false;
            StartCoroutine(Waiting());
        }
    }
    public void Buy3()
    {
        if (GameManager.currency >= buy3Cost)
        {
            GameManager.currency -= buy3Cost;
            print("Animation");
            print("Buy 3 card");
            currencyValue.text = GameManager.currency.ToString();
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(5f);
        backButton.interactable = true;
        buyButton.interactable = true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        currencyValue.text = GameManager.currency.ToString();
    }
}
