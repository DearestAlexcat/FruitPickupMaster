using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class LevelCompleteSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        private readonly EcsFilterInject<Inc<LevelCompleteRequest>> _completeFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var item in _completeFilter.Value)
            {
                systems.GetWorld().DelEntity<LevelCompleteRequest>(item);

                DOTween.Sequence()
                  .AppendInterval(_staticData.Value.pauseBeforeEnd)
                  .AppendCallback(() =>
                  {
                      systems.GetWorld().ChangeState(GameState.LEVEL_COMPLETE);
                      systems.GetWorld().NewEntity<CameraZoomRequest>();
                  });
            }
        }
    }
}
