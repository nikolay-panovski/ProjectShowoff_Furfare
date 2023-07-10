using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class TimeAnimationEffect : MonoBehaviour
{
    [SerializeField] private int secondsBeforeRoundEnd;
    private Text timerText;

    [Header("Animation properties")]
    [SerializeField] private Ease easeType;
    [SerializeField] private bool animatePosition;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private bool animateRotation;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private bool animateScale;
    [SerializeField] private Vector3 scaleOffset;
    [SerializeField] private bool animateColor;
    [SerializeField] private Vector3 colorRGBOffset;
    [Space(10)]
    [Tooltip("Play a countdown sound each time the timer is animated (here: each 1 second).")]
    [SerializeField] private bool alsoPlaySound;
    [SerializeField] private SoundPlay.Sound stepSound = SoundPlay.Sound.countdownBeep;

    private TweenParams animProperties;

    private void Start()
    {
        animProperties = new TweenParams();
        animProperties.SetEase(easeType);
        animProperties.SetLoops(2, LoopType.Yoyo);
        animProperties.SetRelative(true);
    }

    public void CheckForBeforeRoundEnd(int secondsRemaining)
    {
        // This is hooked to InGameUI and will run every round period (1 second).
        // As such, AnimateTimeStep will be built to only animate one independent step per call.
        // (This is also a justification for the timer animation being less flexible to modify than the score animation.)
        if (secondsRemaining >= 0 && secondsRemaining <= secondsBeforeRoundEnd)
        {
            AnimateTimeStep();
            //if (alsoPlayBeeps) SoundPlay.PlaySound(stepSound);
        }
    }

    private void AnimateTimeStep()
    {
        Sequence animSequence = DOTween.Sequence();

        if (animatePosition) animSequence.Insert(0, timerText.transform.DOMove(positionOffset, 0.5f).SetAs(animProperties));
        if (animateRotation) animSequence.Insert(0, timerText.transform.DORotate(rotationOffset, 0.5f).SetAs(animProperties));
        if (animateScale) animSequence.Insert(0, timerText.transform.DOScale(scaleOffset, 0.5f).SetAs(animProperties));
        if (animateColor) animSequence.Insert(0, timerText.DOColor(new Color(colorRGBOffset.x,
                                                                             colorRGBOffset.y,
                                                                             colorRGBOffset.z,
                                                                             1f), 0.5f).SetAs(animProperties));

        if (alsoPlaySound) SoundPlay.PlaySound(stepSound);
    }

    public void SetTimerTextRef(Text text)
    {
        timerText = text;
    }
}
