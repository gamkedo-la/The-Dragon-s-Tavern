using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/Spell")]
public class SpellCard : ScriptableObject
{
    public new string name;
    public string effect;
    public string type;
    public Sprite artwork;

    //Not sure how to do effects, but maybe they go here?

}