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

    //for Battle
    public bool isEnemyCard;
    int playerAttack;
    int playerDefense;
    int enemyAttack;
    int enemyDefense;
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

    //Once it is on the table
    public bool hasBeenPlayed;
    public bool inDefense;
    //

    Transform playerCardPlacementOnTableParent;
    Transform enemyCardPlacementOnTableParent;

    bool monsterTargeted;

    public string NameOfCard()
    {
        if (isMonster)
        {
            return card.name;
        }
        else
        {
            return spellCard.name;
        }
    }

    public void ReadyToInit()
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
            GameManager.hasBeenPulled = true;
            if (isMonster)
            {
                GameManager.monsterPulled = true;
                GameManager.cardToBePlayed = card.name;
                GameManager.monsterSelected = true;
            }
            else
            {
                GameManager.spellPulled = true;
                GameManager.cardToBePlayed = spellCard.name;
                GameManager.spellSelected = true;
            }
        }
        //

        //putting card back into hand
        if (Input.GetMouseButtonDown(1) && hasEntered && hasBeenPulled)
        {
            this.gameObject.transform.localPosition += pullFromHand;
            PutCardBackInHand();
        }
        //

        //checking if Card has been played (referenced in PlayableSpot.cs
        if (hasBeenPulled && GameManager.cardPlayed)
        {
            PutCardBackInHand();
            GameManager.cardPlayed = false;
            Destroy(transform.parent.gameObject);
        }

        if (Input.GetMouseButtonDown(1) && isMonster && hasBeenPlayed && GameObject.Find("GameState").GetComponent<GameState>().gamePhase == 2)
        {
            inDefense = !inDefense;
            FlipCardPosition();
        }
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

        void ClearGameManager()
    {
        GameManager.monsterSelected = false;
        GameManager.spellSelected = false;
        GameManager.hasBeenPulled = false;
    }

    public void PutCardBackInHand()
    {
        GameObject.Find("PlayerHand").transform.localPosition -= pullFromHand;
        hasBeenPulled = false;
        ClearGameManager();
    }

    public void FlipCardPosition()
    {
        if (inDefense)
        {
            print("Im in defense!");
            this.transform.eulerAngles = new Vector3(90, 0, 90); 
        }
        else
        {
            print("Im in attack!");
            this.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }

    #region SpellCards
    public void ActivateQuickSpell()
    {
        if (spellCard.name == "Diamond of Gold")
        {
            DiamondOfGold();
        }
        else if (spellCard.name == "Book of Spells")
        {
            BookOfSpells();
        }
        else if (spellCard.name == "Charasmatic Speech")
        {
            CharasmaticSpeech();
        }
        else if (spellCard.name == "Ancestral Lore")
        {
            AncestralLore();
        }
    }

    public void AncestralLore()
    {
        //Scoop up cards in the parent
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            if (cardsOnTable[i].card.type.ToString() == "Lore")
            {
                int totalCards = cardsOnTable.Length;

                cardsOnTable[i].card.defense += totalCards;
                cardsOnTable[i].def.text = cardsOnTable[i].card.defense.ToString();
            }
        }
        StartCoroutine(RemovePlayedCard());
    }

    public void CharasmaticSpeech()
    {
        //Scoop up cards in the parent
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;
        CardDisplay[] cardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            print(cardsOnTable[i]);
            cardsOnTable[i].card.attack -= 1;
            cardsOnTable[i].att.text = cardsOnTable[i].card.attack.ToString();
            print(cardsOnTable[i].card.attack);
        }
        StartCoroutine(RemovePlayedCard());
    }


    public void BookOfSpells()
    {
        //Scoop up cards in the parent
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

         for (int i = 0; i < cardsOnTable.Length; i++)
         {
             if (cardsOnTable[i].type.ToString() == "Spellcaster")
             {
                print(cardsOnTable[i]);
                cardsOnTable[i].card.attack += 2;
                 cardsOnTable[i].att.text = cardsOnTable[i].card.attack.ToString();
                print(cardsOnTable[i].card.attack);
             }
         }
        StartCoroutine(RemovePlayedCard());
    }

    public void DiamondOfGold()
    {
        GameState.CurrencyThisTurn += 3;
        GameObject.Find("GameState").GetComponent<GameState>().UpdateCardValueUI();
        StartCoroutine(RemovePlayedCard());
    }

    IEnumerator RemovePlayedCard()
    {
        yield return new WaitForSeconds(2f);
        //Wait after card has been placed on table

        yield return new WaitForSeconds(1f);
        //Enhance card to show 1) what card 2) card description

        //Unparent card

        //Move card to graveyard

        //For now, just destroy it
        Destroy(transform.parent.gameObject);
    }

    #endregion

    #region Monster Attack

    public void CardClicked()
    {
        if (isMonster && hasBeenPlayed && GameObject.Find("GameState").GetComponent<GameState>().gamePhase == 2)
        {
            monsterTargeted = true;

            UpdateCardColors();
        }
    }

    public void CardAttackingOtherCard(Button buttonName)
    {
        if (monsterTargeted)
        {
            playerAttack = this.card.attack;

            print(buttonName.GetComponent<CardDisplay>().card.defense);

            CardDisplay[] cardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

            for (int i = 0; i < cardsOnTable.Length; i++)
            {
                cardsOnTable[i].GetComponent<CardDisplay>().card.attack -= playerAttack = this.card.attack;
                print(cardsOnTable[i].card.attack);
                cardsOnTable[i].att.text = cardsOnTable[i].card.attack.ToString();
                if (cardsOnTable[i].GetComponent<CardDisplay>().card.attack <= 0)
                {
                    print("Remove from field");
                }
            }
         /*   if (isEnemyCard)
            {
                if (inDefense)
                {
                    print(buttonName.GetComponent<CardDisplay>().card.defense);
                }
                else
                {
                    print(buttonName.GetComponent<CardDisplay>().card.attack);
                }
                
            }*/
            monsterTargeted = false;
            UpdateCardColors();
        }
    }

    void UpdateCardColors()
    {
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            if (cardsOnTable[i].GetComponent<CardDisplay>().isMonster)
            {
                if (monsterTargeted)
                {
                    cardsOnTable[i].GetComponent<Image>().color = Color.red;
                }
                else
                {
                    cardsOnTable[i].GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    #endregion
}
