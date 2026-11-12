using UnityEngine;

public class Tim : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }
    public bool canInteract;
    public AudioClip hint;
    public AudioClip timVO;
    private bool hintGiven;
    private BoxCollider2D hintBox;
    public CircleCollider2D promptCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hintBox = GetComponent<BoxCollider2D>();
        IsInteractable = true;
        canInteract = true;
        hintGiven = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && hintGiven == false)
        {
            hintGiven = true;
            hintBox.enabled = false;
            promptCollider.enabled = true;
            AudioManager.Instance?.sfxSource.PlayOneShot(hint);
            HUDCanvas.Instance.contextPrompt.SetActive(false);
        }
    }

    public void Interact()
    {
        if (canInteract)
        {
            AudioManager.Instance?.sfxSource.PlayOneShot(timVO);
            canInteract = false;
            IsInteractable = false;
            HUDCanvas.Instance.contextPrompt.SetActive(false);
            CardManager.Instance.AddTimsCard();
            PopUp.Instance.SetText("You gained Tim's Card!");
            PopUp.Instance.ShowPopUp();
        }
        
    }
}
