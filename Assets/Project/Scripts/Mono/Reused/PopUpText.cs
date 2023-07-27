using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public TMP_Text textUP;

    public float durationUP;
    public float durationFade;
    public float positionUP;
    public Ease ease;

    private void Start()
    {
        transform.DOLocalMove(transform.localPosition + Vector3.up * positionUP, durationUP).OnComplete(() => Destroy(gameObject));
        textUP.DOFade(0f, durationFade).SetEase(ease);
    }
}
