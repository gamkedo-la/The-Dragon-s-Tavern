using UnityEngine;
using UnityEngine.UI;

public class DeckSetupUI : MonoBehaviour
{
    public Button playButton;
    public Text playText;

    public Text cardsInDeck;

    // Update is called once per frame
    void Update()
    {
        if (CardsSelectedForDeck.instance.monsterCards.Count == 0 && CardsSelectedForDeck.instance.spellCards.Count == 0)
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

    void ShowUI()
    {
        cardsInDeck.text = "Cards In Deck:  " + (CardsSelectedForDeck.instance.monsterCards.Count + CardsSelectedForDeck.instance.spellCards.Count);
    }
}
