using UnityEngine;
using UnityEngine.InputSystem;

public class Card : MonoBehaviour
{
    public enum CardType { Attack, Block, Heal }
    public CardType type;

    private Vector3 baseScale;
    private bool collidingEnemy, collidingPlayer;
    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            collidingEnemy = true;
        }
        else if (other.CompareTag("Player"))
        {
            collidingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            collidingEnemy = false;
        }
        else if (other.CompareTag("Player"))
        {
            collidingPlayer = false;
        }
    }

    private void OnMouseEnter()
    {
        transform.localScale *= 1.5f;
    }

    private void OnMouseExit()
    {
        transform.localScale = baseScale;
    }

    private void OnMouseDrag()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnMouseUp()
    {
        if (!collidingPlayer && !collidingEnemy)
        {
            transform.position = startPos;
        }
    }

}
