using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        //The initial Wait After Player draws their hand to draw a 6th card
        StartCoroutine(InitialWait());
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Advance Battle State
            gamePhase++;
            
            DetermineTurn();
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

    void PlayerDraw()
    {
        displayCurrentState.text = "Player Draw";

        gamePhase++;
        StartCoroutine(WaitForPlayerDraw());
    }

    IEnumerator WaitForPlayerDraw()
    {
        yield return new WaitForSeconds(1f);
        playerDeck.GetComponent<DrawACard>().DrawCard();
        DetermineTurn();
    }

    void PlayerSet()
    {
        displayCurrentState.text = "Player Set";
        playerEndSetButton.SetActive(true);
    }

    void PlayerAttack()
    {
        //Scoop up cards in the parent
        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();
       
        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            Debug.Log(cardsOnTable[i].NameOfCard());
        }

        //this should check the rules between the cards, may reference functions from other cards
        //the actual doing of the stuff should be on the card display (updating HP or functions)

        displayCurrentState.text = "Player Attack";
        playerEndAttackButton.SetActive(true);
    }

    void PlayerEnd()
    {
        displayCurrentState.text = "Player End";
        StartCoroutine(CycleTurnThisIsTemp());
    }

    void AIDraw()
    {
        displayCurrentState.text = "AI Draw";
        StartCoroutine(CycleTurnThisIsTemp());
    }

    void AISet()
    {
        displayCurrentState.text = "AI Set";
        StartCoroutine(CycleTurnThisIsTemp());
    }

    void AIAttack()
    {
        displayCurrentState.text = "AI Attack";
        StartCoroutine(CycleTurnThisIsTemp());
    }

    void AIEnd()
    {
        displayCurrentState.text = "AI End";
        StartCoroutine(CycleTurnThisIsTemp());
    }

    IEnumerator CycleTurnThisIsTemp()
    {
        yield return new WaitForSeconds(2f);
        gamePhase++;
        DetermineTurn();
    }

    //UI Buttons to advance Player Turns
    public void EndSet()
    {       
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
}
