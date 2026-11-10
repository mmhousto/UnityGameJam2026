using UnityEngine;

public class Tim : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }
    public bool canInteract;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canInteract = true;
    }
    
    public void Interact()
    {
        if (canInteract)
        {
            canInteract = false;
            CardManager.Instance.AddTimsCard();
        }
        
    }
}
