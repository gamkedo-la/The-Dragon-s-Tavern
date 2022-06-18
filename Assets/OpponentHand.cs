using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHand : MonoBehaviour
{
    public GameState gameState;
    public int initialCardsToDraw = 5;

    //Player Hand - remove this later to a more accessible place
    public List<Card> monsterCards = new List<Card>();
    public List<SpellCard> spellCards = new List<SpellCard>();
    public List<string> cardsInHand = new List<string>();

    int totalCardsInDeck;

    string cardNameToSave;

    private void Start()
    {
        //draw initial hand

        
        StartCoroutine(WaitToDrawCard());
        totalCardsInDeck = monsterCards.Count + spellCards.Count;
        
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

            //Making the card name match the card that is drawn
            cardNameToSave = cardNameToSave.Replace(" (Card)", "");

            cardsInHand.Add(cardNameToSave);
            Printing(cardNameToSave);

            Card tempCard = Resources.Load<Card>("ScriptableObject/Monsters/" + cardNameToSave) as Card;

            //Remove Monster Card from List
            monsterCards.Remove(monsterCards[monsterCardToMake]);
        }

        //pulling a spell card
        else
        {
            cardNameToSave = spellCards[randomCardFromDeck].ToString();
            //Making the card name match the card that is drawn
            cardNameToSave = cardNameToSave.Replace(" (SpellCard)", "");

            cardsInHand.Add(cardNameToSave);
            Printing(cardNameToSave);

            SpellCard tempCard = Resources.Load<SpellCard>("ScriptableObject/Spell/" + cardNameToSave) as SpellCard;



            //Remove Spell card from list
            spellCards.Remove(spellCards[randomCardFromDeck]);
        }
        totalCardsInDeck--;
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
    void Printing(string message)
    {
        Debug.Log(message);
    }
    public void DrawACard()
    {
        Printing("card is drawn");
        DrawCard();
    }

    public void PlayHand()
    {
        Printing("card is being played");
        Printing(cardsInHand.Count.ToString());
        //Choose a random card to play
        int cardToChoose = Random.Range(0, cardsInHand.Count);
        Printing(cardsInHand[cardToChoose]);
        //Remove card from list
        Printing(cardsInHand[cardToChoose] + " has been removed from the hand");
        cardsInHand.Remove(cardsInHand[cardToChoose]);
        Printing(cardsInHand.Count.ToString());

        //Work on this, this is just a placeholder
        gameState.AdvanceTurnFromAnotherScript();
    }
}