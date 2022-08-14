using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool hasBeenPulled;
    public static bool monsterSelected;
    public static bool spellSelected;

    //Play Card from Hand
    public static bool monsterPulled, spellPulled;
    public static bool cardPlayed;
    public static string cardToBePlayed;

    //Store damage dealt
    public static int directDamage;
    public static int attackDamage;
    public static bool playerAttacking;

    public static CardDisplay InitiatorCard;
    public static CardDisplay ReceivingCard;

    //Card aftermath
    public static bool hailMary;

    // Every Card Dictionary
    public static GameManager instance;
    public List<Card> MonsterCardList;
    public List<SpellCard> SpellCardList;

    //Currency
    public static int currency = 3;

    //TotalCardsPlayerCanPull;
    public List<Card> MonsterCardsToBePulled;
    public List<SpellCard> SpellCardsToBePulled;

    public List<Card> MonsterCardsOwned;
    public List<SpellCard> SpellCardsOwned;

    private void Start()
    {
        instance = this;
    }

    public Card FindMonster(string cardName)
    {
        for (int i = 0; i < MonsterCardList.Count; i++)
        {
            if (MonsterCardList[i].name == cardName)
            {
                return MonsterCardList[i];
            }
        }
        return null;
    }

    public SpellCard FindSpell(string cardName)
    {
        for (int i = 0; i < SpellCardList.Count; i++)
        {
            if (SpellCardList[i].name == cardName)
            {
                return SpellCardList[i];
            }
        }
        return null;
    }
}
