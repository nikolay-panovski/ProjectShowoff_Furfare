using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip FilipMusic;
    public AudioClip AdriMusic;
    public AudioClip MonaMusic;
    public AudioClip WinningScreen;
    AudioSource Source;

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
        if (scene.name == "AssetsMaterialsLevel")
        {
            Source.clip = FilipMusic;
            Source.Play();
        }
        if (scene.name == "Level_Adri")
        {
            Source.clip = AdriMusic;
            Source.Play();
        }
        if (scene.name == "GardenFlat")
        {
            Source.clip = MonaMusic;
            Source.Play();
        }
        if (scene.name == "WinningArt")
        {
            Source.clip = WinningScreen;
            Source.Play();
        }
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
    }
}
