using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Card", menuName = "Cards/Monsters") ]
public class Card : ScriptableObject
{
    public new string name;
    public string description;
    public string type; 
    public Sprite artwork;

    public int cost;
    public int attack;
    public int defense;

}
