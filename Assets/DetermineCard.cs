using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetermineCard : MonoBehaviour
{
    public GameObject monster, spell;

    public Card card;
    public SpellCard spellCard;

    public Image monsterBackground, spellBackground;
    public Text monsterTitle, spellTitle;
    public Text monsterDesc, spellDesc;
    public Text attack, defense, cost;
    public Text monsterType, spellType;

    public GameManager gameManager;

    GameObject cardPreview;

    public void ChooseCardToUnlock()
    {
        int monsterCardToPick = Random.Range(0, gameManager.MonsterCardsToBePulled.Count);
        int spellCardToPick = Random.Range(0, gameManager.SpellCardsToBePulled.Count);

        int chooseMonsterOrSpell = Random.Range(0, 2);

        if (chooseMonsterOrSpell == 0)
        {
            if (gameManager.MonsterCardsToBePulled.Count == 0)
            {
                ChooseCardToUnlock();
            }
            else
            {
                //Unlock Monster
                monster.SetActive(true);

                // Set Card Visuals
                monsterTitle.text = gameManager.MonsterCardsToBePulled[monsterCardToPick].name;
                monsterDesc.text = gameManager.MonsterCardsToBePulled[monsterCardToPick].description;
                monsterType.text = gameManager.MonsterCardsToBePulled[monsterCardToPick].type;
                attack.text = gameManager.MonsterCardsToBePulled[monsterCardToPick].attack.ToString();
                defense.text = gameManager.MonsterCardsToBePulled[monsterCardToPick].defense.ToString();
                cost.text = gameManager.MonsterCardsToBePulled[monsterCardToPick].cost.ToString();
                monsterBackground.sprite = gameManager.MonsterCardsToBePulled[monsterCardToPick].artwork;

                //Unlock in the game manager
            }
        }

        if (chooseMonsterOrSpell == 1)
        {
            if (gameManager.SpellCardsToBePulled.Count == 0)
            {
                ChooseCardToUnlock();
            }
            else
            {
                //Unlock Monster
                spell.SetActive(true);

                // Set Card Visuals
                spellTitle.text = gameManager.SpellCardsToBePulled[spellCardToPick].name;
                spellDesc.text = gameManager.SpellCardsToBePulled[spellCardToPick].effect;
                spellType.text = gameManager.SpellCardsToBePulled[spellCardToPick].type;
                spellBackground.sprite = gameManager.SpellCardsToBePulled[spellCardToPick].artwork;
                //Unlock in the game manager
            }
        }
    }
}
