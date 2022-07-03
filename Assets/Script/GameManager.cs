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
}
