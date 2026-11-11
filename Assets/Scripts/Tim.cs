using UnityEngine;

public class Tim : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }
    public bool canInteract;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsInteractable = true;
        canInteract = true;
    }
    
    public void Interact()
    {
        if (canInteract)
        {
            canInteract = false;
            IsInteractable = false;
            HUDCanvas.Instance.contextPrompt.SetActive(false);
            CardManager.Instance.AddTimsCard();
            PopUp.Instance.SetText("You gained Tim's Card!");
            PopUp.Instance.ShowPopUp();
        }
        
    }
}
