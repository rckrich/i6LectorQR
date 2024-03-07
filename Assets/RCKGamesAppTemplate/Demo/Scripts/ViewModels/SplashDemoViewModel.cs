using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDemoViewModel : ViewModel
{
    [Header("Animation Clip Lenght Reference")]
    public AnimationClip logoAnimation;

    private float animationTime = 0f;
    private WaitForSeconds waitForSeconds = null;

    private void Start()
    {
        Initialize();
    }

    public override void Initialize(params object[] list)
    {
        NewScreenManager.instance.SetHeaderViewActive(false);

        animationTime = logoAnimation.length;
        waitForSeconds = new WaitForSeconds(animationTime);
        StartCoroutine(CR_WaitForLogoAnimation());
    }

    private IEnumerator CR_WaitForLogoAnimation()
    {
        yield return waitForSeconds;
        NewScreenManager.instance.ChangeToMainView(ViewID.LogInDemoViewModel);
    }
}
