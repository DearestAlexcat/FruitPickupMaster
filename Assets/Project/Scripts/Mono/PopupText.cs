using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Client
{
    public class PopupText : MonoBehaviour
    {
        public TMP_Text textUP;

        public float durationUP;
        public float durationFade;
        public float positionUP;
        public Ease ease;

        void Start()
        {
            transform.DOLocalMove(transform.localPosition + Vector3.up * positionUP, durationUP)
                .SetLink(gameObject)
                .OnComplete(() => Destroy(gameObject));
            
            textUP.DOFade(0f, durationFade)
                  .SetLink(textUP.gameObject)
                  .SetEase(ease);
        }
    }
}
