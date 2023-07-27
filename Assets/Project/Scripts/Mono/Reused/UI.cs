using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{ 
    public class UI : MonoBehaviour
    {
        [field: SerializeField] public WindowWinSimple WinWindow { get; set; }
        [field: SerializeField] public UISmoothBlackout blackout { get; set; }
        [field: SerializeField] public UIAnimation ThisUIAnimation { get; set; }
        [field: SerializeField] public SliderHelper LevelProgress { get; set; }
        [field: SerializeField] public TMPTextHelper LevelLabel { get; set; }

        public void DisableSmoothBlackoutUI()
        {
            blackout.DisableSmoothBlackout(() =>
            {
                EnvironmentInitialize();
                ButtonForwardInitialize();
            });
        }

        private void ButtonForwardInitialize()
        {
            WinWindow.buttonForward.onClick.AddListener(() =>
            {
                WinWindow.SetActiveWindow(false, () =>
                {
                    blackout.SmoothBlackout(() =>
                    {
                        Levels.LoadCurrentWithSkip();
                    });
                });
            }
            );
        }

        private void EnvironmentInitialize()
        {
            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    ThisUIAnimation.Show("LevelLabel", () =>
                    {
                        //ThisUIAnimation.Show("SliderProgress");
                        SwitchToPlayingState();
                    });
                });
        }

        private void SwitchToPlayingState()
        {
            Service<EcsWorld>.Get().ChangeState(GameState.PLAYING);
        }
    }
}