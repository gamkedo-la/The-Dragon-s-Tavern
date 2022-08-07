using System.Collections.Generic;
using UnityEngine;

public class CardsSelectedForDeck : MonoBehaviour
{
    public List<Card> monsterCards;
    public List<SpellCard> spellCards;
    
    public void SelectMonsterCard(Card card)
    {
        monsterCards.Add(card);
    }

    public void SelectSpellCard(SpellCard card)
    {
        spellCards.Add(card);
    }
}
