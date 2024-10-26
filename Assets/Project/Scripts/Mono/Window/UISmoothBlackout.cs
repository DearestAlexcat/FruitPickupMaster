using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISmoothBlackout : MonoBehaviour
{
    public Image blackout;
    public Ease easeShow;
    public Ease easeHide;
    public float showDuration = 0.2f;
    public float hideDuration = 0.2f;
    public float minAlpha = 0f;
    public float maxAlpha = 1f;

    public void SetColorAlpha(float a)
    {
        Color c = blackout.color;
        c.a = a;
        blackout.color = c;
    }

    public Tween SmoothBlackout(System.Action onComplete = null)
    {
        SetColorAlpha(minAlpha);
        return blackout.DOFade(maxAlpha, showDuration).SetEase(easeShow).OnComplete(onComplete != null ? onComplete.Invoke : null);
    }

    public Tween DisableSmoothBlackout(System.Action onComplete = null)
    {
        SetColorAlpha(maxAlpha);
        return blackout.DOFade(minAlpha, hideDuration).SetEase(easeHide).OnComplete(onComplete != null ? onComplete.Invoke : null);
    }

    public Tween SmoothBlackout(float showDuration, System.Action onComplete = null)
    {
        SetColorAlpha(minAlpha);
        return blackout.DOFade(maxAlpha, showDuration).SetEase(easeShow).OnComplete(onComplete != null ? onComplete.Invoke : null);
    }

    public Tween DisableSmoothBlackout(float hideDuration, System.Action onComplete = null)
    {
        SetColorAlpha(maxAlpha);
        return blackout.DOFade(minAlpha, hideDuration).SetEase(easeHide).OnComplete(onComplete != null ? onComplete.Invoke : null);
    }
}
