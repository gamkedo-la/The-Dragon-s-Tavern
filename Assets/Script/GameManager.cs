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
}
