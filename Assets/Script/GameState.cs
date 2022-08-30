using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GamePhases { PlayerDraw, PlayerSet, PlayerAttack, PlayerEnd, AIDraw, AISet, AIAttack, AIEnd }

public class GameState : MonoBehaviour
{
    public GamePhases state;
    public int gamePhase;

    public GameObject playerDeck;

    public Text displayCurrentState;
    public Animator CurrentStateUIImage;

    public GameObject playerEndSetButton;
    public GameObject playerEndAttackButton;

    //This is checking the players/enemies cards on table
    public Transform playerCardPlacementOnTableParent;
    public Transform enemyCardPlacementOnTableParent;
    //

    //Checking player currency per turn 
    public static int CurrencyThisTurn;
    public static GameObject playerPointsDisplay;
    public GameObject playerPointsImage;

    public Color startingColor = Color.yellow;
    public Image currencyBackground;
    public Color notEnoughCurrencyColor = Color.red;
    float tooLowOfCurrency = .25f;
    bool tooLowOfCurrencyTrigger;
    //

    public Animator playerCamera;
    public GameObject playerHandUI;

    //Opponent Hand
    public OpponentHand opponentHand;

    CardDisplay cardDisplay;

    //Life Points
    public Text playerHealthUI, opponentHealthUI;
    public static int playerHealth, opponentHealth; 

    public static int turnCount;

    //advance Player turn off of draw
    bool advanceTurn;

    public bool isHub;

    public GameObject playerLoss, playerWin;
    public Text winningsShown, winningsShownLoss;

    GameManager gameManager;

    public string sameRoom, lobby;

    public bool outOfCards, opponentOutOfCards;

    public GameObject opponentDeck;

    private void Start()
    {
        //The initial Wait After Player draws their hand to draw a 6th card

        if (!isHub)
        {
            StartCoroutine(InitialWait());

            playerHealth = opponentHealth = 15;
            UpdateHealthUI();
            turnCount = 0;

            //Card remnants from previous duels
            GameManager.hailMary = false;
            advanceTurn = true;
            GameManager.gameManager.DirectAttackButton = GameObject.Find("DirectAttack");
            GameManager.gameManager.DirectAttackButton.SetActive(false);
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    IEnumerator InitialWait()
    {
        //Wait 4 seconds from start. Long enough for player to draw 5 cards from hand, then draw a 6th. 
        yield return new WaitForSeconds(4f);
        displayCurrentState.text = "Player Draw";
        state = GamePhases.PlayerDraw;
        DetermineTurn();
    }

    private void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.A))
        {
            //Advance Battle State
            gamePhase++;
            
            DetermineTurn();
        }*/

        if (tooLowOfCurrencyTrigger)
        {
            tooLowOfCurrency -= Time.deltaTime;
            currencyBackground.color = Color.Lerp(notEnoughCurrencyColor, startingColor, Mathf.PingPong(Time.time * 10, 1));
            if (tooLowOfCurrency <= 0)
            {
                currencyBackground.color = startingColor;
                tooLowOfCurrencyTrigger = false;
                tooLowOfCurrency = .25f;
            }
        }
    }

    void DetermineTurn()
    {
        CurrentStateUIImage.SetTrigger("Play");

        if (gamePhase > 7)
        {
            gamePhase = 0;
        }

        switch (gamePhase)
        {
            case 0:
                state = GamePhases.PlayerDraw;
                PlayerDraw();
                break;
            case 1:
                state = GamePhases.PlayerSet;
                PlayerSet();
                break;
            case 2:
                state = GamePhases.PlayerAttack;
                PlayerAttack();
                break;
            case 3:
                state = GamePhases.PlayerEnd;
                PlayerEnd();
                break;
            case 4:
                state = GamePhases.AIDraw;
                AIDraw();
                break;
            case 5:
                state = GamePhases.AISet;
                AISet();
                break;
            case 6:
                state = GamePhases.AIAttack;
                AIAttack();
                break;
            case 7:
                state = GamePhases.AIEnd;
                AIEnd();
                break;
        }
    }

    public void PlayerDraw()
    {
        if (advanceTurn)
        {
            displayCurrentState.text = "Player Draw";

            turnCount++;

            gamePhase++;
        }
        StartCoroutine(WaitForPlayerDraw());
    }

    IEnumerator WaitForPlayerDraw()
    {
        yield return new WaitForSeconds(1f);
        playerDeck.GetComponent<DrawACard>().DrawCard();
        if (advanceTurn)
        {
            DetermineTurn();
        }
        advanceTurn = false;
    }

    void PlayerSet()
    {
        displayCurrentState.text = "Player Set";
        playerEndSetButton.SetActive(true);

        playerPointsImage.SetActive(true);

        //Turn interaction of cards on for cards that were on the table at the start of the turn (tributable)
        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            cardsOnTable[i].TurnPlayerInteractableCardsOn();
        }

        CurrencyThisTurn = 4;
        UpdateCardValueUI();
    }

    public void UpdateCardValueUI()
    {
        playerPointsDisplay = GameObject.Find("PlayerCurrencyThisTurnText");
        playerPointsDisplay.GetComponent<Text>().text = CurrencyThisTurn.ToString();
    }

    void PlayerAttack()
    {
        playerCamera.SetTrigger("Inward");
        playerHandUI.SetActive(false);

        playerPointsImage.SetActive(false);

        //Scoop up cards in the parent
        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        if (!GameManager.hailMary)
        {
            for (int i = 0; i < cardsOnTable.Length; i++)
            {
                cardsOnTable[i].TurnPlayerInteractableCardsOn();
            }

            CardDisplay[] opponentCardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();
            for (int i = 0; i < opponentCardsOnTable.Length; i++)
            {
                opponentCardsOnTable[i].TurnEnemyInteractableCardsOn();
            }
        }
        else
        {
            for (int i = 0; i < cardsOnTable.Length; i++)
            {
                cardsOnTable[i].TurnPlayerInteractableCardsOff();
                cardsOnTable[i].inDefense = true;
                cardsOnTable[i].transform.eulerAngles = new Vector3(90, 0, 90);
                print("gamestate");
            }
            GameManager.hailMary = false;
        }


        //this should check the rules between the cards, may reference functions from other cards
        //the actual doing of the stuff should be on the card display (updating HP or functions)

        displayCurrentState.text = "Player Attack";
        playerEndAttackButton.GetComponent<Button>().interactable = true;
        playerEndAttackButton.SetActive(true);
    }

    void PlayerEnd()
    {
        //Scoop up cards in the parent
        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            cardsOnTable[i].TurnPlayerInteractableCardsOff();
        }

        displayCurrentState.text = "Player End";
        StartCoroutine(CycleTurnThisIsTemp());
    }

    void AIDraw()
    {
            displayCurrentState.text = "AI Draw";
            opponentHand.DrawACard();
            StartCoroutine(CycleTurnThisIsTemp());
    }

    void AISet()
    {
        if (opponentOutOfCards)
        {
            opponentHand.GetComponent<OpponentHand>().enabled = false ;
        }

        else
        {
            CurrencyThisTurn = 6;

            displayCurrentState.text = "AI Set";
            opponentHand.PlayHand();
        }
    }

    void AIAttack()
    {
        if (opponentOutOfCards)
        {
            opponentHand.GetComponent<OpponentHand>().enabled = false;
            print("here");
        }

        else
        {
            displayCurrentState.text = "AI Attack";

            opponentHand.AttackPlayer();

            // StartCoroutine(CycleTurnThisIsTemp());
        }
    }

    void AIEnd()
    {
        playerCamera.SetTrigger("Outward");
        StartCoroutine(WaitForSeconds(1f));
        displayCurrentState.text = "AI End";
        advanceTurn = true;
        StartCoroutine(CycleTurnThisIsTemp());
    }

    IEnumerator CycleTurnThisIsTemp()
    {
        yield return new WaitForSeconds(2f);
    //    print("here");
        gamePhase++;
        DetermineTurn();
    }

    public void AdvanceTurnFromAnotherScript()
    {
        gamePhase++;
        DetermineTurn();
    }

    //UI Buttons to advance Player Turns
    public void EndSet()
    {       
        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            cardsOnTable[i].NoTribute();
        }
        gamePhase++;
        DetermineTurn();
        playerEndSetButton.SetActive(false);
    }

    public void EndAttack()
    {
        gamePhase++;
        DetermineTurn();
        playerEndAttackButton.SetActive(false);
    }
    //

    //Change Color if not enough Currency
    public void ChangeColor()
    {
        currencyBackground.color = notEnoughCurrencyColor;
        tooLowOfCurrencyTrigger = true;
    }

    public void UpdateHealthUI()
    {
        playerHealthUI.text = playerHealth.ToString();
        opponentHealthUI.text = opponentHealth.ToString();

        TriggerWinCondition();

    }

    IEnumerator WaitForSeconds(float seconds)
    { 
        yield return new WaitForSeconds(seconds);
        playerHandUI.SetActive(true);
    }

    public void TriggerWinCondition(bool resign = false)
    {
        if (opponentHealth <= 0 || opponentOutOfCards)
        {
            print("Player Wins!");
            playerWin.SetActive(true);
            int additionalCurrency = Random.Range(3, 6);
            GameManager.currency += additionalCurrency;
            winningsShown.text = additionalCurrency.ToString();
            opponentOutOfCards = false;
        }
        else if (playerHealth <= 0 || outOfCards || resign)
        {
            print("Opponent Wins!");
            playerLoss.SetActive(true);
            GameManager.currency += 1;
            winningsShownLoss.text = "1";
            outOfCards = false;
        }
    }

    public void RestartRoom()
    {
       // gameManager.SaveGame();
        SceneManager.LoadScene(sameRoom);
    }

    public void ReturnToLobby()
    {
       // gameManager.SaveGame();
        SceneManager.LoadScene(lobby);
    }
}
