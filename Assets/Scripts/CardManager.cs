using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static CardManager instance;
    public static CardManager Instance {  get { return instance; } }

    private List<Card> playerCards = new List<Card>();
    public List<PlayingCard> playerDeck = new List<PlayingCard>();
    public List<PlayingCard> uniqueCards = new List<PlayingCard>();
    public GameObject hud;
    public GameObject viewDeckContent;
    public GameObject cardUI;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(hud);
            
            SetStartingDeck();
        }
    }

    /*
     * 2-12 Range
     * 
     * Lucky number exact - x5
     * 1-3 - x3
     * 4-5 - x2
     * 6-7 - x1
     * 8+ x0.5
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLuckyNumber(PlayingCard playingCard, int newLucky)
    {
        foreach(Card card in playerCards)
        {
            if(card.GetPlayingCard() == playingCard)
            {
                card.luckyNumber = newLucky;
                card.GetPlayingCard().luckyNumber = newLucky;
            }
        }
    }

    public void UpdateDamage(PlayingCard playingCard, int newValue)
    {
        foreach (Card card in playerCards)
        {
            if (card.GetPlayingCard() == playingCard)
            {
                card.damage = newValue;
                card.GetPlayingCard().damage = newValue;
            }
        }
    }

    public void UpdateBlock(PlayingCard playingCard, int newValue)
    {
        foreach (Card card in playerCards)
        {
            if (card.GetPlayingCard() == playingCard)
            {
                card.block = newValue;
                card.GetPlayingCard().block = newValue;
            }
        }
    }

    public void ResetCardManager()
    {
        foreach(Card c in playerCards)
        {
            Destroy(c.gameObject);
        }

        playerCards.Clear();
        playerDeck.Clear();
        uniqueCards.Clear();

        SetStartingDeck();
    }

    public void SetStartingDeck()
    {
        // STARTING DECK
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13), 0, ""));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13), 0, ""));

        foreach (PlayingCard card in playerDeck)
        {
            GameObject newCard = Instantiate(cardUI, viewDeckContent.transform);
            Card theCard = newCard.GetComponent<Card>();
            theCard.SetUpCard(card);
            playerCards.Add(theCard);
        }

        // ATTACKING CARDS
        uniqueCards.Add(new PlayingCard(CardType.Attack, "Tail Whip", 3, 1, 0, Random.Range(2, 13), 100, ""));
        uniqueCards.Add(new PlayingCard(CardType.Attack, "Pounce", 5, 3, 0, Random.Range(2, 13), 200, ""));
        uniqueCards.Add(new PlayingCard(CardType.Attack, "Hair Ball", 9, 7, 0, Random.Range(2, 13), 500, ""));
        uniqueCards.Add(new PlayingCard(CardType.Attack, "The Box", 10, 4, 0, Random.Range(2, 13), 500, "50% chance to damage enemy or you for 1/2 damage"));

        // HEALING CARDS
        uniqueCards.Add(new PlayingCard(CardType.Heal, "Drink Milk", 5, 2, 0, Random.Range(2, 13), 150, ""));
        uniqueCards.Add(new PlayingCard(CardType.Heal, "Curiosity", -2, 5, 0, Random.Range(2, 13), 250, "Sacrifice 2 health for a random card"));

        //Blocking Cards
        uniqueCards.Add(new PlayingCard(CardType.Block, "Knead", 0, 3, 8, Random.Range(2, 13), 250, ""));
    }

    public void AddNewCard(PlayingCard playinCard)
    {
        playerDeck.Add(playinCard);
        GameObject newCard = Instantiate(cardUI, viewDeckContent.transform);
        Card theCard = newCard.GetComponent<Card>();
        theCard.SetUpCard(playinCard);
        playerCards.Add(theCard);
    }
}
