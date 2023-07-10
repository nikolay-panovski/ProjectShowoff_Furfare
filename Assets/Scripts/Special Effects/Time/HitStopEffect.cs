using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class HitStopEffect : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float finalTimeScale;

    [Header("Animation properties")]
    [SerializeField] private Ease easeType;
    [SerializeField] private float animDuration;

    // animate timeScale from default 1 to chosen lower number at round end
    public void SlowDownTime()
    {
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, finalTimeScale, animDuration).SetEase(easeType);
    }
}
