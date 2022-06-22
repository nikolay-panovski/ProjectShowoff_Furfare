using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVoiceLine : MonoBehaviour
{
    AudioSource _as;
    public AudioClip[] CharChooseOrThrow;
    public AudioClip[] WinOrHit;
    public void Awake()
    {
        _as = this.GetComponent<AudioSource>();
    }
    public void Throw()
    {
        int num = Random.Range(0, 2);
        if (num == 1)
        {
            _as.clip = CharChooseOrThrow[Random.Range(0, CharChooseOrThrow.Length)];
            _as.PlayOneShot(_as.clip);
        }
    }
    public void ChooseCharacter()
    {
        _as.clip = CharChooseOrThrow[Random.Range(0, CharChooseOrThrow.Length)];
        _as.PlayOneShot(_as.clip);
    }
    public void WinOrGetsHit()
    {
        _as.clip = WinOrHit[Random.Range(0, WinOrHit.Length)];
        _as.PlayOneShot(_as.clip);
    }
}
