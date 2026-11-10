using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    public Player player;
    public CardManager cardManager;
    public Enemy[] enemies;
    public GameObject cardPrefab;
    public Transform playerHandObject;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;

    private bool gameOver;
    private int enemyCount;
    private List<PlayingCard> playerHand = new List<PlayingCard>();
    private List<PlayingCard> discardPile = new List<PlayingCard>();
    private List<PlayingCard> currentDeck;
    private List<Card> cardsInHand = new List<Card>();
    private ArrangePlayerHand arrangePlayerHand;

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
        gameOver = false;
        arrangePlayerHand = playerHandObject.GetComponent<ArrangePlayerHand>();
        player = GameObject.FindWithTag("PlayerData").GetComponent<Player>();
        cardManager = GameObject.FindWithTag("CardManager").GetComponent<CardManager>();
        player.playerInfoHUD.SetActive(true);
        currentDeck = new List<PlayingCard>(cardManager.playerDeck);
        Shuffle(currentDeck);
        DrawCards(5);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Health <= 0 && gameOver == false)
        {
            GameOver();
        }

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount <= 0 && gameOver == false)
        {
            GameWin();
        }
    }

    void DrawCards(int draws)
    {
        for(int i = 0; i < draws; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, playerHandObject);
            Card c = newCard.GetComponent<Card>();

            // Check if ran out of cards
            if(currentDeck.Count <= 0)
            {
                currentDeck.AddRange(discardPile); // Adds discard pile to deck
                discardPile.Clear(); // clears discards pile
                Shuffle(currentDeck); // shuffle deck
            }

            c.SetUpCard(currentDeck[0]);
            cardsInHand.Add(c);

            playerHand.Add(currentDeck[0]);
            currentDeck.RemoveAt(0);
        }

        arrangePlayerHand.ArrangeChildren();
        ResetCardPosition();
    }

    void ResetCardPosition()
    {
        foreach(Card c in cardsInHand)
        {
            c.UpdateStartPosition();
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
        if (player.stamina >= card.stamina)
        {
            player.UseStamina(card.stamina);

            Enemy enemyBeingHit = enemy.GetComponent<Enemy>();
            int incomingDamage = card.damage;

            if (card.luckyNumber == player.luckyNumber)
            {
                incomingDamage *= 7;
                player.TakeDamage(-7);
                player.block += 7;
                player.IncreaseMaxStamina();

                // Increase Card Damage/Block/Heal Permanently
                card.damage += 3;
                card.GetPlayingCard().damage = card.damage;
                cardManager.UpdateDamage(card.GetPlayingCard(), card.damage);
            }
            else if (card.luckyNumber + player.luckyNumber == 14)
            {
                incomingDamage += 7;
                player.TakeDamage(-7);
                player.block += 7;
            }

            // Can Enemy Block
            if (incomingDamage > enemyBeingHit.block)
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

            DiscardCard(card);
        }
        else
        {
            card.SetToStartPosition();
        }
    }

    public void HealPlayer(Card card)
    {
        if(player.stamina >= card.stamina)
        {
            int incomingHealth = card.damage;
            if (card.luckyNumber == player.luckyNumber)
            {
                incomingHealth *= 7;
                player.TakeDamage(-incomingHealth);
                player.AddBlock(7);
                player.IncreaseMaxStamina();

                // Increase Card Damage/Block/Heal Permanently
                card.damage += 3;
                card.GetPlayingCard().damage = card.damage;
                cardManager.UpdateDamage(card.GetPlayingCard(), card.damage);
            }
            else if (card.luckyNumber + player.luckyNumber == 14)
            {
                incomingHealth += 7;
                player.TakeDamage(-incomingHealth);
                player.AddBlock(7);
            }
            else
            {
                player.TakeDamage(-card.damage);
            }

            player.UseStamina(card.stamina);
            DiscardCard(card);
        }
        else
        {
            card.SetToStartPosition();
        }

    }

    public void BlockPlayer(Card card)
    {
        if (player.stamina >= card.stamina)
        {
            int incomingBlock = card.block;
            if (card.luckyNumber == player.luckyNumber)
            {
                incomingBlock *= 7; // *7 block
                player.TakeDamage(-7); // +7 health
                player.AddBlock(incomingBlock); // Adds block
                player.IncreaseMaxStamina(); // Increase Max Stamina by 1

                // Increase Card Damage/Block/Heal Permanently
                card.block += 3;
                card.GetPlayingCard().block = card.block;
                cardManager.UpdateBlock(card.GetPlayingCard(), card.block);
            }
            else if (card.luckyNumber + player.luckyNumber == 14)
            {
                incomingBlock += 7;
                player.TakeDamage(-7);
                player.AddBlock(incomingBlock);
            }
            else
            {
                player.AddBlock(card.block);
            }

            player.UseStamina(card.stamina);
            DiscardCard(card);
        }
        else
        {
            card.SetToStartPosition();
        }
    }

    public void EndTurn()
    {
        // Enemy Attacks
        PerformEnemyAttacks();

        // Player Draws til 5 cards
        int numCardsInHand = cardsInHand.Count;
        int cardsToDraw = 5 - numCardsInHand;
        if(cardsToDraw < 0) cardsToDraw = 0;
        DrawCards(cardsToDraw);
        player.SetStamina(player.maxStamina);
    }

    private void DiscardCard(Card card)
    {
        int newLuck = Random.Range(2, 13);
        cardManager.UpdateLuckyNumber(card.GetPlayingCard(), newLuck);
        card.GetNewLuckyNumber(newLuck); // Gets new Lucky Number
        // ADD TO DISCARD PILE
        discardPile.Add(card.GetPlayingCard());
        // REMOVE FROM HAND
        playerHand.Remove(card.GetPlayingCard());
        cardsInHand.Remove(card);

        // NEED TO MAKE THIS BETTER WITH OBJECT POOLING IF TIME PERMITS
        Destroy(card.gameObject);
    }

    private void PerformEnemyAttacks()
    {
        foreach (Enemy enemy in enemies)
        {
            //Checks if player has blocks
            int incomingDamage = enemy.GetCurrentHit();
            if (incomingDamage > player.block)
            {
                incomingDamage -= player.block;
                player.block = 0;
            }
            else
            {
                player.block -= incomingDamage;
                incomingDamage = 0;
            }

            //Update Block Number
            player.UpdateShieldUI();

            // Player Takes Damage
            player.TakeDamage(incomingDamage);

            // Enemy Displays Next Hit
            enemy.GetNextHit();
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void GameWin()
    {
        Player.Instance.nipCoin += 250;
        gameOver = true;
        gameWinScreen.SetActive(true);
    }

    public void Restart()
    {
        player.ResetPlayer();
        cardManager.ResetCardManager();
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
