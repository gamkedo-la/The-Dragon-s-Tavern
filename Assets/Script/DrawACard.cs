using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawACard : MonoBehaviour
{
    public GameObject[] cards;
    public Transform playerHand;

    private void OnMouseOver()
    {
        //This draws a new card from the player's deck
            //Right now, it is pulling the same gameobject. Eventually, this will be a dynamic list
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newCard = Instantiate(cards[0], playerHand.transform.position, Quaternion.identity);
            newCard.transform.parent = playerHand;
            newCard.transform.localScale = new Vector3(1, 1, 1);
            newCard.transform.localRotation = Quaternion.identity;
        }
    }
}
