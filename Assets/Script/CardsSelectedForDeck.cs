using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsSelectedForDeck : MonoBehaviour
{
    public static CardsSelectedForDeck instance;
    public List<Card> monsterCards;
    public List<SpellCard> spellCards;
    
    public SelectedCardUI selectedCardUI = null;

    public Button playButton;
    public Text playText;

    public Text cardsInDeck;

    private void Awake() 
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        if (monsterCards.Count == 0 && spellCards.Count == 0) return;

        DrawACard playersDeck = FindObjectOfType<DrawACard>();
        playersDeck.monsterCards = monsterCards;
        playersDeck.spellCards = spellCards;
    }

    private void Update()
    {
        if (monsterCards.Count == 0 && spellCards.Count == 0)
        {
            playButton.interactable = false;
            playText.text = "Build a Deck";
        }
        else
        {
            playButton.interactable = true;
            playText.text = "Play";
        }
        ShowUI();
    }

    public void SelectMonsterCard(Card card)
    {
        monsterCards.Add(card);
        selectedCardUI.DisplaySelectedMonsterCard(card);

       // ShowUI();
    }

    public void SelectSpellCard(SpellCard card)
    {
        spellCards.Add(card);
        selectedCardUI.DisplaySelectedSpellCard(card);

        
    }

    public void RemoveMonsterCard(Card card)
    {
        for(int i = 0; i < monsterCards.Count; i++)
        {
            if (monsterCards[i].name == card.name)
            {
                monsterCards.RemoveAt(i);
                return;
            }
        }

        //ShowUI();
    }

    public void RemoveSpellCard(SpellCard card)
    {
        for(int i = 0; i < spellCards.Count; i++)
        {
            if (spellCards[i].name == card.name)
            {
                spellCards.RemoveAt(i);
                return;
            }
        }
       // ShowUI();
    }

    void ShowUI()
    {
        cardsInDeck.text = "Cards In Deck:  " + (monsterCards.Count + spellCards.Count);
    }
}
