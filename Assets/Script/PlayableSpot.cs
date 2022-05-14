using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayableSpot : MonoBehaviour
{
    bool isOpen;
    public GameObject monsterCard, spellCard;
    public GameObject spellPulledFX, monsterPulledFX, occupiedFX;
    string cardToRecall;
    GameObject cardCreated;

    private void Start()
    {
        isOpen = true;
    }

    private void Update()
    {
        if (GameManager.hasBeenPulled && isOpen)
        {
            this.GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        bool justPulledACard = false;

        //if space is unoccupied
        if (GameManager.hasBeenPulled && isOpen)
        {
            //update currency for turn

            //animation of card from hand to table

            //create monster or spell card
            if (GameManager.spellPulled)
            {
                cardCreated = Instantiate(spellCard, transform.position, Quaternion.identity) as GameObject;
                if (spellPulledFX) Instantiate(spellPulledFX, transform.position, transform.rotation);

                //Recalling the correct card
                cardToRecall = GameManager.cardToBePlayed.ToString();
                cardToRecall.Replace(" (SpellCard)", "");

                print(cardToRecall);

                cardCreated.GetComponentInChildren<CardDisplay>().spellCard = Resources.Load<SpellCard>("ScriptableObject/Spell/" + cardToRecall) as SpellCard;

                cardCreated.GetComponentInChildren<CardDisplay>().ReadyToInit();

               GameManager.spellPulled = false;
               justPulledACard = true;
            }
            if (GameManager.monsterPulled)
            {
                cardCreated = Instantiate(monsterCard, transform.position, Quaternion.identity) as GameObject;

                //Recalling the correct card
                cardToRecall = GameManager.cardToBePlayed.ToString();
                cardToRecall.Replace(" (Card)", "");

                print(cardToRecall);

                cardCreated.GetComponentInChildren<CardDisplay>().card = Resources.Load<Card>("ScriptableObject/Monsters/" + cardToRecall) as Card;

                cardCreated.GetComponentInChildren<CardDisplay>().ReadyToInit();

                if (GameState.CurrencyThisTurn >= cardCreated.GetComponentInChildren<CardDisplay>().card.cost)
                {
                    print(cardCreated.GetComponentInChildren<CardDisplay>().card.name + " " + cardCreated.GetComponentInChildren<CardDisplay>().card.cost);
                    GameState.CurrencyThisTurn -= cardCreated.GetComponentInChildren<CardDisplay>().card.cost;
                    GameState.playerPointsDisplay.GetComponent<Text>().text = GameState.CurrencyThisTurn.ToString();


                    if (monsterPulledFX) Instantiate(monsterPulledFX, transform.position, transform.rotation);

                    GameManager.monsterPulled = false;
                    justPulledACard = true;

                    cardCreated.transform.parent = this.gameObject.transform;
                    cardCreated.transform.localScale = new Vector3(.7f, .45f, .8f);
                    cardCreated.transform.localRotation = Quaternion.identity;
                    cardCreated.transform.localPosition = new Vector3(35, 0, 0);

                    GameManager.cardToBePlayed = "";

                    //remove card from hand
                    GameManager.cardPlayed = true;
                    //See CardDisplay.cs, if cardPlayed = true, CardDisplay will destroy the card

                    //update status of playable spot
                    isOpen = false;
                }

                //not enough points to play card
                else
                {
                    Debug.Log("Not enough currency");

                    //Prevent Card From Playing

                    //Change Color to Red then back to yellow 
                }
            }
        }

        // this would fire if we JUST placed a card above without the extra bool
        //if space occupied by monster
        if (GameManager.hasBeenPulled && !isOpen && !justPulledACard)
        {
            //UI - do you want to tribute?
            if (occupiedFX) Instantiate(occupiedFX, transform.position, transform.rotation);
        }

        //if space occupied by spell - FIXME - implement as above
        //if (occupiedFX) Instantiate(occupiedFX, transform.position, Quaternion.identity);

    }

    private void OnMouseOver()
    {
        //if space is occupied - shower enlarged version of card off to side of table
    }
}
