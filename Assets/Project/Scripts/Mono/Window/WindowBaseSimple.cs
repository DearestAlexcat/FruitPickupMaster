using System;
using UnityEngine;
using DG.Tweening;

namespace Client
{
    public class WindowBaseSimple : MonoBehaviour
    {
        [Header("REFERENCES")]
        public UIAnimation uIAnimation;
        public UIKEY[] uIKeys;

        public Action ShowCallbackWindow { get; set; }
        public Action HideCallbackWindow { get; set; }

        public virtual void OnEnable()
        {
            ShowWindowElements();
        }

        public virtual void OnDisable() { }

        void ShowWindowElements()
        {
            for (int i = 0; i < uIKeys.Length; i++)
            {
                Service<UI>.Get().ThisUIAnimation.Show(uIKeys[i], ShowCallbackWindow != null ? ShowCallbackWindow.Invoke : null);
            }
        }

        void HideWindowElements()
        {
            for (int i = 0; i < uIKeys.Length; i++)
            {
                Service<UI>.Get().ThisUIAnimation.Hide(uIKeys[i], HideCallbackWindow != null ? HideCallbackWindow.Invoke : null);
            }
        }

        public void SetActive(bool value, Action callback = null)
        {
            if (value) gameObject.SetActive(value);
            else DisableUI(callback);
        }

        public void DisableUI(Action callback)
        {
            try
            {
                DOTween.Sequence()
                    .AppendCallback(HideWindowElements)
                    .AppendInterval(0.5f)
                    .AppendCallback(() => {
                        gameObject.SetActive(false);
                        callback?.Invoke();
                    });
            }
            catch { }
        }
    }
}