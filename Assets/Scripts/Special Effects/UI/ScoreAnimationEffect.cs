using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

// Assuming implementation of effects like the one below in a fresh project, it might be good to
// leave flexibility for any text type to be animated (Unity UI Text, TextMeshPro, ...(?)).
// That type would only need to have the common properties in the interface to be valid,
// which Text and TextMeshPro do by default (if the special effect limits itself to tweening operations).
// But for simplicity (as to not make wrapper objects), this project will reference Text directly instead of using this interface.
/**public interface AnimatableText
{
    public Transform transform { get; set; }
    public string text { get; set; }
    public Color color { get; set; }
}
/**/

public enum ScoreModifyMode
{
    INSTANT,
    STEPWISE
}

public enum AnimateVar
{
    POSITION,
    ROTATION,
    SCALE,
    COLOR
}

[System.Serializable]
public struct ScoreAnimationConfig
{
    public AnimateVar animateVar;
    public float animDuration;
    public Ease easeType;
    [Tooltip("If true, the chosen variable will be animated via a shake. \"Values\" then corresponds to the shake strength.")]
    public bool shakeVar;
    //[Tooltip("This represents either the target values for the animated variable (relative to the starting values!), or a shake strength.")]
    [Tooltip("This represents the target values for the animated variable (relative to the starting values!).")]
    public Vector3 values;
    //[Tooltip("Multiply or add the values specified to the starting values?")]
    //public bool multiplyValues;
    public int shakeVibrato;
    [Range(0f, 90f)] public float shakeRandomness;
}

public class ScoreAnimationEffect : MonoBehaviour
{
    [SerializeField] private ScoreModifyMode scoreModifyMode = ScoreModifyMode.INSTANT;

    [Header("Animation properties")]
    [SerializeField] private List<ScoreAnimationConfig> animationsPerVar;

    private TweenParams finalAnimation;
    private Tween lastTween;

    private void Start()
    {
        finalAnimation = new TweenParams();
    }


    public void SetVisualScore(Text refText, int toScore)
    {
        switch (scoreModifyMode)
        {
            case ScoreModifyMode.INSTANT:
                refText.text = toScore.ToString();
                break;
            case ScoreModifyMode.STEPWISE:
                // set text to the next partial step as defined by step parameters here, then call this again when it is time for another step

                //finalAnimation.OnComplete(SetVisualScore(refText, toScore));
                break;
            default:
                throw new System.ArgumentException("Invalid ScoreModifyMode: " + scoreModifyMode);
        }

        AnimateScoreModification(refText);
    }

    public void AnimateScoreModification(/*AnimatableText*/Text refText)
    {
        if (lastTween != null) lastTween.Complete();

        foreach (ScoreAnimationConfig config in animationsPerVar)
        {
            finalAnimation.Clear();
            finalAnimation.SetEase(config.easeType);
            finalAnimation.SetLoops(2, LoopType.Yoyo);  //

            switch (config.animateVar)
            {
                case AnimateVar.POSITION:
                    if (config.shakeVar) { lastTween = refText.transform.DOShakePosition(config.animDuration, config.values, config.shakeVibrato, config.shakeRandomness).SetAs(finalAnimation); }
                    else { lastTween = refText.transform.DOMove(refText.transform.position + config.values, config.animDuration).SetAs(finalAnimation); }
                    break;
                case AnimateVar.ROTATION:
                    if (config.shakeVar) { lastTween = refText.transform.DOShakeRotation(config.animDuration, config.values, config.shakeVibrato, config.shakeRandomness).SetAs(finalAnimation); }
                    else { lastTween = refText.transform.DORotate(refText.transform.localRotation.eulerAngles + config.values, config.animDuration).SetAs(finalAnimation); }
                    break;
                case AnimateVar.SCALE:
                    if (config.shakeVar) { lastTween = refText.transform.DOShakeScale(config.animDuration, config.values, config.shakeVibrato, config.shakeRandomness).SetAs(finalAnimation); }
                    else { lastTween = refText.transform.DOScale(refText.transform.localScale + config.values, config.animDuration).SetAs(finalAnimation); }
                    break;
                case AnimateVar.COLOR:
                    if (config.shakeVar) { Debug.LogWarning("[ScoreAnimationEffect] Cannot shake color values"); }
                    lastTween = refText.DOColor(new Color(refText.color.r + config.values.x,
                                                          refText.color.g + config.values.y,
                                                          refText.color.b + config.values.z, 1f), config.animDuration).SetAs(finalAnimation);
                    break;
            }
        }
    }
}
