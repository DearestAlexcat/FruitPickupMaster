using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class UIElement
{
    public UIKEY key;
    public RectTransform rectTransform;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float duration;
    public Ease easeStart;
    public Ease easeEnd;
}

public enum UIKEY
{
    LEVEL_LABEL = 0,
    LEVEL_STATE_TEXT = 1,
    NEXT_LEVEL_BUTTON = 2,
}

public class UIAnimation : MonoBehaviour
{
    public List<UIElement> uiElements;

    private UIElement GetDataElement(UIKEY key)
    {
        return uiElements.Single((x) => x.key == key);
    }

    public void SetStartPosition(UIKEY key)
    {
        var element = GetDataElement(key);
        element.rectTransform.anchoredPosition = element.startPosition;
    }

    public void SetEndPosition(UIKEY key)
    {
        var element = GetDataElement(key);
        element.rectTransform.anchoredPosition = element.endPosition;
    }

    public void Show(UIKEY key, TweenCallback callback = null)
    {
        var element = GetDataElement(key);
        element.rectTransform.DOAnchorPos(element.endPosition, element.duration)
                             .SetEase(element.easeEnd)
                             .SetLink(element.rectTransform.gameObject)
                             .OnKill(callback);
    }
    
    public void Hide(UIKEY key, TweenCallback callback = null)
    {
        var element = GetDataElement(key);
        element.rectTransform.DOAnchorPos(element.startPosition, element.duration)
                             .SetEase(element.easeStart)
                             .SetLink(element.rectTransform.gameObject)
                             .OnKill(callback);
    }
}