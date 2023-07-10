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

public enum AnimateVar
{
    POSITION,
    ROTATION,
    SCALE,
    COLOR,
    SCORE
}

[System.Serializable]
public struct ScoreAnimationConfig
{
    [Tooltip("Config only applies if enabled. Useful for fast prototyping in play mode.")]
    public bool enabled;
    public AnimateVar animateVar;
    public float animDuration;
    public Ease easeType;
    [Tooltip("If true, the chosen variable will be animated via a shake. \"Values\" then corresponds to the shake strength.")]
    public bool shakeVar;
    //[Tooltip("This represents either the target values for the animated variable (relative to the starting values!), or a shake strength.")]
    [Tooltip("This represents the target values for the animated variable (relative to the starting values!).")]
    public Vector3 values;
    public int shakeVibrato;
    [Range(0f, 90f)] public float shakeRandomness;
}

public class ScoreAnimationEffect : MonoBehaviour
{
    [Header("Animation properties")]
    [SerializeField] private List<ScoreAnimationConfig> animationsPerVar;

    private TweenParams finalAnimation;

    private Dictionary<PlayerConfig, Sequence> playersAnimSequences = new Dictionary<PlayerConfig, Sequence>(4);

    private void Start()
    {
        finalAnimation = new TweenParams();
    }

    public void AnimateScoreModification(/*AnimatableText*/PlayerConfig ofPlayer, int toScore)
    {
        if (!playersAnimSequences.ContainsKey(ofPlayer)) playersAnimSequences.Add(ofPlayer, null);

        Sequence animSequence = playersAnimSequences[ofPlayer];

        if (animSequence != null) animSequence.Complete();
        animSequence = DOTween.Sequence();

        Text refText = ofPlayer.playerUICard.GetComponentInChildren<Text>();

        foreach (ScoreAnimationConfig config in animationsPerVar)
        {
            if (!config.enabled) continue;

            finalAnimation.Clear();
            finalAnimation.SetEase(config.easeType);
            finalAnimation.SetLoops(2, LoopType.Yoyo);  //

            switch (config.animateVar)
            {
                case AnimateVar.POSITION:
                    if (config.shakeVar) { animSequence.Insert(0, refText.transform.DOShakePosition(config.animDuration, config.values, config.shakeVibrato, config.shakeRandomness).SetAs(finalAnimation)); }
                    else { animSequence.Insert(0, refText.transform.DOMove(refText.transform.position + config.values, config.animDuration).SetAs(finalAnimation)); }
                    break;
                case AnimateVar.ROTATION:
                    if (config.shakeVar) { animSequence.Insert(0, refText.transform.DOShakeRotation(config.animDuration, config.values, config.shakeVibrato, config.shakeRandomness).SetAs(finalAnimation)); }
                    else { animSequence.Insert(0, refText.transform.DORotate(refText.transform.localRotation.eulerAngles + config.values, config.animDuration).SetAs(finalAnimation)); }
                    break;
                case AnimateVar.SCALE:
                    if (config.shakeVar) { animSequence.Insert(0, refText.transform.DOShakeScale(config.animDuration, config.values, config.shakeVibrato, config.shakeRandomness).SetAs(finalAnimation)); }
                    else { animSequence.Insert(0, refText.transform.DOScale(refText.transform.localScale + config.values, config.animDuration).SetAs(finalAnimation)); }
                    break;
                case AnimateVar.COLOR:
                    if (config.shakeVar) { Debug.LogWarning("[ScoreAnimationEffect] Cannot shake color values"); }
                    animSequence.Insert(0, refText.DOColor(new Color(refText.color.r + config.values.x,
                                                                     refText.color.g + config.values.y,
                                                                     refText.color.b + config.values.z, 1f), config.animDuration).SetAs(finalAnimation));
                    break;
                case AnimateVar.SCORE:
                    animSequence.Insert(0, DOTween.To(() => int.Parse(refText.text), x => refText.text = x.ToString(), toScore, config.animDuration));
                    break;
            }

            playersAnimSequences[ofPlayer] = animSequence;
        }
    }
}
