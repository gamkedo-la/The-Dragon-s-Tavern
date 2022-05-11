using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawACard : MonoBehaviour
{
    public GameObject[] cards;
    public Transform playerHand;

    public int initialCardsToDraw = 5;
    int cardCount;
    //Player Hand - remove this later to a more accessible place
    public List<Card> monsterCards = new List<Card>();
    public List<SpellCard> spellCards = new List<SpellCard>();
    List<MonoBehaviour> list = new List<MonoBehaviour>();

    private void Start()
    {
        //draw initial hand
        StartCoroutine(WaitToDrawCard());
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrawCard();
        }
    }

    void DrawCard()
    {
        //list of monster cards
        //list of spell cards
        //run a deck random number counter
        //if random number < total spell cards, pull a spell card, else, pull a monster card
        //resize the given list
        GameObject newCard = Instantiate(cards[0], playerHand.transform.position, Quaternion.identity);
        newCard.transform.parent = playerHand;
        newCard.transform.localScale = new Vector3(1, 1, 1);
        newCard.transform.localRotation = Quaternion.identity;

        cardCount++;
    }

    IEnumerator WaitToDrawCard()
    {
        //While initial cards in hand is less than initialCardsToDraw, draw another card but wait 0.25f
        while (initialCardsToDraw > 0)
        {
            //Wait .25f seconds
            yield return new WaitForSeconds(0.25f);
            DrawCard();
            initialCardsToDraw--;
        }
    }
}
