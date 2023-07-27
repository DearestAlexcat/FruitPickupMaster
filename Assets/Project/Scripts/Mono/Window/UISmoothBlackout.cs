using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISmoothBlackout : MonoBehaviour
{
    public Image panel;
    public Ease ease;
    public float showDuration = 0.2f;
    public float hideDuration = 0.2f;
    public float minAlpha = 0f;
    public float maxAlpha = 1f;

    public void SetColorAlpha(float a)
    {
        Color c = panel.color;
        c.a = a;
        panel.color = c;
    }

    public Tween SmoothBlackout(float show_duration, System.Action onComplete = null)
    {
        SetColorAlpha(minAlpha);

        return panel.DOFade(maxAlpha, show_duration).SetEase(ease).OnComplete(() => { onComplete?.Invoke(); });
    }

    public Tween DisableSmoothBlackout(float hide_duration, System.Action onComplete = null)
    {
        return panel.DOFade(minAlpha, hide_duration).SetEase(ease).OnComplete(() => { onComplete?.Invoke(); });
    }

    public Tween SmoothBlackout(System.Action onComplete = null)
    {
        SetColorAlpha(minAlpha);

        return panel.DOFade(maxAlpha, showDuration).SetEase(ease).OnComplete(() => { onComplete?.Invoke(); });
    }

    public Tween DisableSmoothBlackout(System.Action onComplete = null)
    {
        return panel.DOFade(minAlpha, hideDuration).SetEase(ease).OnComplete(() => { onComplete?.Invoke(); });
    }
}
