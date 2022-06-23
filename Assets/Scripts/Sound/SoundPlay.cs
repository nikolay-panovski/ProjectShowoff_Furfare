using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundPlay
{
    public enum Sound
    {
        portalSource,
        projectile,
        points,
        countdown_Start,
        countdown_Finish,
        cancel_Button,
        selected_Button,
        character_Stunned,
        bush,
        couch,
        sack,
        wateringCan,
        wood
    }
    public static bool MusicIsOn;
    public static bool SoundIsOn;
    public static void PlaySound(Sound sound)
    {
        if (SoundIsOn)
        {
            GameObject soundGameObject = GameObject.FindGameObjectWithTag("SoundManager");
            AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        Debug.Log("Sound Called: " + sound);
        foreach (SoundManager.SoundAudioClip soundAudioClip in SoundManager.i.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "not found!");
        return null;
    }
}
