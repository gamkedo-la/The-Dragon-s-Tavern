using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawACard : MonoBehaviour
{
    public GameObject[] cards;
    public Transform playerHand;

    public GameObject[] physicalCardsOnTable;
    public Text deckCountRemainingUI;

    public int initialCardsToDraw = 5;

    //Player Hand - remove this later to a more accessible place
    public List<Card> monsterCards = new List<Card>();
    public List<SpellCard> spellCards = new List<SpellCard>();

    int totalCardsInDeck;
    GameObject newCard;
    string cardNameToSave;
    SpellCard spellCardToPull;

    private void Start()
    {
        //draw initial hand
        StartCoroutine(WaitToDrawCard());
        totalCardsInDeck = monsterCards.Count + spellCards.Count;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        //List of Monster Cards and Spell cards are public right now. May want to hide that eventually.

        //run a deck random number counter
        int randomCardFromDeck = Random.Range(0, totalCardsInDeck);
        //pulling a monster card
        if (randomCardFromDeck >= spellCards.Count)
        {
            //take list of cards (monsters+spells), subtract the spell cards, then pull a monster card
            int monsterCardToMake = randomCardFromDeck - spellCards.Count;
            cardNameToSave = monsterCards[monsterCardToMake].ToString();
            newCard = Instantiate(cards[1], playerHand.transform.position, Quaternion.identity) as GameObject;

            //Making the card name match the card that is drawn
            cardNameToSave = cardNameToSave.Replace(" (Card)", "");
            newCard.GetComponentInChildren<CardDisplay>().card = Resources.Load<Card>("ScriptableObject/Monsters/" +cardNameToSave) as Card;

            //Remove Monster Card from List
            monsterCards.Remove(monsterCards[monsterCardToMake]);
        }

        //pulling a spell card
        else
        {
            cardNameToSave = spellCards[randomCardFromDeck].ToString();
            newCard = Instantiate(cards[0], playerHand.transform.position, Quaternion.identity);

            //Making the card name match the card that is drawn
            cardNameToSave = cardNameToSave.Replace(" (SpellCard)", "");
            newCard.GetComponentInChildren<CardDisplay>().spellCard = Resources.Load<SpellCard>("ScriptableObject/Spell/" + cardNameToSave) as SpellCard;

            //Remove Spell card from list
            spellCards.Remove(spellCards[randomCardFromDeck]);
        }

        //Create the card

        newCard.transform.parent = playerHand;
        newCard.transform.localScale = new Vector3(1, 1, 1);
        newCard.transform.localRotation = Quaternion.identity;

         totalCardsInDeck--;
        //Update UI of cards in player's deck
         deckCountRemainingUI.text = totalCardsInDeck.ToString();
        //Remove cards that are on the table if less than 10 remain
        if (totalCardsInDeck < 10)
        {
            physicalCardsOnTable[totalCardsInDeck].SetActive(false);
        }
    }

    IEnumerator WaitToDrawCard()
    {
        //While initial cards in hand is less than initialCardsToDraw, draw another card but wait 0.25f
        while (initialCardsToDraw > 0)
        {
            //Wait .25f seconds
            yield return new WaitForSeconds(0.5f);
            DrawCard();
            initialCardsToDraw--;
        }
    }
}
