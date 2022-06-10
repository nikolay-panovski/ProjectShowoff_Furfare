using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
<<<<<<< Updated upstream
    public AudioSource portalSource;

=======
>>>>>>> Stashed changes
    //Projectiles
    public AudioSource pro_Pick_Up;
    public AudioSource pro_Spawn;

    //Points
    public AudioSource points;

    //Events
    public AudioSource event_On;
    public AudioSource event_Off;

    //Countdowns
    public AudioSource countdown_Start;
    public AudioSource countdown_Finish;

    //Controller
    public AudioSource cancel_Button;
    public AudioSource selected_Button;
    public AudioSource snapping_Options;

    public AudioSource character_Stunned;
<<<<<<< Updated upstream

    public void PortalSound()
    {
        portalSource.Play();
    }
=======
    public bool IamAPortal;
>>>>>>> Stashed changes

    //Point
    public void Points()
    {
        points.Play();
    }

    //Events
    public void Event_On()
    {
        event_On.Play();
    }
    public void Event_Off()
    {
        event_Off.Play();
    }

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
