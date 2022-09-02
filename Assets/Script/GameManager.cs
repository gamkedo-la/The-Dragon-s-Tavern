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

    //DifficultyModifications (HubAreaTriggers.cs)
    public static float animationSpeed;
    public static int opponentCurrency;
    public static int playerCurrency;

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

        animationSpeed = 2.25f;
        opponentCurrency = 6;
        playerCurrency = 4;

        currency = 5;
        LoadGame();
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
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("FirstTime", firstTimeLoadingIn);

        //Currency
        PlayerPrefs.SetInt("PackPoints", currency);

        //Cards Owned

        for (int i = 0; i < MonsterCardsOwned.Count; i++)
        {
            PlayerPrefs.SetString("MonsterCard" + i, MonsterCardsOwned[i].name);
        }

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

        for(int i = 0; i < CardsSelectedForDeck.instance.monsterCards.Count; i++){
            PlayerPrefs.SetString("MonstersInDeck" + i, CardsSelectedForDeck.instance.monsterCards[i].name);
        }

        PlayerPrefs.SetInt("MonsterCardsInDeck", CardsSelectedForDeck.instance.monsterCards.Count);

        for(int i = 0; i < CardsSelectedForDeck.instance.spellCards.Count; i++){
            PlayerPrefs.SetString("SpellsInDeck" + i, CardsSelectedForDeck.instance.spellCards[i].name);
        }

        PlayerPrefs.SetInt("SpellCardsInDeck", CardsSelectedForDeck.instance.spellCards.Count);
    }

    public void LoadGame()
    {
        print("Game Loaded");

        //FirstTimeLoadingIn
        firstTimeLoadingIn = PlayerPrefs.GetInt("FirstTime", 0);



        if (firstTimeLoadingIn == 0)
        {
            currency = 5;
            firstTimeLoadingIn += 1;
            SaveGame();
        }

        MonsterCardsOwned = new List<Card>();
        SpellCardsOwned = new List<SpellCard>();

        MonsterCardsToBePulled = new List<Card>();
        SpellCardsToBePulled = new List<SpellCard>();

        CardsSelectedForDeck.instance.monsterCards = new List<Card>();
        CardsSelectedForDeck.instance.spellCards = new List<SpellCard>();

        //Currency
        currency = PlayerPrefs.GetInt("PackPoints", currency);

        //Cards Owned

        totalMonstersOwned = PlayerPrefs.GetInt("MonsterCardsOwned");
        totalSpellsOwned = PlayerPrefs.GetInt("SpellCardsOwned");

        for (int i = 0; i < totalMonstersOwned; i++)
        {
            string cardName = PlayerPrefs.GetString("MonsterCard" + i);

           // print(cardName);

            Card cardToAdd = Resources.Load<Card>("ScriptableObject/Monsters/" + cardName) as Card;
            MonsterCardsOwned.Add(cardToAdd);
        }

        for (int i = 0; i < totalSpellsOwned; i++)
        {
            string cardName = PlayerPrefs.GetString("SpellCard" + i);

           // print(cardName);

            SpellCard cardToAdd = Resources.Load<SpellCard>("ScriptableObject/Spell/" + cardName) as SpellCard;
            SpellCardsOwned.Add(cardToAdd);
        }

        //Card Packs remaining

        totalMonstersToPull = PlayerPrefs.GetInt("MonsterCardsToBePulled");
        totalSpellsToPull = PlayerPrefs.GetInt("SpellCardsToBePulled");

        for (int i = 0; i < totalMonstersToPull; i++)
        {
            string cardName = PlayerPrefs.GetString("MonsterPacks" + i);
            
           // print(cardName);

            Card cardToAdd = Resources.Load<Card>("ScriptableObject/Monsters/" + cardName) as Card;
            MonsterCardsToBePulled.Add(cardToAdd);
        }

        for (int i = 0; i < totalSpellsToPull; i++)
        {
            string cardName = PlayerPrefs.GetString("SpellPacks" + i);

            //print(cardName);

            SpellCard cardToAdd = Resources.Load<SpellCard>("ScriptableObject/Spell/" + cardName) as SpellCard;
            SpellCardsToBePulled.Add(cardToAdd);
        }

        int monstersInDeck = PlayerPrefs.GetInt("MonsterCardsInDeck", 0);

        for(int i = 0; i < monstersInDeck; i++){
            string cardName = PlayerPrefs.GetString("MonstersInDeck" + i);

           // print(cardName);

            Card cardToAdd = Resources.Load<Card>("ScriptableObject/Monsters/" + cardName) as Card;
            CardsSelectedForDeck.instance.monsterCards.Add(cardToAdd);
            
        }

        int spellsInDeck = PlayerPrefs.GetInt("SpellCardsInDeck", 0);

        for(int i = 0; i < spellsInDeck; i++){
            string cardName = PlayerPrefs.GetString("SpellsInDeck" + i);

            //print(cardName);

            SpellCard cardToAdd = Resources.Load<SpellCard>("ScriptableObject/Spell/" + cardName) as SpellCard;
            CardsSelectedForDeck.instance.spellCards.Add(cardToAdd);
        }

    }

    [ContextMenu("Clear PlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void FireProjectile(Vector3 from, Vector3 to)
    {
        SpellTrail spell = Instantiate(spellTrail, from, Quaternion.identity);
        spell.SetTarget(to);
    }
}
