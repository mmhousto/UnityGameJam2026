using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    public AudioMixer masterMixer;
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;
    public AudioClip mainMenu;
    public AudioClip overWorld;
    public AudioClip battle;
    public AudioClip uiClick;
    public AudioClip collectItem;
    public AudioClip drawCard;
    public AudioClip metalPipe;
    public AudioClip playerAttack;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        
        masterMixer.SetFloat("MasterVol", GetMasterVolume());
        masterMixer.SetFloat("MusicVol", GetMusicVolume());
        masterMixer.SetFloat("SFXVol", GetSFXVolume());
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "Main Menu")
        {
            backgroundMusicSource.Stop();
            backgroundMusicSource.clip = mainMenu;
            backgroundMusicSource.Play();
            HUDCanvas.Instance?.viewDeckButton.SetActive(false);
        }
        else if(scene.name.Contains("area", System.StringComparison.CurrentCultureIgnoreCase))
        {
            backgroundMusicSource.Stop();
            backgroundMusicSource.clip = overWorld;
            backgroundMusicSource.Play();
            HUDCanvas.Instance?.viewDeckButton.SetActive(true);
        }
        else if (scene.name.Contains("Fight", System.StringComparison.CurrentCultureIgnoreCase))
        {
            backgroundMusicSource.Stop();
            backgroundMusicSource.clip = battle;
            backgroundMusicSource.Play();
            HUDCanvas.Instance?.viewDeckButton.SetActive(true);
        }
    }

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat("MasterVol", 1f);
    }

    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVol", -10f);
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVol", 10f);
    }

    public void SetMasterVolume(float soundLevel)
    {
        PlayerPrefs.SetFloat("MasterVol", soundLevel);
        masterMixer.SetFloat("MasterVol", soundLevel);
    }

    public void SetMusicVolume(float soundLevel)
    {
        PlayerPrefs.SetFloat("MusicVol", soundLevel);
        masterMixer.SetFloat("MusicVol", soundLevel);
    }

    public void SetSFXVolume(float soundLevel)
    {
        PlayerPrefs.SetFloat("SFXVol", soundLevel);
        masterMixer.SetFloat("SFXVol", soundLevel);
    }
}
