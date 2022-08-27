using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentHand : MonoBehaviour
{
    public GameState gameState;
    public int initialCardsToDraw = 5;

    //Player Hand - remove this later to a more accessible place
    public List<Card> monsterCards = new List<Card>();
    public List<SpellCard> spellCards = new List<SpellCard>();
    public List<string> cardsInHand = new List<string>();

    public GameObject[] playableAreas;
    public GameObject[] playerSpots;

    int iterationCountLimit = 0;

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

    CardDisplay refInitiator;

    bool directAttack = false;

    int ChoosingPlayerCard;

    public GameObject winningScreen;
    public Text winningScreenText;

    public GameObject[] tableDeck;

    GameManager gameManager;

    private void Start()
    {
        //draw initial hand
        StartCoroutine(WaitToDrawCard());
        totalCardsInDeck = monsterCards.Count /*+ spellCards.Count*/;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DrawCard()
    {
        opponentArm.SetTrigger("DrawCard");

        newCard = Instantiate(opponentVisualCard, transform.position, transform.rotation) as GameObject;
        newCard.transform.SetParent(opponentHand.transform, false);
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

        if (totalCardsInDeck < 10)
        {
            tableDeck[totalCardsInDeck].SetActive(false);
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
    void Printing(string message)
    {
        Debug.Log(message);
    }
    public void DrawACard()
    {
        GameState gameState = GameObject.Find("GameState").GetComponent<GameState>();
        if (totalCardsInDeck <= 0 || gameState.opponentOutOfCards)
        {
            gameState.opponentOutOfCards = true;
            gameState.TriggerWinCondition();
            print("Opponent has no more cards to draw. The game is over.");
            winningScreen.SetActive(true);
            winningScreenText.text = "You outlasted your opponent. They have no more cards to draw. You win this round";

            this.GetComponent<OpponentHand>().enabled = false;
        }
        else
        {
            //  Printing("card is drawn");
            DrawCard();
        }
    }

    public void PlayHand()
    {
        //Changing card position to attack or defense
        if (totalCardsInDeck <= 0 || gameState.opponentOutOfCards)
        {
            for (int i = 0; i < playableAreas.Length; i++)
            {
                playableAreas[i].SetActive(false);
            }
        }

        else
        {
            for (int i = 0; i < playableAreas.Length; i++)
            {
                if (playableAreas[i].transform.childCount != 0)
                {
                    if (playableAreas[i].GetComponentInChildren<CardDisplay>().thisCardsAttack >= playableAreas[i].GetComponentInChildren<CardDisplay>().thisCardsDefense)
                    {
                        playableAreas[i].GetComponentInChildren<CardDisplay>().thisCardInDefense = false;
                        playableAreas[i].transform.GetChild(0).transform.localScale = new Vector3(.7f, .45f, .8f);
                        playableAreas[i].transform.GetChild(0).transform.localRotation = Quaternion.identity;
                        playableAreas[i].transform.GetChild(0).transform.localPosition = new Vector3(35, 0, 0);
                    }
                    else
                    {
                        playableAreas[i].GetComponentInChildren<CardDisplay>().thisCardInDefense = true;
                        playableAreas[i].transform.GetChild(0).transform.localScale = new Vector3(.45f, .7f, .8f);
                        playableAreas[i].transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 90);
                        playableAreas[i].transform.GetChild(0).transform.localPosition = new Vector3(0, 20, 0);
                    }
                }
            }
            //figuring out where to place it
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


            for (int i = 0; i < cardsInHand.Count; i++)
            {
                //Choose a random card to play
                cardToChoose = Random.Range(0, cardsInHand.Count);

                string chosen = cardsInHand[cardToChoose];

          //      print(chosen);

                Card monsterCard = gameManager.FindMonster(chosen);
                //SpellCard spellCard = GameManager.instance.FindSpell(chosen);

                string cardName = "error";
                int cardCost = 0;

                if (monsterCard != null)
                {
                    cardName = monsterCard.name;
                    cardCost = monsterCard.cost;
                }
                else
                {
                    Debug.LogError("No card name found: " + chosen);
                }

                if (cardCost <= GameState.CurrencyThisTurn)
                {
                    GameState.CurrencyThisTurn -= cardCost;
                    ChooseWhereToPlayCard();
                }
                else
                {
                    i++;
                }
            }

            StartCoroutine(HoldForTime());
        }
    }

    IEnumerator HoldForTime()
    {
        yield return new WaitForSeconds(1.5f);
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

            //this is a valid location
            cardCreated = Instantiate(monsterCard, playableAreas[randomOpenPlayableSpot].transform.position, Quaternion.identity) as GameObject;
            cardCreated.transform.SetParent(playableAreas[randomOpenPlayableSpot].transform, false);

            cardCreated.GetComponentInChildren<CardDisplay>().card = Resources.Load<Card>("ScriptableObject/Monsters/" + cardsInHand[cardToChoose]) as Card;
            cardCreated.GetComponentInChildren<CardDisplay>().ReadyToInit();
            cardCreated.GetComponentInChildren<CardDisplay>().isEnemyCard = true;

            cardCreated.GetComponentInChildren<CardDisplay>().card.playedByAI = true;

            cardsInHand.RemoveAt(cardToChoose);

            //chooseAttackOrDefense
            int choosePosition = Random.Range(0, 2);

            //Defense
            if (choosePosition == 0)
            {
                cardCreated.GetComponentInChildren<CardDisplay>().thisCardInDefense = true;
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
        }
    }
    public void AttackPlayer()
    {
        StartCoroutine(AttackWithDelays());
    }

    IEnumerator AttackWithDelays()
    { 
        for (int i = 0; i < playableAreas.Length; i++)
        {
            if (playableAreas[i].transform.childCount != 0 && !playableAreas[i].GetComponentInChildren<CardDisplay>().thisCardInDefense)
            {
                //Currently only one is attacking - need one of each card to attack, with a delay
                GameManager.InitiatorCard = playableAreas[i].GetComponentInChildren<CardDisplay>();
                refInitiator = GameManager.InitiatorCard;
                print(GameManager.InitiatorCard.card.name);
                iterationCountLimit = 0;
                ChoosingPlayerCardToAttack();
                yield return new WaitForSeconds(1.25f);
            }
            else
            {
                print("Advance turn, no more cards to play");
            }

            if (i == playableAreas.Length -1)
            {
                gameState.AdvanceTurnFromAnotherScript();
            }
        }
    }

    public void ChoosingPlayerCardToAttack()
    {
     //   print("choose a random player card to attack");

        if (playerSpots[0].transform.childCount == 0 && playerSpots[1].transform.childCount == 0 && playerSpots[2].transform.childCount == 0 && playerSpots[3].transform.childCount == 0 && playerSpots[4].transform.childCount == 0 && playerSpots[5].transform.childCount == 0)
        {
            print("this is a direct attack");
            directAttack = true;
        }
        AttackingACard();
    }

    void AttackingACard()
    {
        if (!directAttack)
        {
            ChoosingPlayerCard = Random.Range(0, 6);

          //  print(ChoosingPlayerCard);

            if (playerSpots[ChoosingPlayerCard].transform.childCount != 0)
            {
        //        print("Card found");

                GameManager.InitiatorCard = refInitiator;

                if (directAttack)
                {
                    GameManager.ReceivingCard = null;
                }
                else
                {
                    GameManager.ReceivingCard = playerSpots[ChoosingPlayerCard].GetComponentInChildren<CardDisplay>();
                }

        //        print(GameManager.ReceivingCard.card.name + " " + GameManager.InitiatorCard.card.name);

                GameManager.InitiatorCard.GetComponent<CardDisplay>().AttackingEquation();

                if (directAttack)
                {
                    GameState.playerHealth -= GameManager.InitiatorCard.thisCardsAttack;
                    gameState.UpdateHealthUI();
                }

                directAttack = false;
            }
            else
            {
                print("Card not found");
            }
        }
        else
        {
            GameManager.InitiatorCard = refInitiator;

            GameManager.ReceivingCard = null;

            GameManager.InitiatorCard.GetComponent<CardDisplay>().AttackingEquation();

            GameState.playerHealth -= refInitiator.thisCardsAttack;
            //GameState.playerHealth -= GameManager.InitiatorCard.thisCardsAttack;
            gameState.UpdateHealthUI();

            directAttack = false;
        }

        GameManager.InitiatorCard = null;
        GameManager.ReceivingCard = null;
    }
}
