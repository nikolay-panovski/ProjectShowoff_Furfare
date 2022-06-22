using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip FilipMusic;
    public AudioClip AdriMusic;
    public AudioClip MonaMusic;
    AudioSource Source;

    private static SoundManager _i;
    public static SoundManager i
    {
        get
        {
            return _i;
        }
    }
    /*private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene);
        if (scene.name == "AssetsMaterialsLevel")
        {
            Source.clip = FilipMusic;
        }
    }*/
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
