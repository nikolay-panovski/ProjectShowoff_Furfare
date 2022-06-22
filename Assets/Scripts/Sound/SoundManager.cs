using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _i;
    public static SoundManager i
    {
        get
        {
            return _i;
        }
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
