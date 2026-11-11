using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Slider master, music, sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        master.value = AudioManager.Instance.GetMasterVolume();
        music.value = AudioManager.Instance.GetMusicVolume();
        sfx.value = AudioManager.Instance.GetSFXVolume();
    }

    public void SetMasterVolume(float soundLevel)
    {
        AudioManager.Instance.SetMasterVolume(soundLevel);
    }

    public void SetMusicVolume(float soundLevel)
    {
        AudioManager.Instance.SetMusicVolume(soundLevel);
    }

    public void SetSFXVolume(float soundLevel)
    {
        AudioManager.Instance.SetSFXVolume(soundLevel);
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioManager.Instance.sfxSource.PlayOneShot(clip);
    }
}
