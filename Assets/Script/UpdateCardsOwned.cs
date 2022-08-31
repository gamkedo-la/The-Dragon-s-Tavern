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
    public Transform selectedContentHolder;

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
        foreach (Transform child in selectedContentHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        List<Card> monsterCardsAvailable = new List<Card>(gameManager.MonsterCardsOwned);
        List<SpellCard> spellCardsAvailable = new List<SpellCard>(gameManager.SpellCardsOwned);

        for(int i = 0; i < CardsSelectedForDeck.instance.monsterCards.Count; i++){

            GameObject monsterCardCreated = Instantiate(monsterCard, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            monsterCardCreated.GetComponentInChildren<HubCardDisplay>().monsterCard = CardsSelectedForDeck.instance.monsterCards[i];
            monsterCardCreated.GetComponentInChildren<HubCardDisplay>().isInSelectedArea = true;

            monsterCardCreated.transform.SetParent(selectedContentHolder.transform, false);
            monsterCardCreated.transform.localScale = new Vector3(1, 1, 1);

            monsterCardsAvailable.Remove(CardsSelectedForDeck.instance.monsterCards[i]);
        }

        for(int i = 0; i < CardsSelectedForDeck.instance.spellCards.Count; i++){
            GameObject spellCardCreated = Instantiate(spellCard, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            spellCardCreated.GetComponentInChildren<HubCardDisplay>().spellCard = CardsSelectedForDeck.instance.spellCards[i];
            spellCardCreated.GetComponentInChildren<HubCardDisplay>().isInSelectedArea = true;

            spellCardCreated.transform.SetParent(selectedContentHolder.transform, false);
            spellCardCreated.transform.localScale = new Vector3(1, 1, 1);

            spellCardsAvailable.Remove(CardsSelectedForDeck.instance.spellCards[i]);

        }


        for (int i = 0; i < spellCardsAvailable.Count; i++)
        {
            GameObject spellCardCreated = Instantiate(spellCard, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            spellCardCreated.GetComponentInChildren<HubCardDisplay>().spellCard = spellCardsAvailable[i];

            spellCardCreated.transform.SetParent(contentHolder.transform, false);
            spellCardCreated.transform.localScale = new Vector3(1, 1, 1);
        }

        for (int i = 0; i < monsterCardsAvailable.Count; i++)
        {
            GameObject monsterCardCreated = Instantiate(monsterCard, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            monsterCardCreated.GetComponentInChildren<HubCardDisplay>().monsterCard = monsterCardsAvailable[i];

            monsterCardCreated.transform.SetParent(contentHolder.transform, false);
            monsterCardCreated.transform.localScale = new Vector3(1, 1, 1);
        }

        

    }
}
