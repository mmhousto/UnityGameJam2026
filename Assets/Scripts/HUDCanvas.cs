using UnityEngine;
using UnityEngine.InputSystem.HID;

public class HUDCanvas : MonoBehaviour
{
    private static HUDCanvas instance;
    public static HUDCanvas Instance { get { return instance; } }

    public GameObject contextPrompt;
    public GameObject viewDeckButton;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioManager.Instance.sfxSource.PlayOneShot(clip);
    }
}
