using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPacks : MonoBehaviour
{
    public Text currencyValue;
    public int buy1Cost, buy3Cost;

    private void Start()
    {
        currencyValue.text = GameManager.currency.ToString();
    }

    public void Buy1()
    {
        if (GameManager.currency >= buy1Cost)
        {
            GameManager.currency -= buy1Cost;
            print("Animation");
            print("Buy 1 card");
            currencyValue.text = GameManager.currency.ToString();
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
}
