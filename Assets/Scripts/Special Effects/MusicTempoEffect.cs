using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class MusicTempoEffect : MonoBehaviour
{
    [field: SerializeField] public int secondsBeforeRoundEnd { get; private set; }
    [Tooltip("This actually modifies the \"pitch\" setting of the AudioSource. The result may be faster tempo, but also squeakier sound.")]
    [SerializeField, Range(-3, 3f)] private float tempo = 1f;

    [Header("Animation properties")]
    [SerializeField] private Ease easeType;
    [SerializeField] private float animDuration;

    public void SetMusicTempo()
    {
        SoundManager.i.SoundtrackSource.DOPitch(tempo, animDuration).SetEase(easeType);
    }

    public void TurnMusicOff()
    {
        // Value tweening will only work well if there is enough time at/after the end of the round to revert changes.
        // This is not guaranteed from this effect, it might be implemented by unrelated effects.
        // (In this case, InGameUI will call this at 1 second left. However, it is not good practice outside of this project.)
        SoundManager.i.SoundtrackSource.DOPitch(1f, 1f).SetEase(easeType);
        SoundManager.i.SoundtrackSource.DOFade(0f, 1f).SetEase(easeType).OnComplete(RevertChanges);

        // On the way to stopping the music, restore the pitch before next level starts:
        //SoundManager.i.SoundtrackSource.pitch = 1f;
        //SoundManager.i.SoundtrackSource.Stop();
    }

    private void RevertChanges()
    {
        SoundManager.i.SoundtrackSource.pitch = 1f;
        SoundManager.i.SoundtrackSource.volume = 0.5f;
        SoundManager.i.SoundtrackSource.Stop();
    }
}
