using UnityEngine;
using Vuforia;

public class AnimationManager : DefaultObserverEventHandler
{
    // get animator component
    [SerializeField] private Animator introAnimator;
    private bool hasPlayedIntro = false;

    // play intro animation on tracking found
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        
        // Only play intro animation if it hasn't played yet
        if (!hasPlayedIntro && introAnimator != null)
        {
            introAnimator.Play("IntroAnim");
            hasPlayedIntro = true;
        }
    }

    // reset hasPlayedIntro on tracking loss
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        
        hasPlayedIntro = false;
    }   
}