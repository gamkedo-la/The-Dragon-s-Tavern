using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public static bool hasBeenPulled;
    public static bool monsterSelected;
    public static bool spellSelected;

    //Play Card from Hand
    public static bool monsterPulled, spellPulled;
    public static bool cardPlayed;
    public static string cardToBePlayed;

    //Store damage dealt
    public static int directDamage;
    public static int attackDamage;
    public static bool playerAttacking;

    public static CardDisplay InitiatorCard;
    public static CardDisplay ReceivingCard;

    //Card aftermath
    public static bool hailMary;

    // Every Card Dictionary
    public static GameManager instance;
    public List<Card> MonsterCardList;
    public List<SpellCard> SpellCardList;

    //Currency
    public static int currency;

    //TotalCardsPlayerCanPull;
    public List<Card> MonsterCardsToBePulled;
    public List<SpellCard> SpellCardsToBePulled;

    public List<Card> MonsterCardsOwned;
    public List<SpellCard> SpellCardsOwned;

    public static int firstTimeLoadingIn;

    int totalMonstersOwned, totalSpellsOwned, totalMonstersToPull, totalSpellsToPull;

    public GameObject DirectAttackButton;
    public SpellTrail spellTrail;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        instance = this;

        currency = 5;
      //  LoadGame();
    }

    public Card FindMonster(string cardName)
    {
        for (int i = 0; i < MonsterCardList.Count; i++)
        {
            if (MonsterCardList[i].name == cardName)
            {
                return MonsterCardList[i];
            }
        }
        return null;
    }

    public SpellCard FindSpell(string cardName)
    {
        for (int i = 0; i < SpellCardList.Count; i++)
        {
            if (SpellCardList[i].name == cardName)
            {
                return SpellCardList[i];
            }
        }
        return null;
    }

    public void SaveGame()
    {
        print("Game Saved");

        //FirstTimeLoadingIn
        PlayerPrefs.SetInt("FirstTime", firstTimeLoadingIn);
        firstTimeLoadingIn = PlayerPrefs.GetInt("FirstTime");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("FirstTime", firstTimeLoadingIn);

        //Currency
        PlayerPrefs.SetInt("PackPoints", currency);

        //Cards Owned

/*
        for (int i = 0; i < MonsterCardsOwned.Count; i++)
        {
            PlayerPrefs.SetString("MonsterCard" + i, MonsterCardsOwned[i].name);
        }
*/
        PlayerPrefs.SetInt("MonsterCardsOwned", MonsterCardsOwned.Count);

        for (int i = 0; i < SpellCardsOwned.Count; i++)
        {
            PlayerPrefs.SetString("SpellCard" + i, SpellCardsOwned[i].name);
        }

        PlayerPrefs.SetInt("SpellCardsOwned", SpellCardsOwned.Count);

        //Card Packs remaining
        for (int i = 0; i < MonsterCardsToBePulled.Count; i++)
        {
            PlayerPrefs.SetString("MonsterPacks" + i, MonsterCardsToBePulled[i].name);
        }

        PlayerPrefs.SetInt("MonsterCardsToBePulled", MonsterCardsToBePulled.Count);

        for (int i = 0; i < SpellCardsToBePulled.Count; i++)
        {
            PlayerPrefs.SetString("SpellPacks" + i, SpellCardsToBePulled[i].name);
        }

        PlayerPrefs.SetInt("SpellCardsToBePulled", SpellCardsToBePulled.Count);
    }

    public void LoadGame()
    {
        print("Game Loaded");

        if (firstTimeLoadingIn == 0)
        {
            currency = 5;
            firstTimeLoadingIn += 1;
        }
        else
        {
            MonsterCardsOwned.Clear();
            SpellCardsOwned.Clear();

            MonsterCardsToBePulled.Clear();
            SpellCardsToBePulled.Clear();
        }

        //FirstTimeLoadingIn
        firstTimeLoadingIn = PlayerPrefs.GetInt("FirstTime");

        //Currency
        currency = PlayerPrefs.GetInt("PackPoints");

        //Cards Owned

        totalMonstersOwned = PlayerPrefs.GetInt("MonsterCardsOwned");
        totalSpellsOwned = PlayerPrefs.GetInt("SpellCardsOwned");

        for (int i = 0; i < totalMonstersOwned; i++)
        {
            string cardName = PlayerPrefs.GetString("MonsterCard" + i);

            print(cardName);

            Card cardToAdd = Resources.Load<Card>("Cards/Monsters/" + cardName) as Card;
            MonsterCardsOwned.Add(cardToAdd);
        }

        for (int i = 0; i < totalSpellsOwned; i++)
        {
            string cardName = PlayerPrefs.GetString("SpellCard" + i);
            // MonsterCardsOwned.Add(cardName)
        }

        //Card Packs remaining

        totalMonstersToPull = PlayerPrefs.GetInt("MonsterCardsToBePulled");
        totalSpellsToPull = PlayerPrefs.GetInt("SpellCardsToBePulled");

        for (int i = 0; i < totalMonstersToPull; i++)
        {
            string cardName = PlayerPrefs.GetString("MonsterPacks" + i);
            // MonsterCardsOwned.Add(cardName)
        }

        for (int i = 0; i < totalSpellsToPull; i++)
        {
            string cardName = PlayerPrefs.GetString("SpellPacks" + i);
            // MonsterCardsOwned.Add(cardName)
        }
    }

    public void FireProjectile(Vector3 from, Vector3 to)
    {
        SpellTrail spell = Instantiate(spellTrail, from, Quaternion.identity);
        spell.SetTarget(to);
    }
}
