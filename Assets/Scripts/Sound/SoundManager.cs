using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource portalSource;

    public AudioSource pro_Pick_Up;
    public AudioSource pro_Spawn;

    //Points
    public AudioClip points;

    //Events
    

    //Countdowns
    public AudioSource countdown_Start;
    public AudioSource countdown_Finish;

    //Controller
    public AudioSource cancel_Button;
    public AudioSource selected_Button;
    public AudioSource snapping_Options;

    public AudioSource character_Stunned;

    AudioSource _as;
    private void Start()
    {
        _as = this.GetComponent<AudioSource>();
    }
    public void PortalSound()
    {
        portalSource.Play();
    }
    public bool IamAPortal;

    //Point
    public void Points()
    {
        _as.PlayOneShot(points);
    }

    //Events
    /*public void Event_On()
    {
        _as.PlayOneShot(event_On);
    }
    public void Event_Off()
    {
        _as.PlayOneShot(event_Off);
    }*/

    //Countdown
    public void Countdown_Start()
    {
        countdown_Start.Play();
    }
    public void Countdown_Finish()
    {
        countdown_Finish.Play();
    }

    //Buttons
    public void Cancel_Button()
    {
        cancel_Button.Play();
    }
    public void Selected_Button()
    {
        selected_Button.Play();
    }
    public void Snapping_Options()
    {
        snapping_Options.Play();
    }

    public void Character_Stunned()
    {
        character_Stunned.Play();
    }
}
