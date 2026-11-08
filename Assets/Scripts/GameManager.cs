using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public Player player;
    public CardManager cardManager;
    public Enemy[] enemies;
    public GameObject cardPrefab;
    public Transform playerHandObject;

    private List<PlayingCard> playerHand = new List<PlayingCard>();
    private List<PlayingCard> discardPile = new List<PlayingCard>();
    private List<PlayingCard> currentDeck;
    private List<Card> cardsInHand = new List<Card>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("PlayerData").GetComponent<Player>();
        cardManager = GameObject.FindWithTag("CardManager").GetComponent<CardManager>();
        player.playerInfoHUD.SetActive(true);
        currentDeck = new List<PlayingCard>(cardManager.playerDeck);
        Shuffle(currentDeck);
        DrawXTimes(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawXTimes(int draws)
    {
        for(int i = 0; i < draws; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, playerHandObject);
            Card c = newCard.GetComponent<Card>();
            c.SetUpCard(currentDeck[0]);
            cardsInHand.Add(c);

            playerHand.Add(currentDeck[0]);
            currentDeck.RemoveAt(0);
        }
        
    }

    void Shuffle(List<PlayingCard> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            // gets random card
            int rng = Random.Range(0, i + 1);

            // swap cards
            PlayingCard temp = list[i];
            list[i] = list[rng];
            list[rng] = temp;
        }
    }

    /*
     * 2-12 Range
     * 
     * base + roll
     * 
     * Lucky number exact - x7 & +7 health +7 block
     * Opposite Lucky # - +7 dmg, health, block
     *
     *                      
     * • Inverted Pair (Opposite Lucky): (2-12, 3-11, 4-10, 5-9, 6-8, 7-7, 8-6, ...) sum = 14
     *                    -10, -8,    -6,   -4,  -2,   0,   2,  4, 6, 8, 10
     *                    Exclude 7-7?
     */

    public void AttackEnemy(GameObject enemy, Card card)
    {
        Enemy enemyBeingHit = enemy.GetComponent<Enemy>();
        int incomingDamage = card.damage;

        if(card.luckyNumber == player.luckyNumber)
        {
            incomingDamage *= 7;
            player.TakeDamage(-7);
            player.block += 7;
        }
        else if (card.luckyNumber + player.luckyNumber == 14)
        {
            incomingDamage += 7;
            player.TakeDamage(-7);
            player.block += 7;
        }
        
        // Can Enemy Block
        if(incomingDamage > enemyBeingHit.block)
        {
            incomingDamage -= enemyBeingHit.block;
            enemyBeingHit.block = 0;
        }
        else
        {
            enemyBeingHit.block -= incomingDamage;
            incomingDamage = 0;
        }
        enemyBeingHit.UpdateShield();
        enemyBeingHit.TakeDamage(incomingDamage);

        // ADD TO DISCARD PILE
        // REMOVE FROM HAND

    }
}
