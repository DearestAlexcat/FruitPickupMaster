using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMPTextHelper : MonoBehaviour
{
    [SerializeField] string pattern;
    [SerializeField] TMP_Text text;

    public void SetText(string value)
    {
        text.text = pattern + value;
    }

    public void SetText(int value)
    {
        text.text = pattern + value;
    }

    public void SetText(float value)
    {
        text.text = pattern + value;
    }
}
