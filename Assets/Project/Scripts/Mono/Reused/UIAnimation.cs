using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class UIElement
{
    public string name;
    public RectTransform rectTransform;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float duration;
    public Ease easeStart;
    public Ease easeEnd;
}


public class UIAnimation : MonoBehaviour
{
    public List<UIElement> uiElements;

    private UIElement GetDataElement(string name)
    {
        return uiElements.Single((x) => x.name == name);
    }

    public void SetStartPosition(string name)
    {
        var element = GetDataElement(name);
        element.rectTransform.anchoredPosition = element.startPosition;
    }

    public void SetEndPosition(string name)
    {
        var element = GetDataElement(name);
        element.rectTransform.anchoredPosition = element.endPosition;
    }

    public void Show(string name, TweenCallback callback = null)
    {
        var element = GetDataElement(name);
        element.rectTransform.DOAnchorPos(element.endPosition, element.duration).SetEase(element.easeEnd).OnComplete(callback);
    }
    
    public void Hide(string name, TweenCallback callback = null)
    {
        var element = GetDataElement(name);
        element.rectTransform.DOAnchorPos(element.startPosition, element.duration).SetEase(element.easeStart).OnComplete(callback);
    }
}
