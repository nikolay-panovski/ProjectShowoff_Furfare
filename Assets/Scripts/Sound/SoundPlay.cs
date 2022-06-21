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
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
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
