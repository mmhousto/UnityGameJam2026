using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInteract : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }
    public int sceneToLoad = 1;

    private void Start()
    {
        IsInteractable = true;
    }

    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
