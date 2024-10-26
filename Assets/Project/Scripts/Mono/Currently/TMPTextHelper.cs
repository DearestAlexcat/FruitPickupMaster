using UnityEngine;
using TMPro;

public class TMPTextHelper : MonoBehaviour
{
    [SerializeField] string pattern;
    [SerializeField] TMP_Text text;

    public void SetText(int value)
    {
        text.text = pattern + value;
    }
}
