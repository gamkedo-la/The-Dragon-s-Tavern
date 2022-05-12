using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    //This section is to determine the card flip speed with a bool to skip it if it is annoying
    public bool skipCardFlip;
    public GameObject backOfCard;
    float rotatedAngle = 180;
    bool rotation;
    public float rotationSpeed;
    //

    //Determines if it is a spell or a monster
    public bool isMonster;
    public Card card;
    public SpellCard spellCard;
    //

    //Needed to display the card info on the card itself
    public Image background;
    public Text title;
    public Text desc;
    public Text def;
    public Text att;
    public Text cost;
    public Text type;
    //

    //Hovering over card and pulling card from hand
    public Vector3 offset = new Vector3(0, 30, 0);
    public Vector3 pullFromHand = new Vector3(0, -175f, 0);

    bool hasEntered;
    bool hasBeenPulled;
    //
    void Start()
    {
        //Initial display of information
        if (isMonster)
        {
            title.text = card.name;
            desc.text = card.description;
            def.text = card.defense.ToString();
            att.text = card.attack.ToString();
            cost.text = card.cost.ToString();
            type.text = card.type;
            background.sprite = card.artwork;
        }
        else
        {
            title.text = spellCard.name;
            desc.text = spellCard.effect;
            type.text = spellCard.type;
            background.sprite = spellCard.artwork;
        }
        //

        //to skip or play the card animation
        if (skipCardFlip)
        {

        }
        else
        {
            backOfCard.SetActive(true);
            this.gameObject.transform.localRotation = Quaternion.Euler(0, rotatedAngle, 0);
            rotation = true;
        }
        //
    }

    private void Update()
    {
        //Rotating the cards once they are drawn
        if (rotation)
        {
            rotatedAngle -= rotationSpeed;
            this.gameObject.transform.localRotation = Quaternion.Euler(0, rotatedAngle, 0);
            if (rotatedAngle <= 90)
            {
                backOfCard.SetActive(false);
            }
            if (rotatedAngle <= 0)
            {
                rotatedAngle = 0;
                rotation = false;
                skipCardFlip = false;
            }
        }
        //

        // pull card from hand
        if (Input.GetMouseButtonDown(0) && hasEntered && !hasBeenPulled)
        {
            this.gameObject.transform.localPosition -= pullFromHand;
            GameObject.Find("PlayerHand").transform.localPosition += pullFromHand;
            hasBeenPulled = true;
            if (isMonster)
            {
                GameManager.cardName = card.name;
                GameManager.monsterSelected = true;
            }
            else
            {
                GameManager.cardName = spellCard.name;
                GameManager.spellSelected = true;
            }
            UpdateGameManager();
        }
        //

        //putting card back into hand
        if (Input.GetMouseButtonDown(1) && hasEntered && hasBeenPulled)
        {
            this.gameObject.transform.localPosition += pullFromHand;
            GameObject.Find("PlayerHand").transform.localPosition -= pullFromHand;
            hasBeenPulled = false;
            ClearGameManager();
        }
        //
    }

    public void CardHoverEnter()
    {
        if (!hasBeenPulled)
        {
            this.gameObject.transform.localPosition += offset;
            hasEntered = true;
        }
    }

    public void CardHoverExit()
    {
        if (!hasBeenPulled)
        {
            this.gameObject.transform.localPosition -= offset;
            hasEntered = false;
        }
    }

    public void OnMouseOver()
    {
        if (hasBeenPulled)
        {
            print(gameObject.name);
        }
    }

    void UpdateGameManager()
    {
        GameManager.hasBeenPulled = true;
        if (isMonster)
        {
            GameManager.cardName = card.name;
            GameManager.description = card.description;
            GameManager.defense = card.defense;
            GameManager.attack = card.attack;
            GameManager.cost = card.cost;
            GameManager.type = card.type;
            GameManager.artwork = card.artwork;
        }
        else
        {
            GameManager.spellName = spellCard.name;
            GameManager.effect = spellCard.effect;
            GameManager.spellType = spellCard.type;
            GameManager.spellArtwork = spellCard.artwork;
        }
    }

    void ClearGameManager()
    {
        GameManager.monsterSelected = false;
        GameManager.spellSelected = false;
        GameManager.hasBeenPulled = false;

        GameManager.cardName = null;
        GameManager.description = null;
        GameManager.defense = 0;
        GameManager.attack = 0;
        GameManager.cost = 0;
        GameManager.type = null;
        GameManager.artwork = null;
        GameManager.spellName = null;
        GameManager.effect = null;
        GameManager.spellType = null;
        GameManager.spellArtwork = null;
    }
}
