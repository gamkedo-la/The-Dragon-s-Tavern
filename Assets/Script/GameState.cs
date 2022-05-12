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
            if (gamePhase > 7)
            {
                gamePhase = 0;
            }
            DetermineTurn();
        }
    }

    void DetermineTurn()
    {
        CurrentStateUIImage.SetTrigger("Play");

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
        playerDeck.GetComponent<DrawACard>().DrawCard();

        gamePhase++;
        DetermineTurn();
    }

    void PlayerSet()
    {
        displayCurrentState.text = "Player Draw";
    }

    void PlayerAttack()
    {
        displayCurrentState.text = "Player Attack";
    }

    void PlayerEnd()
    {
        displayCurrentState.text = "Player End";
    }

    void AIDraw()
    {
        displayCurrentState.text = "AI Draw";
    }

    void AISet()
    {
        displayCurrentState.text = "AI Set";
    }

    void AIAttack()
    {
        displayCurrentState.text = "AI Attack";
    }

    void AIEnd()
    {
        displayCurrentState.text = "AI End";
    }

}
