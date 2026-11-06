using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInteract : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }

    public void Interact()
    {
        SceneManager.LoadScene(1);
    }
}
