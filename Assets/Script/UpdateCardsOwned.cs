using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCardsOwned : MonoBehaviour
{
    int CardsToCreate = 10;
    public GameObject monsterCard;
    public GameObject spellCard;

    public Transform contentHolder;

    public GameManager gameManager;

    void Start()
    {
        //Refresh the List
        RefreshList();
    }

    public void RefreshList()
    {
        foreach (Transform child in contentHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < gameManager.SpellCardsOwned.Count; i++)
        {
            GameObject spellCardCreated = Instantiate(spellCard, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            spellCardCreated.GetComponentInChildren<HubCardDisplay>().spellCard = gameManager.SpellCardsOwned[i];

            spellCardCreated.transform.parent = contentHolder.transform;
            spellCardCreated.transform.localScale = new Vector3(1, 1, 1);
        }

        for (int i = 0; i < gameManager.MonsterCardsOwned.Count; i++)
        {
            GameObject monsterCardCreated = Instantiate(monsterCard, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            monsterCardCreated.GetComponentInChildren<HubCardDisplay>().monsterCard = gameManager.MonsterCardsOwned[i];

            monsterCardCreated.transform.parent = contentHolder.transform;
            monsterCardCreated.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
