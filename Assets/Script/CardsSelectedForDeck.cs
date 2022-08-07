using System.Collections.Generic;
using UnityEngine;

public class CardsSelectedForDeck : MonoBehaviour
{
    public static CardsSelectedForDeck instance;
    public List<Card> monsterCards;
    public List<SpellCard> spellCards;

    private void Awake() 
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        Debug.Log("Hello");
        if (monsterCards.Count == 0 && spellCards.Count == 0) return;

        DrawACard playersDeck = FindObjectOfType<DrawACard>();
        playersDeck.monsterCards = monsterCards;
        playersDeck.spellCards = spellCards;
    }
    
    public void SelectMonsterCard(Card card)
    {
        monsterCards.Add(card);
    }

    public void SelectSpellCard(SpellCard card)
    {
        spellCards.Add(card);
    }
}
