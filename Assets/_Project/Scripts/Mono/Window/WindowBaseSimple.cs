using System;
using UnityEngine;
using DG.Tweening;

namespace Client
{
    public class WindowBaseSimple : MonoBehaviour
    {
        [Header("REFERENCES")]
        public UISmoothBlackout blackout;
        public UIAnimation uIAnimation;
        public string[] UInames;

        public Action showCallbackWindow;
        public Action hideCallbackWindow;

        public virtual void OnEnable()
        {
            DOTween.Sequence()
                .Append(blackout.SmoothBlackout())
                .AppendCallback(() => ShowWindowElements());
        }

        public virtual void OnDisable() { }

        public void DisableUI(string[] UInames, Action callback)
        {
            try
            {
                DOTween.Sequence()
                    .AppendCallback(() => HideWindowElements())
                    .AppendInterval(0.5f)
                    .Append(blackout.DisableSmoothBlackout(() =>
                    {
                        gameObject.SetActive(false);
                        callback?.Invoke();
                    }));
            }
            catch { }
        }

        public void HideWindowElements()
        {
            for (int i = 0; i < UInames.Length; i++)
            {
                Service<SceneContext>.Get().ThisUIAnimation.Hide(UInames[i], () => hideCallbackWindow?.Invoke());
            }
        }

        public void ShowWindowElements()
        {
            for (int i = 0; i < UInames.Length; i++)
            {
                Service<SceneContext>.Get().ThisUIAnimation.Show(UInames[i], () => showCallbackWindow?.Invoke());
            }
        }

        public void SetActiveWindow(bool value, Action callback = null)
        {
            if (value)
                gameObject.SetActive(value);
            else DisableUI(UInames, callback);
        }
    }
}