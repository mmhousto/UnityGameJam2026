using UnityEngine;
using UnityEngine.UI;

public class PlayUIButtonClick : MonoBehaviour
{
    private Button button;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayUIClickSound);
    }

    public void PlayUIClickSound()
    {
        audioManager?.sfxSource.PlayOneShot(audioManager?.uiClick);
    }
}
