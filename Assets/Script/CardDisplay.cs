using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public bool isMonster;

    public Card card;
    public SpellCard spellCard;

    public Image background;
    public Text title;
    public Text desc;
    public Text def;
    public Text att;
    public Text cost;
    public Text type;

    void Start()
    {
        if (isMonster)
        {
            title.text = card.name;
            desc.text = card.description;
            def.text = card.defense.ToString();
            att.text = card.attack.ToString();
            cost.text = card.cost.ToString();
            type.text = card.type;
        }
        else
        {
            title.text = spellCard.name;
            desc.text = spellCard.effect;
            type.text = spellCard.type;
        }
    }
}
