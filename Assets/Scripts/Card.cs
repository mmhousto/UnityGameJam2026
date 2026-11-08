using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardType type;
    public string cardName;
    public int damage;
    public int stamina;
    public int block;
    public int luckyNumber;
    public bool uiCard;

    public TextMeshProUGUI typeUI;
    public TextMeshProUGUI nameUI;
    public TextMeshProUGUI dmgHealBlockUI;
    public TextMeshProUGUI stamUI;
    public TextMeshProUGUI luckUI;

    private PlayingCard playingCard;
    private GameObject collidingObject;
    private SpriteRenderer sprite;
    private Canvas canvas;
    private Vector3 baseScale;
    private bool collidingEnemy, collidingPlayer;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        baseScale = transform.localScale;
        sprite = GetComponent<SpriteRenderer>();
        canvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (typeUI.text != type.ToString())
            typeUI.text = type.ToString();

        if (nameUI.text != cardName)
            nameUI.text = cardName;

        if (stamUI.text != stamina.ToString())
            stamUI.text = stamina.ToString();

        if (luckUI.text != luckyNumber.ToString())
            luckUI.text = luckyNumber.ToString();

        if ((type == CardType.Attack || type == CardType.Heal) && dmgHealBlockUI.text != damage.ToString())
            dmgHealBlockUI.text = damage.ToString();
        else if(type == CardType.Block && dmgHealBlockUI.text != block.ToString())
            dmgHealBlockUI.text = block.ToString();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !uiCard)
        {
            collidingEnemy = true;
            collidingObject = other.gameObject;
        }
        else if (other.CompareTag("Player") && !uiCard)
        {
            collidingPlayer = true;
            collidingObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !uiCard)
        {
            collidingEnemy = false;
            collidingObject = null;
        }
        else if (other.CompareTag("Player") && !uiCard)
        {
            collidingPlayer = false;
            collidingObject = null;
        }
    }

    private void OnMouseEnter()
    {
        if (!uiCard)
        {
            transform.localScale *= 1.5f;
            sprite.sortingOrder += 2;
            canvas.sortingOrder += 2;
        }
    }

    private void OnMouseExit()
    {
        if (!uiCard)
        {
            transform.localScale = baseScale;
            sprite.sortingOrder -= 2;
            canvas.sortingOrder -= 2;
        }
    }

    private void OnMouseDrag()
    {
        if (!uiCard)
        {
            if(transform.localScale != baseScale)
                transform.localScale = baseScale;
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = 10f;
            transform.position = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        }

    }

    private void OnMouseUp()
    {
        if (uiCard) return;

        if (!collidingPlayer && !collidingEnemy)
        {
            transform.position = startPos;
        }

        if (collidingEnemy && type == CardType.Attack)
        {
            GameManager.Instance.AttackEnemy(collidingObject, this);
        }
        else if(collidingPlayer && type == CardType.Heal)
        {
            GameManager.Instance.HealPlayer(this);
        }
        else if (collidingPlayer && type == CardType.Block)
        {
            GameManager.Instance.BlockPlayer(this);
        }
    }

    public void UpdateStartPosition()
    {
        startPos = transform.position;
    }

    public void SetToStartPosition()
    {
        transform.position = startPos;
    }

    public void SetUpCard(PlayingCard card)
    {
        playingCard = card;
        type = playingCard.type;
        name = playingCard.cardName;
        cardName = playingCard.cardName;
        damage = playingCard.damage;
        stamina = playingCard.stamina;
        block = playingCard.block;
        luckyNumber = playingCard.luckyNumber;

    }

    public PlayingCard GetPlayingCard()
    {
        return playingCard;
    }

    public void GetNewLuckyNumber(int newLucky)
    {
        playingCard.luckyNumber = newLucky;
        luckyNumber = newLucky;
    }

}
