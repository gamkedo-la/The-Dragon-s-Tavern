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

    public GameObject[] playableAreas;

    int totalCardsInDeck;

    string cardNameToSave;

    GameObject newCard;
    public GameObject[] cards;
    int randomOpenPlayableSpot;
    int cardToChoose;

    public GameObject monsterCard, spellCard;

    public List<GameObject> opponentsVisualCardsInHand;
    public GameObject opponentVisualCard;
    public Transform opponentHand;
    GameObject toBeDestroyed;

    public Animator opponentArm;

    private void Start()
    {
        //draw initial hand
        StartCoroutine(WaitToDrawCard());
        totalCardsInDeck = monsterCards.Count /*+ spellCards.Count*/;      
    }

    public void DrawCard()
    {
        opponentArm.SetTrigger("DrawCard");

        newCard = Instantiate(opponentVisualCard, transform.position, transform.rotation) as GameObject;
        newCard.transform.parent = opponentHand.transform;
        newCard.transform.position = new Vector3(.5f, .2f, -4.3f);
        newCard.transform.localScale = new Vector3(1, 30, 20);
        opponentsVisualCardsInHand.Add(newCard);

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
          //  Printing(cardNameToSave);

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
          //  Printing(cardNameToSave);
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
      //  Printing("card is drawn");
        DrawCard();
    }

    public void PlayHand()
    {

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            //Choose a random card to play
            cardToChoose = Random.Range(0, cardsInHand.Count);

            string chosen = cardsInHand[cardToChoose];

            //need to get the .name of Monster cards in order for them to line up and be played on the table. 

          /*  if (monsterCards[i].name == chosen)
            {
                if (monsterCards[i].cost <= GameState.CurrencyThisTurn)
                {
                    // Choose a random spot to create the card
             //       ChooseWhereToPlayCard();

                    GameState.CurrencyThisTurn -= monsterCards[i].cost;

                    //Remove card from list
                    cardsInHand.Remove(cardsInHand[i]);
                    i++;
                }
            }
        }
          
        for (int i = 0; i < cardsInHand.Count; i++)
        {
          */
            Card monsterCard = GameManager.instance.FindMonster(chosen);
            SpellCard spellCard = GameManager.instance.FindSpell(chosen);

            string cardName = "error";
            int cardCost = 0;

            if (monsterCard != null)
            {
                cardName = monsterCard.name;
                cardCost = monsterCard.cost;
            }
            else if (spellCard != null)
            {
                cardName = spellCard.name;
                //there is no cost for spell cards
                cardCost = 0;
            }
            else
            {
                Debug.LogError("No card name found: " + chosen);
            }
            
            //print(chosen.ToString() + " " + cardName);

            if (cardCost <= GameState.CurrencyThisTurn)
            {
                GameState.CurrencyThisTurn -= cardCost;
                print("Currency:" + GameState.CurrencyThisTurn);
                ChooseWhereToPlayCard();
            }
            else
            {
                i++;
                print("Currency:" + GameState.CurrencyThisTurn + " Too Expensive");
            }
        }
        //Work on this, this is just a placeholder
        gameState.AdvanceTurnFromAnotherScript();
    }

    void ChooseWhereToPlayCard()
    {
        opponentArm.SetTrigger("PlayCard");
        GameObject cardCreated;
        //play card on table
        randomOpenPlayableSpot = Random.Range(0, playableAreas.Length);
        if (playableAreas[randomOpenPlayableSpot].transform.childCount != 0)
        {
            if (playableAreas[0].transform.childCount != 0 && playableAreas[1].transform.childCount != 0 && playableAreas[2].transform.childCount != 0 && playableAreas[3].transform.childCount != 0 && playableAreas[4].transform.childCount != 0 && playableAreas[5].transform.childCount != 0)
            {
                print("no playable spots available");
            }
            else
            {
                ChooseWhereToPlayCard();
            }
        }
        else
        {
            //visually pick random card from hand and put on table
            int pulledCardVisual = Random.Range(0, opponentsVisualCardsInHand.Count);
            toBeDestroyed = opponentsVisualCardsInHand[pulledCardVisual];
            opponentsVisualCardsInHand.Remove(opponentsVisualCardsInHand[pulledCardVisual]);
            Destroy(toBeDestroyed);

           // Printing(cardsInHand[cardToChoose]);
            //this is a valid location
            cardCreated = Instantiate(monsterCard, playableAreas[randomOpenPlayableSpot].transform.position, Quaternion.identity) as GameObject;
            cardCreated.transform.parent = playableAreas[randomOpenPlayableSpot].transform;

            cardCreated.GetComponentInChildren<CardDisplay>().card = Resources.Load<Card>("ScriptableObject/Monsters/" + cardsInHand[cardToChoose]) as Card;
            cardCreated.GetComponentInChildren<CardDisplay>().ReadyToInit();

            cardCreated.GetComponentInChildren<CardDisplay>().card.playedByAI = true;

            //chooseAttackOrDefense

            int choosePosition = Random.Range(0, 2);


           // print(choosePosition);
            //Defense
            if (choosePosition == 0)
            {
                cardCreated.GetComponentInChildren<CardDisplay>().thisCardInDefense = true;
             //   Debug.Log(cardCreated.GetComponentInChildren<CardDisplay>().thisCardInDefense);
             //   print(cardCreated.GetComponentInChildren<CardDisplay>().card.name + " " + cardCreated.GetComponentInChildren<CardDisplay>().card.inDefense);
                cardCreated.transform.localScale = new Vector3(.45f, .7f, .8f);
                cardCreated.transform.localRotation = Quaternion.Euler(0, 0, 90);
                cardCreated.transform.localPosition = new Vector3(0, 20, 0);
            }
            //Attack
            else
            {
                cardCreated.transform.localScale = new Vector3(.7f, .45f, .8f);
                cardCreated.transform.localRotation = Quaternion.identity;
                cardCreated.transform.localPosition = new Vector3(35, 0, 0);
            }

           // print(cardCreated.GetComponentInChildren<CardDisplay>().card.name + " " + cardCreated.GetComponentInChildren<CardDisplay>().thisCardInDefense);

        }
    }
}
