using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayableSpot : MonoBehaviour
{
    bool isHovering;
    Card card;
    SpellCard spellCard;


    //Needed to display the card info on the card itself
    public Image backgroundImage;
    public Text title;
    public Text desc;
    public Text def;
    public Text att;
    public Text cost;
    public Text type;
    public Image backgroundColor;
    public Color monsterColor, spellColor;
    //

    public void CardZoneEnter()
    {
        isHovering = true;
    }
    public void CardZoneExit()
    {
        isHovering = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hovering fine");

            //Checking Placement Spot
            print(gameObject.name);
            if (GameManager.monsterSelected)
            {
                print(GameManager.cardName);

                  card.name = GameManager.cardName.ToString();

                  title.text = GameManager.name;
                  desc.text = GameManager.description;
                  def.text = GameManager.defense.ToString();
                  att.text = GameManager.attack.ToString();
                  cost.text = GameManager.cost.ToString();
                  type.text = GameManager.type;
                  backgroundImage.sprite = GameManager.artwork;
                  backgroundColor.GetComponent<Image>().color = monsterColor;
                

                //Instantiate Monster (GameManager.name)
            }
            if (GameManager.spellSelected)
            {
                spellCard.name = GameManager.cardName;

                title.text = spellCard.name;
                desc.text = spellCard.effect;
                type.text = spellCard.type;
                backgroundImage.sprite = spellCard.artwork;
                backgroundColor.GetComponent<Image>().color = spellColor;
                //Instantiate Spell (GameManager.name)
            }
            GameManager.monsterSelected = false;
            GameManager.spellSelected = false;
            GameManager.cardName = "";

            //Checking if card can be placed here
            //If spell - no
            //if Monster - tribute (gain playable points equal to monster's value)

            //Instantiate Card on table

            //Destroy Card in hand

            //Update costs
        }
        else
        {
           // GetComponent<Image>().material.color = Color.white;
        }
    }

}
