using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float maxSpeed = 4f;
    public GameObject contextPrompt;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    float xInput, yInput;
    bool interacting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if(contextPrompt == null)
        {
            contextPrompt = GameObject.FindWithTag("ContextPrompt");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (contextPrompt == null)
        {
            contextPrompt = GameObject.FindWithTag("ContextPrompt");
        }

        MovePlayer();
        FlipSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IInteractable>(out IInteractable interactable) && contextPrompt != null)
            contextPrompt.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable) && interacting)
            interactable.Interact();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable) && contextPrompt != null)
            contextPrompt.SetActive(false);
    }

    private void FlipSprite()
    {
        if(xInput > 0 && sprite.flipX == false)
            sprite.flipX = true;

        if(xInput < 0 && sprite.flipX == true)
            sprite.flipX = false;
    }

    private void MovePlayer()
    {
        Vector2 moveDir = new Vector2(xInput, yInput).normalized;
        rb.AddForce(moveDir * speed * Time.deltaTime, ForceMode2D.Impulse);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);

        if (xInput == 0 && yInput == 0)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    public void OnMove(InputValue input)
    {
        xInput = input.Get<Vector2>().x;
        yInput = input.Get<Vector2>().y;
    }

    public void OnInteract(InputValue input)
    {
        interacting = input.isPressed;
    }
}
