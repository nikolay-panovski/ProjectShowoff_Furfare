using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Wrapper script that separates the Animator work from the other script components attached to Player.
 */
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private string throwAnimationName;
    [SerializeField] private string stunnedAnimationName;

    void Start()
    {
        // forced by hierarchy - Animator is on model itself, which is a child of the GameObject with all player-related scripts.
        // that GameObject MUST NOT have an Animator itself. (or only?)
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // ~~Main idea for this part: To initiate throw/stun animation, set bool to true.
        // To stop it, rely on exit time in the animator. But bool needs to be reset too, for which try to also use when the animation ends.
        if (HasFinishedPlayingAnimation(throwAnimationName))
        {
            SetBool("IsThrowing", false);
        }
        if (HasFinishedPlayingAnimation(stunnedAnimationName))
        {
            SetBool("IsStunned", false);
        }
    }

    public void SetFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }

    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }

    /* ~~Works only if an animation is not supposed to loop, check normalizedTime for more info if needed.
     */
    public bool HasFinishedPlayingAnimation(string stateName)
    {
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return currentStateInfo.IsName(stateName) && currentStateInfo.normalizedTime >= currentStateInfo.length;
    }
}
