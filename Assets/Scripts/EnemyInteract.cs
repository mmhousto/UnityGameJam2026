using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyInteract : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }
    public int sceneToLoad = 1;

    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
