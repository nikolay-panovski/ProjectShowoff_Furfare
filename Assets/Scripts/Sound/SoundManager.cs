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
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if (scene.name == "MainMenuVisual")
        {
            Source.clip = MainMenu;
        }
        if (scene.name == "AssetsMaterialsLevel")
        {
            Source.clip = FilipMusic;
        }
        if (scene.name == "Level_Adri")
        {
            Source.clip = AdriMusic;
        }
        if (scene.name == "GardenFlat")
        {
            Source.clip = MonaMusic;
        }
        if (scene.name == "WinningArt")
        {
            Source.clip = WinningScreen;
        }
        if (SoundPlay.MusicIsOn)
        {
            Source.Play();
        }
    }
    public static void SoundToggle()
    {
        if (SoundPlay.MusicIsOn)
        {
            Source.Stop();
        }
        if (SoundPlay.MusicIsOn == false)
        {
            Source.Play();
        }
        SoundPlay.MusicIsOn = !SoundPlay.MusicIsOn;
    }
    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
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
