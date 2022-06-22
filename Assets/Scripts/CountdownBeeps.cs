using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownBeeps : MonoBehaviour
{
    AudioSource _as;
    Text text;
    public AudioClip countdown;
    public AudioClip countdownFinalBeep;
    // Start is called before the first frame update
    void Start()
    {
        _as = this.GetComponent<AudioSource>();
        text = this.GetComponent<Text>();
    }
    public void CountDownBeep()
    {
        _as.PlayOneShot(countdown);
    }
    public void CountDownLastBeep()
    {
        _as.PlayOneShot(countdownFinalBeep);
    }
    public void GOText()
    {
        text.text = "GO";
    }
}
