using System;
using UnityEngine;

public class FishingRodAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private string animationName;
    [SerializeField] private string reverseAnimationName;
    [SerializeField] private int animatorLayerIndex = 0;

    private string currentAnimation = "";

    public void UpdateAnimation(float pressure, float lastPressure)
    {
        float animationSpeed = (pressure - lastPressure) * speedMultiplier;

        if (animationSpeed < 0)
            PlayNewAnimation(reverseAnimationName);
        else
            PlayNewAnimation(animationName);

        animator.speed = MathF.Abs(animationSpeed);
    }

    private void PlayNewAnimation(string animationName)
    {
        float reversedFrameOffset = 1f - FrameOffset();
        if (currentAnimation != animationName)
        {
            animator.Play(animationName, animatorLayerIndex, reversedFrameOffset);
            currentAnimation = animationName;
        }
    }

    private float FrameOffset()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(currentAnimation))
            return stateInfo.normalizedTime % 1f;
        return 0f;
    }
}
