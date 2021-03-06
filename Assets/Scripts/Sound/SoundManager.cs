using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip MainMenu;
    public AudioClip FilipMusic;
    public AudioClip AdriMusic;
    public AudioClip MonaMusic;
    public AudioClip WinningScreen;
    static AudioSource Source;
    public AudioSource SoundtrackSource;
    public bool MusicIsOn;
    public bool SoundIsOn;

    private static SoundManager _i;
    public static SoundManager i
    {
        get
        {
            return _i;
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(mode);
        if (scene.name == "MainMenuVisual")
        {
            SoundtrackSource.clip = MainMenu;

        }
        if (scene.name == "AssetsMaterialsLevel")
        {
            SoundtrackSource.clip = FilipMusic;
            if (MusicIsOn)
            {
                SoundtrackSource.Play();
            }
        }
        if (scene.name == "Level_Adri")
        {
            SoundtrackSource.clip = AdriMusic;
            if (MusicIsOn)
            {
                SoundtrackSource.Play();
            }
        }
        if (scene.name == "GardenFlat")
        {
            SoundtrackSource.clip = MonaMusic;
            if (MusicIsOn)
            {
                SoundtrackSource.Play();
            }
        }
        if (scene.name == "WinningArt")
        {
            SoundtrackSource.clip = WinningScreen;
            if (MusicIsOn)
            {
                SoundtrackSource.Play();
            }
        }
        
    }
    public void MusicToggle()
    {
        if (MusicIsOn)
        {
            SoundtrackSource.Stop();
        }
        if (MusicIsOn == false)
        {
            SoundtrackSource.Play();
        }
    }
    public void SoundToggle()
    {
        if (SoundIsOn)
        {
            Source.enabled = false;
        }
        if (MusicIsOn == false)
        {
            Source.enabled = true;
        }
    }
    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public SoundAudioClip[] soundAudioClipArray;
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundPlay.Sound sound;
        public AudioClip audioClip;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (i != null && i != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _i = this;
        }
        Source = this.GetComponent<AudioSource>();
    }
}
