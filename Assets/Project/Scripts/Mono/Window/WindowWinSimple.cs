using UnityEngine.UI;
using System.Collections;
using UnityEngine;

namespace Client
{
    public class WindowWinSimple : WindowBaseSimple
    {
        [SerializeField] Button buttonForward;
        [SerializeField] float confettiDisplayTime;

        private void Awake()
        {
            uIAnimation.SetStartPosition(UIKEY.LEVEL_STATE_TEXT);
            uIAnimation.SetStartPosition(UIKEY.NEXT_LEVEL_BUTTON);

            buttonForward.enabled = false;

            ButtonForwardInitialize();

            ShowCallbackWindow += () => buttonForward.enabled = true;
        }

        private void ButtonForwardInitialize()
        {
            buttonForward.onClick.AddListener(() =>
            {
                buttonForward.enabled = false;

                SetActive(false, static () =>
                {
                    Service<UI>.Get().Blackout.SmoothBlackout(
                        Levels.LoadCurrent
                    );
                });
            });
        }

        private IEnumerator DisplayWindow()
        {
            yield return new WaitForSeconds(confettiDisplayTime);
            base.OnEnable();
        }

        public override void OnEnable()
        {
            StartCoroutine(DisplayWindow());
        }
    }
}