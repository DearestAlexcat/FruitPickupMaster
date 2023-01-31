using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SliderHelper : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] TMP_Text text;


    Sequence sequence;

    public float currentValue;

    public bool CheckExceededValue() 
    {
        return currentValue > slider.maxValue;
    }

    public void InitSliderBorder(float min, float max, bool txt = false)
    {
        currentValue = slider.value = 0f;
        slider.minValue = min;
        slider.maxValue = max;

        if(txt)
        {
            text.text = min + "/" + max;
        }
    }

    public void ChangeValue(float value)
    {
        sequence.Kill();
        sequence = DOTween.Sequence().Append(slider.DOValue(value, 0.6f));
    }

    public void ChangeValueWithText(float value)
    {
        sequence.Kill();
        sequence = DOTween.Sequence().Append(slider.DOValue(value, 0.6f));

        currentValue = value;

        text.text = value + "/" + slider.maxValue; //(value > slider.maxValue ? $"<color=\"red\">{value}</color>": $"{value}") + "/" + slider.maxValue;
    }

    public void ResetSlider(System.Action action)
    {
        slider.DOValue(0f, 1f).OnComplete(() => action.Invoke());
    }
}
