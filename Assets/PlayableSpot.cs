using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayableSpot : MonoBehaviour
{
    bool isOpen;

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
