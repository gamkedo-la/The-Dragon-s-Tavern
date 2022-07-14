using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //This section is to determine the card flip speed with a bool to skip it if it is annoying
    public bool isInitializedFromStart = false;
    public bool skipCardFlip;
    public GameObject backOfCard;
    float rotatedAngle = 180;
    bool rotation;
    public float rotationSpeed;
    //

    //for Battle
    public bool isEnemyCard;
    //

    //Determines if it is a spell or a monster
    public bool isMonster;
    public Card card; //mmonster card
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

    public GameObject directAttackAgainstOpponent;

    GameObject cardPreview;
    GameObject previewCard;


    public GameObject tributeButton, tributeNo;

    public bool thisCardInDefense = false;

    GameState gameState;

    int thisCardsAttack;
    int thisCardsDefense;
    int thisCardCost;

    Transform playerGraveyard, opponentGraveyard;

    bool destroyInitiator, destroyReceivor; 


    private void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();

        if (isInitializedFromStart) ReadyToInit();
    }

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
           
            cost.text = card.cost.ToString();
            type.text = card.type;
            background.sprite = card.artwork;

            thisCardsAttack = card.attack;
            thisCardsDefense = card.defense;
            thisCardCost = card.cost;

            UpdateUI();
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

        //checking if Card has been played (referenced in PlayableSpot.cs)
        if (hasBeenPulled && GameManager.cardPlayed)
        {
            PutCardBackInHand();
            GameManager.cardPlayed = false;
            Destroy(transform.parent.gameObject);
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
        if (this.GetComponentInChildren<Button>().interactable)
        {
            if (inDefense)
            {
                this.GetComponentInChildren<CardDisplay>().thisCardInDefense = true;
                print("Im in defense!");
                this.transform.eulerAngles = new Vector3(90, 0, 90);
                this.GetComponentInChildren<Button>().interactable = false;
            }
            else
            {
                this.GetComponentInChildren<CardDisplay>().thisCardInDefense = false;
                print("Im in attack!");
                this.transform.eulerAngles = new Vector3(90, 0, 0);
                this.GetComponentInChildren<Button>().interactable = false;
            }
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
        else if (spellCard.name == "Inverted Tarot")
        {
            InvertedTarot();
        }
        else if (spellCard.name == "Lost Collar")
        {
            LostCollar();
        }
        else if (spellCard.name == "Lost In Quicksand")
        {
            Quicksand();
        }
        else if (spellCard.name == "Tomb of the Risen")
        {
            TombOfTheRisen();
        }
        else if (spellCard.name == "Crown Tax")
        {
            CrownTax();
        }
        else if (spellCard.name == "Ogre Hammer")
        {
            OgreHammer();
        }
        else if (spellCard.name == "Princess of Gifts")
        {
            PrincessOfGifts();
        }
        else if (spellCard.name == "Spear Shatter")
        {
            SpearShatter();
        }
        else if (spellCard.name == "Hail Mary")
        {
            HailMary();
        }
    }
    #region Spells
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
                //for each Lore card, you're adding one so 3 cards would be 3 bonus
                
                int totalCards = cardsOnTable.Length;
                cardsOnTable[i].thisCardsDefense += totalCards;
                //

                //this is increasing it by 1 regardless of the card count on the table
                // cardsOnTable[i].defenseOffset += 1;

                cardsOnTable[i].def.text = thisCardsDefense.ToString();
            }
        }
        StartCoroutine(RemovePlayedCard());
    }

    public void CharasmaticSpeech()
    {
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            //print(cardsOnTable[i]);

            cardsOnTable[i].thisCardsAttack -= 1;
            cardsOnTable[i].att.text = cardsOnTable[i].thisCardsAttack.ToString();
           // print(cardsOnTable[i].card.attack);
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
               // print(cardsOnTable[i]);
                cardsOnTable[i].thisCardsAttack += 2;
                cardsOnTable[i].att.text = cardsOnTable[i].thisCardsAttack.ToString();
               // print(cardsOnTable[i].card.attack);
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

    public void InvertedTarot()
    {
        //Scoop up cards in the parent
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            if (cardsOnTable[i].card.type.ToString() == "Tarot")
            {
                cardsOnTable[i].thisCardsDefense += 2;
                cardsOnTable[i].thisCardsAttack += 1;

                cardsOnTable[i].att.text = thisCardsAttack.ToString();
                cardsOnTable[i].def.text = thisCardsDefense.ToString();
            }
        }
        StartCoroutine(RemovePlayedCard());
    }

    public void LostCollar()
    {
        //Scoop up cards in the parent
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            if (cardsOnTable[i].card.type.ToString() == "Pet")
            {
                cardsOnTable[i].thisCardsAttack -= 1;
                cardsOnTable[i].thisCardsDefense += 2;

                cardsOnTable[i].UpdateUI();
            }
        }
        StartCoroutine(RemovePlayedCard());
    }

    public void Quicksand()
    {
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        print(cardsOnTable[0].name);

        int randomDestroy = Random.Range(0, cardsOnTable.Length);
        Destroy(cardsOnTable[randomDestroy].transform.parent.gameObject);

        StartCoroutine(RemovePlayedCard());
    }

    void TombOfTheRisen()
    {
        //Scoop up cards in the parent
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            if (cardsOnTable[i].card.type.ToString() == "Zombie")
            {
                cardsOnTable[i].thisCardsAttack += 2;

                cardsOnTable[i].UpdateUI();
            }
        }
        StartCoroutine(RemovePlayedCard());
    }

    void CrownTax()
    {
        //Scoop up cards in the parent
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            if (cardsOnTable[i].card.type.ToString() == "Human")
            {
                cardsOnTable[i].thisCardCost += 2;

                cardsOnTable[i].UpdateUI();
            }

            if (cardsOnTable[i].card.type.ToString() == "Diety")
            {
                cardsOnTable[i].thisCardCost += 2;

                cardsOnTable[i].UpdateUI();
            }
        }
        StartCoroutine(RemovePlayedCard());
    }

    void OgreHammer()
    {
        //Scoop up cards in the parent
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            if (cardsOnTable[i].card.type.ToString() == "Fiend")
            {
                cardsOnTable[i].thisCardsAttack += 3;
                cardsOnTable[i].thisCardCost -= 2;

                cardsOnTable[i].UpdateUI();
            }
        }
        StartCoroutine(RemovePlayedCard());
    }

    void PrincessOfGifts()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();

        gameState.PlayerDraw();
        gameState.PlayerDraw();

        StartCoroutine(RemovePlayedCard());
    }

    public void SpearShatter()
    {
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        //print(cardsOnTable[0].name);

        int randomHalf = Random.Range(0, cardsOnTable.Length);
        int attackValue = cardsOnTable[randomHalf].thisCardsAttack;

        Mathf.RoundToInt(attackValue * .5f);
        cardsOnTable[randomHalf].thisCardsAttack = attackValue;

        cardsOnTable[randomHalf].UpdateUI();

        StartCoroutine(RemovePlayedCard());
    }

    public void HailMary()
    {
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        GameManager.hailMary = true;
        CardDisplay[] cardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        for (int i = 0; i < cardsOnTable.Length; i++)
        {
            cardsOnTable[i].thisCardsDefense = 1;
            cardsOnTable[i].thisCardsAttack *= 2;

            if (!cardsOnTable[i].inDefense)
            {
                inDefense = true;

                this.GetComponentInChildren<CardDisplay>().thisCardInDefense = true;
                print("Im in defense!");
                this.transform.eulerAngles = new Vector3(90, 0, 90);
            }

            cardsOnTable[i].UpdateUI();
        }
        StartCoroutine(RemovePlayedCard());
    }
    #endregion
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

    public void TurnPlayerInteractableCardsOn()
    {
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        Button[] playerCardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<Button>();
        for (int i = 0; i < playerCardsOnTable.Length; i++)
        {
            playerCardsOnTable[i].interactable = true;
        }
    }

    public void TurnEnemyInteractableCardsOn()
    {
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;
        Button[] opponentCardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<Button>();
        for (int i = 0; i < opponentCardsOnTable.Length; i++)
        {
            opponentCardsOnTable[i].interactable = true;
        }
    }

    public void TurnPlayerInteractableCardsOff()
    {
        playerCardPlacementOnTableParent = GameObject.Find("Player's Play Area").transform;
        Button[] playerCardsOnTable = playerCardPlacementOnTableParent.GetComponentsInChildren<Button>();
        for (int i = 0; i < playerCardsOnTable.Length; i++)
        {
            playerCardsOnTable[i].interactable = false;
        }
    }

    public void CardClicked()
    {
        //Selecting Monster to attack
        if (isMonster && !inDefense && hasBeenPlayed && GameObject.Find("GameState").GetComponent<GameState>().gamePhase == 2)
        {
            monsterTargeted = true;

            //need a check to make sure a different card is not already selected
           
            GameManager.InitiatorCard = this;

            GameManager.playerAttacking = true;

            UpdateCardColors();

        }

        //Selecting Monster to Tribute
        if (isMonster && hasBeenPlayed && GameObject.Find("GameState").GetComponent<GameState>().gamePhase == 1)
        {
            print("Tribute? Y/N");
            tributeButton.SetActive(true);
            tributeNo.SetActive(true);

            //UpdateCardColors();
        }
    }

    public void CardAttackingOtherCard(Button buttonName)
    {
        enemyCardPlacementOnTableParent = GameObject.Find("Opponent's Play Area").transform;

        CardDisplay[] cardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

        //Direct attack (no monsters left on the field
        if (cardsOnTable.Length <= 0 && GameState.turnCount != 1)
        {
            directAttackAgainstOpponent = GameObject.Find("DirectAttack");
            directAttackAgainstOpponent.GetComponent<Image>().enabled = true;
            for (int i = 0; i < directAttackAgainstOpponent.transform.childCount; i++)
            {
                directAttackAgainstOpponent.transform.GetChild(i).gameObject.SetActive(true);
            }
            directAttackAgainstOpponent.SetActive(true);

            GameManager.directDamage = thisCardsAttack;
            print("attack directly");

            this.GetComponentInChildren<Button>().interactable = false;
        }
        #region Attack All Cards
        //Attack All Cards

        /*

        if (monsterTargeted)
        {
            playerAttack = (this.card.attack + attackOffset);
           // print(playerAttack + "total attack");
            if (this.card.attack <= 0)
            {
                Debug.LogWarning("Player attack is negative - did a sign get flipped? This card: " + this.card.name + " has an attack of: " + this.card.attack);
            }

           // print(buttonName.GetComponent<CardDisplay>().card.defense);

            CardDisplay[] cardsOnTable = enemyCardPlacementOnTableParent.GetComponentsInChildren<CardDisplay>();

            for (int i = 0; i < cardsOnTable.Length; i++)
            {
       //         print(cardsOnTable[i].GetComponent<CardDisplay>().card.name + " " + cardsOnTable[i].GetComponent<CardDisplay>().card.inDefense);

                if (cardsOnTable[i].GetComponent<CardDisplay>().card.inDefense == true)
                {
                    cardsOnTable[i].GetComponent<CardDisplay>().card.defense -= playerAttack;
                    //print(cardsOnTable[i].card.defense);
                    cardsOnTable[i].def.text = cardsOnTable[i].card.defense.ToString();
                    if (cardsOnTable[i].GetComponent<CardDisplay>().card.defense <= 0)
                    {
                        print("Remove from field");
                        //we need to reget the cards on the table (don't loop through this)
                    //    Destroy(cardsOnTable[i].transform.parent.gameObject);
                    }
                }
                else
                {
                    cardsOnTable[i].GetComponent<CardDisplay>().card.attack -= playerAttack;
                   // print(cardsOnTable[i].card.attack);
                    cardsOnTable[i].att.text = cardsOnTable[i].card.attack.ToString();
                    if (cardsOnTable[i].GetComponent<CardDisplay>().card.attack <= 0)
                    {
                        //Calculate life point damage

                        //Remove from field
                        Destroy(cardsOnTable[i].transform.parent.gameObject);
                    }
                }
                
            }

            monsterTargeted = false;
            UpdateCardColors();
        
        }
        */
        #endregion
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
                    cardsOnTable[i].GetComponent<Image>().color = new Color32(255, 142, 109, 255);
                }
            }
        }
    }

    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        playerGraveyard = GameObject.Find("PlayerGraveyard").transform;
        opponentGraveyard = GameObject.Find("OpponentGraveyard").transform;

        if (eventData.button == PointerEventData.InputButton.Right && isMonster && hasBeenPlayed && GameObject.Find("GameState").GetComponent<GameState>().gamePhase == 2)
        {
            inDefense = !inDefense;
            FlipCardPosition();
        }

        if (isMonster && this.gameObject.GetComponentInChildren<CardDisplay>().card.playedByAI && GameManager.playerAttacking && eventData.button == PointerEventData.InputButton.Left && GameObject.Find("GameState").GetComponent<GameState>().gamePhase == 2)
        {
            print(this.gameObject.GetComponentInChildren<CardDisplay>().card.name);

            if (this.gameObject.GetComponentInChildren<CardDisplay>().thisCardInDefense)
            {
                GameManager.ReceivingCard = this;

                //only have a single function to update attacks or defenses of a card, poke the function rather than scattered through the script
                print("calculate attack v defense:" + GameManager.InitiatorCard.thisCardsAttack + " " + GameManager.ReceivingCard.thisCardsDefense);
                if (GameManager.InitiatorCard.thisCardsAttack < GameManager.ReceivingCard.thisCardsDefense)
                {
                    print("Player loses life points: " + (GameManager.InitiatorCard.thisCardsAttack - GameManager.ReceivingCard.thisCardsDefense));

                    GameState.playerHealth += (GameManager.InitiatorCard.thisCardsAttack - GameManager.ReceivingCard.thisCardsDefense);

                    gameState.UpdateHealthUI();

                    GameManager.ReceivingCard.thisCardsDefense -= GameManager.InitiatorCard.thisCardsAttack;

                    GameManager.ReceivingCard.def.text = GameManager.ReceivingCard.thisCardsDefense.ToString();

                    destroyInitiator = true;

                 //   GameManager.InitiatorCard.transform.position = playerGraveyard.transform.position;

                    StartCoroutine(LerpPosition(playerGraveyard.transform.position, 1));

                 //   Destroy(GameManager.InitiatorCard.transform.parent.gameObject);
                }

                else if (GameManager.InitiatorCard.thisCardsAttack == GameManager.ReceivingCard.thisCardsDefense)
                {
                    Destroy(GameManager.InitiatorCard.transform.parent.gameObject);
                    Destroy(GameManager.ReceivingCard.transform.parent.gameObject);
                }

                else if (GameManager.InitiatorCard.thisCardsAttack > GameManager.ReceivingCard.thisCardsDefense)
                {
                    GameManager.InitiatorCard.thisCardsAttack -= GameManager.ReceivingCard.thisCardsDefense;

                    GameManager.InitiatorCard.att.text = GameManager.InitiatorCard.thisCardsAttack.ToString();

                    Destroy(GameManager.ReceivingCard.transform.parent.gameObject);
                }

            }
            else
            {
                GameManager.ReceivingCard = this;

                print("calculate attack v attack :" + GameManager.InitiatorCard.thisCardsAttack + " " + GameManager.ReceivingCard.thisCardsAttack);

                if (GameManager.InitiatorCard.thisCardsAttack > GameManager.ReceivingCard.thisCardsAttack)
                {
                    int difference = GameManager.InitiatorCard.thisCardsAttack - GameManager.ReceivingCard.thisCardsAttack;

                    GameState.opponentHealth -= difference;

                    gameState.UpdateHealthUI();

                    GameManager.InitiatorCard.thisCardsAttack -= GameManager.ReceivingCard.thisCardsAttack;
                    GameManager.InitiatorCard.att.text = GameManager.InitiatorCard.thisCardsAttack.ToString();

                    Destroy(GameManager.ReceivingCard.transform.parent.gameObject);
                }

                else if (GameManager.InitiatorCard.thisCardsAttack < GameManager.ReceivingCard.thisCardsAttack)
                {
                    int difference = GameManager.ReceivingCard.thisCardsAttack - GameManager.InitiatorCard.thisCardsAttack;

                    GameState.playerHealth -= difference;

                    gameState.UpdateHealthUI();

                    GameManager.ReceivingCard.thisCardsAttack -= GameManager.InitiatorCard.thisCardsAttack;
                    GameManager.ReceivingCard.att.text = GameManager.ReceivingCard.thisCardsAttack.ToString();

                    Destroy(GameManager.InitiatorCard.transform.parent.gameObject);
                }

                else if (GameManager.InitiatorCard.thisCardsAttack == GameManager.ReceivingCard.thisCardsAttack)
                {
                    Destroy(GameManager.InitiatorCard.transform.parent.gameObject);
                    Destroy(GameManager.ReceivingCard.transform.parent.gameObject);
                }
            }

            GameManager.InitiatorCard.GetComponent<Button>().interactable = false;

            monsterTargeted = false;
            UpdateCardColors();

            GameManager.attackDamage = 0;
            GameManager.playerAttacking = false;

    

            GameManager.InitiatorCard = null;
            GameManager.ReceivingCard = null; 

            //card interactable is turned off
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        #region Preview Card
        cardPreview = GameObject.Find("CardPreview");
        for (int i = 0; i < cardPreview.transform.childCount; i++)
        {
            cardPreview.transform.GetChild(i).gameObject.SetActive(true);
        }
        previewCard = GameObject.Find("PreviewCard");

        if (this.isMonster)
        {
            previewCard.GetComponent<CardDisplay>().title.text = card.name;
            previewCard.GetComponent<CardDisplay>().desc.text = card.description;
            previewCard.GetComponent<CardDisplay>().def.text = thisCardsDefense.ToString();
            previewCard.GetComponent<CardDisplay>().att.text = thisCardsAttack.ToString();
            previewCard.GetComponent<CardDisplay>().cost.text = thisCardCost.ToString();
            previewCard.GetComponent<CardDisplay>().type.text = card.type;
            previewCard.GetComponent<CardDisplay>().background.sprite = card.artwork;
            previewCard.GetComponent<Image>().color = new Color32(255, 142, 109, 255);

            previewCard.transform.Find("Attack").gameObject.SetActive(true);
            previewCard.transform.Find("Defense").gameObject.SetActive(true);
        }
        else
        {
            previewCard.GetComponent<CardDisplay>().title.text = spellCard.name;
            previewCard.GetComponent<CardDisplay>().desc.text = spellCard.effect;
            previewCard.GetComponent<CardDisplay>().type.text = spellCard.type;
            previewCard.GetComponent<CardDisplay>().background.sprite = spellCard.artwork;
            previewCard.GetComponent<Image>().color = new Color32(109, 192, 255, 255);

            previewCard.transform.Find("Attack").gameObject.SetActive(false);
            previewCard.transform.Find("Defense").gameObject.SetActive(false);
        }
        #endregion
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        cardPreview = GameObject.Find("CardPreview");
        for (int i = 0; i < cardPreview.transform.childCount; i++)
        {
            cardPreview.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Tribute()
    {
        GameState.CurrencyThisTurn += this.GetComponent<CardDisplay>().card.cost;
        GameObject.Find("GameState").GetComponent<GameState>().UpdateCardValueUI();
        //Add particle system and a little time delay
        Destroy(this.transform.parent.gameObject);
    }

    public void NoTribute()
    {
        tributeButton.SetActive(false);
        tributeNo.SetActive(false);
    }

    public void UpdateUI()
    {
        att.text = thisCardsAttack.ToString();
        def.text = thisCardsDefense.ToString();
        cost.text = thisCardCost.ToString();
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        print("play particle system");

       // yield return new WaitForSeconds(1f);
        float time = 0;
        Vector3 startPosition = GameManager.InitiatorCard.transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        destroyInitiator = false;
        destroyReceivor = false;
    }
}
