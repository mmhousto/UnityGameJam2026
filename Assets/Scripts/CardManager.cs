using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<PlayingCard> playerDeck = new List<PlayingCard>();

    public List<PlayingCard> uniqueCards = new List<PlayingCard>();
    public GameObject hud;
    public GameObject viewDeckContent;
    public GameObject cardUI;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(hud);
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
        // STARTING DECK
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Attack, "Scratch", 2, 1, 0, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13)));
        playerDeck.Add(new PlayingCard(CardType.Block, "Block", 0, 2, 5, Random.Range(2, 13)));

        foreach (PlayingCard card in playerDeck)
        {
            GameObject newCard = Instantiate(cardUI, viewDeckContent.transform);
            Card theCard = newCard.GetComponent<Card>();
            theCard.type = card.type;
            theCard.name = card.cardName;
            theCard.cardName = card.cardName;
            theCard.damage = card.damage;
            theCard.stamina = card.stamina;
            theCard.block = card.block;
            theCard.luckyNumber = card.luckyNumber;

        }

        // ATTACKING CARDS
        uniqueCards.Add(new PlayingCard(CardType.Attack, "Tail Whip", 3, 1, 0, Random.Range(2, 13)));
        uniqueCards.Add(new PlayingCard(CardType.Attack, "Pounce", 5, 3, 0, Random.Range(2, 13)));

        // HEALING CARDS
        uniqueCards.Add(new PlayingCard(CardType.Heal, "Drink Milk", -5, 2, 0, Random.Range(2, 13)));

        //Blocking Cards
        uniqueCards.Add(new PlayingCard(CardType.Block, "Knead", 0, 3, 8, Random.Range(2, 13)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
