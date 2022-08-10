using UnityEngine;

public class SelectedCardUI : MonoBehaviour
{
    [SerializeField] GameObject monsterCardPrefab = null;
    [SerializeField] GameObject spellCardPrefab = null;
    [SerializeField] Transform scrollRectContentArea = null;

    public void DisplaySelectedMonsterCard(Card selectedMonsterCard)
    {
        GameObject monsterCardInstance = Instantiate(monsterCardPrefab, scrollRectContentArea);
        HubCardDisplay cardDisplay = monsterCardInstance.GetComponentInChildren<HubCardDisplay>();
        cardDisplay.isInSelectedArea = true;

        cardDisplay.monsterCard = selectedMonsterCard;

        // cardDisplay.title.text = selectedMonsterCard.name;
        // cardDisplay.desc.text = selectedMonsterCard.description;
        
        // cardDisplay.cost.text = selectedMonsterCard.cost.ToString();
        // cardDisplay.type.text = selectedMonsterCard.type;
        // cardDisplay.background.sprite = selectedMonsterCard.artwork;

        // cardDisplay.att.text = selectedMonsterCard.attack.ToString();
        // cardDisplay.def.text = selectedMonsterCard.defense.ToString();
        // cardDisplay.cost.text = selectedMonsterCard.cost.ToString();
    }

    public void DisplaySelectedSpellCard(SpellCard selectedSpellCard)
    {
        GameObject spellCardInstance = Instantiate(spellCardPrefab, scrollRectContentArea);
        HubCardDisplay cardDisplay = spellCardInstance.GetComponentInChildren<HubCardDisplay>();
        cardDisplay.isInSelectedArea = true;
        
        cardDisplay.spellCard = selectedSpellCard;
        
        // cardDisplay.title.text = selectedSpellCard.name;
        // cardDisplay.desc.text = selectedSpellCard.effect;
        
        // cardDisplay.type.text = selectedSpellCard.type;
        // cardDisplay.background.sprite = selectedSpellCard.artwork;
    }
}
