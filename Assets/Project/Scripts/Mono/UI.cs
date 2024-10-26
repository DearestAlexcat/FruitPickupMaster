using DG.Tweening;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{ 
    public class UI : MonoBehaviour
    {
        [field: SerializeField] public WindowWinSimple WinWindow { get; set; }
        [field: SerializeField] public UISmoothBlackout Blackout { get; set; }
        [field: SerializeField] public UIAnimation ThisUIAnimation { get; set; }
        [field: SerializeField] public TMPTextHelper LevelLabel { get; set; }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void DisableSmoothBlackoutUI()
        {
            Blackout.DisableSmoothBlackout(EnvironmentInitialize);
        }

        private void EnvironmentInitialize()
        {
            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() => {
                    ThisUIAnimation.Show(UIKEY.LEVEL_LABEL, () => {
                        Service<EcsWorld>.Get().ChangeState(GameState.PLAYING);
                    });
                });
        }
    }
}