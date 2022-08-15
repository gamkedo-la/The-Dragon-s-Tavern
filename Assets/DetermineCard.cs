using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetermineCard : MonoBehaviour
{
    public GameObject monster, spell;

    public Image monsterBackground, spellBackground;
    public Text monsterTitle, spellTitle;
    public Text monsterDesc, spellDesc;
    public Text attack, defense, cost;
    public Text monsterType, spellType;

    public GameManager gameManager;

    public GameObject particles, cardFace;

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

                gameManager.MonsterCardsOwned.Add(gameManager.MonsterCardsToBePulled[monsterCardToPick]);
                gameManager.MonsterCardsToBePulled.Remove(gameManager.MonsterCardsToBePulled[monsterCardToPick]);
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
                spellBackground.GetComponent<Image>().sprite = gameManager.SpellCardsToBePulled[spellCardToPick].artwork;


                //Unlock in the game manager
                gameManager.SpellCardsOwned.Add(gameManager.SpellCardsToBePulled[spellCardToPick]);
                gameManager.SpellCardsToBePulled.Remove(gameManager.SpellCardsToBePulled[spellCardToPick]);
            }
        }
    }

    public void TurnEverythingOff()
    {
        monsterTitle.text = null;
        monsterDesc.text = null;
        monsterType.text = null;
        attack.text = null;
        defense.text = null;
        cost.text = null;
        monsterBackground.sprite = null;

        spellTitle.text = null;
        spellDesc.text = null;
        spellType.text = null;
        spellBackground.GetComponent<Image>().sprite = null;



        monster.SetActive(false);
        spell.SetActive(false);

        particles.SetActive(false);
        cardFace.SetActive(false);
    }
}
