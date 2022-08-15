using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCardsOwned : MonoBehaviour
{
    int CardsToCreate = 10;
    public GameObject monsterCard;
    public GameObject spellCard;

    public Transform contentHolder;

    void Start()
    {
        //Refresh the List

        for (int i = 0; i < CardsToCreate; i++)
        {
            spellCard = Instantiate(spellCard, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            spellCard.transform.parent = contentHolder.transform;
            spellCard.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
