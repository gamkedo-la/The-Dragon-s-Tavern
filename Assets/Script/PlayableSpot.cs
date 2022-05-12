using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayableSpot : MonoBehaviour
{
    bool isOpen;
    public GameObject monsterCard, spellCard;
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
        //if space is unoccupied
        if (GameManager.hasBeenPulled && isOpen)
        {
            //update currency for turn

            //animation of card from hand to table

            //create monster or spell card
            if (GameManager.spellPulled)
            {
                cardCreated = Instantiate(spellCard, transform.position, Quaternion.identity) as GameObject;

                //Recalling the correct card
                cardToRecall = GameManager.cardToBePlayed.ToString();
                cardToRecall.Replace(" (SpellCard)", "");
                cardCreated.GetComponentInChildren<CardDisplay>().spellCard = Resources.Load<SpellCard>("ScriptableObject/Spell/" + cardToRecall) as SpellCard;

                GameManager.spellPulled = false;
            }
            if (GameManager.monsterPulled)
            {
                cardCreated = Instantiate(monsterCard, transform.position, Quaternion.identity) as GameObject;

                //Recalling the correct card
                cardToRecall = GameManager.cardToBePlayed.ToString();
                cardToRecall.Replace(" (Card)", "");
                cardCreated.GetComponentInChildren<CardDisplay>().card = Resources.Load<Card>("ScriptableObject/Monsters/" + cardToRecall) as Card;

                GameManager.monsterPulled = false;
            }
            
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

        //if space occupied by monster
        if (GameManager.hasBeenPulled && !isOpen)
        {
            //UI - do you want to tribute?
        }

        //if space occupied by spell
    }

    private void OnMouseOver()
    {
        //if space is occupied - shower enlarged version of card off to side of table
    }
}