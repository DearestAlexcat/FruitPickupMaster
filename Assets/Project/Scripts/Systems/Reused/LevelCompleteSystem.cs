using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class LevelCompleteSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsFilterInject<Inc<LevelCompleteRequest>> _completeFilter = default;
        private readonly EcsFilterInject<Inc<Component<PlayerUnit>, UnitWin>> _playerFilter = default;

        public void Init(EcsSystems systems)
        {
            Service<UI>.Get().WinWindow.buttonForward.onClick.AddListener(() =>
                {
                    Service<UI>.Get().WinWindow.SetActiveWindow(false, () =>
                    {
                        Service<UI>.Get().blackout.SmoothBlackout();
                    });
                }
            );
        }

        public void Run(EcsSystems systems)
        {
            foreach (var item in _completeFilter.Value)
            {
                _world.Value.DelEntity<LevelCompleteRequest>(item);

                DOTween.Sequence()
                  .AppendInterval(_staticData.Value.pauseBeforeEnd)
                  .AppendCallback(() =>
                  {
                      SetFinalStateOfPlayer();

                      _world.Value.ChangeState(GameState.WIN);
                      _world.Value.NewEntityRef<CameraZoomRequest>();
                  });
            }
        }

        private void SetFinalStateOfPlayer()
        {
            foreach (var item in _playerFilter.Value)
            {
                var player = _playerFilter.Pools.Inc1.Get(item).Value;
                player.PlayAnimation(AnimationState.SILLYDANCING, AnimationFlags.SILLYDANCING);
            }
        }
    }
}
