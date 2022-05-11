using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool hasBeenPulled;
    public static bool monsterSelected;
    public static bool spellSelected;
    //Monsters
    public static string cardName;
    public static new string name;
    public static string description;
    public static string type;
    public static Sprite artwork;
    public static int cost;
    public static int attack;
    public static int defense;

    //Spells
    public static string spellName;
    public static string effect;
    public static string spellType;
    public static Sprite spellArtwork;

    //Player's Card Drawn Name
    public static string cardNameToReference;
}
