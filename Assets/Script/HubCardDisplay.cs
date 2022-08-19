using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HubCardDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Card monsterCard; //monster card
    public SpellCard spellCard;
    
    //Needed to display the card info on the card itself
    public Image background;
    public Text title;
    public Text desc;
    public Text def;
    public Text att;
    public Text cost;
    public Text type;

    // Preview
    GameObject cardPreview;
    GameObject previewCard;

    // Hover Param
    public Vector3 offset = new Vector3(0, 30, 0);
    public bool hasEntered;

    // Booleans
    public bool isInSelectedArea = false;

    GameManager gameManager;
    UpdateCardsOwned updateCardsOwned;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        updateCardsOwned = GameObject.Find("Canvas-DeckCreation").GetComponent<UpdateCardsOwned>();
        //Initial display of information
        if (monsterCard != null)
        {
            title.text = monsterCard.name;
            desc.text = monsterCard.description;
           
            cost.text = monsterCard.cost.ToString();
            type.text = monsterCard.type;
            background.sprite = monsterCard.artwork;

            att.text = monsterCard.attack.ToString();
            def.text = monsterCard.defense.ToString();
            cost.text = monsterCard.cost.ToString();
        }
        else
        {
            title.text = spellCard.name;
            desc.text = spellCard.effect;
            type.text = spellCard.type;
            background.sprite = spellCard.artwork;
        }
    }

    public void CardHoverEnter()
    {
        this.gameObject.transform.localPosition += offset;
        hasEntered = true;
    }

    public void CardHoverExit()
    {
        this.gameObject.transform.localPosition -= offset;
        hasEntered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardsSelectedForDeck cardsSelectedForDeck = FindObjectOfType<CardsSelectedForDeck>();
        
        if (!isInSelectedArea){
            AddCardToSelection(cardsSelectedForDeck);

            if (this.monsterCard != null)
            {
                gameManager.MonsterCardsOwned.Remove(this.monsterCard);
                updateCardsOwned.RefreshList();
            }
            else
            {
                gameManager.SpellCardsOwned.Remove(this.spellCard);
                updateCardsOwned.RefreshList();
            }
        }
        else {
            RemoveCardFromSelection(cardsSelectedForDeck);

            if (this.monsterCard != null)
            {
                gameManager.MonsterCardsOwned.Add(this.monsterCard);
                updateCardsOwned.RefreshList();
            }
            else
            {
                gameManager.SpellCardsOwned.Add(this.spellCard);
                updateCardsOwned.RefreshList();
            }
        }
    }

    private void RemoveCardFromSelection(CardsSelectedForDeck cardsSelectedForDeck)
    {
        if (monsterCard != null)
        {
            cardsSelectedForDeck.RemoveMonsterCard(monsterCard);
        }
        else
        {
            cardsSelectedForDeck.RemoveSpellCard(spellCard);
        }

        Destroy(transform.parent.gameObject);
    }

    private void AddCardToSelection(CardsSelectedForDeck cardsSelectedForDeck)
    {   
        if (monsterCard != null)
        {
            cardsSelectedForDeck.SelectMonsterCard(monsterCard);
        }
        else
        {
            cardsSelectedForDeck.SelectSpellCard(spellCard);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        #region Preview Card
        cardPreview = GameObject.Find("CardPreview");
        for (int i = 0; i < cardPreview.transform.childCount; i++)
        {
            cardPreview.transform.GetChild(i).gameObject.SetActive(true);
        }
        previewCard = GameObject.Find("PreviewCard");
        
        // Set Card Visuals
        previewCard.GetComponent<CardDisplay>().title.text = title.text;
        previewCard.GetComponent<CardDisplay>().desc.text = desc.text;
        previewCard.GetComponent<CardDisplay>().type.text = type.text;
        
        if (this.monsterCard != null)
        {
            previewCard.GetComponent<CardDisplay>().background.sprite = monsterCard.artwork;
            previewCard.GetComponent<Image>().color = new Color32(255, 142, 109, 255);
   
            previewCard.transform.Find("Attack").gameObject.SetActive(true);
            previewCard.transform.Find("Defense").gameObject.SetActive(true);
            previewCard.transform.Find("Cost").gameObject.SetActive(true);
            
            previewCard.GetComponent<CardDisplay>().def.text = def.text;
            previewCard.GetComponent<CardDisplay>().att.text = att.text;
            previewCard.GetComponent<CardDisplay>().cost.text = cost.text;
         
        }
        else
        {
            previewCard.GetComponent<CardDisplay>().background.sprite = spellCard.artwork;
            previewCard.GetComponent<Image>().color = new Color32(109, 192, 255, 255);

            previewCard.transform.Find("Attack").gameObject.SetActive(false);
            previewCard.transform.Find("Defense").gameObject.SetActive(false);
            previewCard.transform.Find("Cost").gameObject.SetActive(false);
        }
        #endregion
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardPreview = GameObject.Find("CardPreview");
        for (int i = 0; i < cardPreview.transform.childCount; i++)
        {
            cardPreview.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
