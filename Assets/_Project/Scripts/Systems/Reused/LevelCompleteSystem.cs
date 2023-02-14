using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class LevelCompleteSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<Services.AnalyticsManager> _gameAnalitics = default;
        private readonly EcsFilterInject<Inc<LevelCompleteRequest>> _levelCompleteFilter = default;

        public void Init(EcsSystems systems)
        {
            _sceneContext.Value.WinWindow.buttonForward.onClick.AddListener(() =>
                {
                // _sceneContext.Value.blackout.SmoothBlackout();
                _sceneContext.Value.WinWindow.SetActiveWindow(false, () =>
                    {
                        _sceneContext.Value.blackout.SmoothBlackout(() =>
                        {
                            systems.GetWorld().GetPool<FinalizeRequestComponent>().Add(systems.GetWorld().NewEntity()).Value = LevelEndState.WIN;
                        });
                    });
                }
            );
        }

        public void Run(EcsSystems systems)
        {
            foreach (var levelComplete in _levelCompleteFilter.Value)
            {
                systems.GetWorld().DelEntity(levelComplete);

                _sceneContext.Value.WinWindow.SetActiveWindow(true);

                _gameAnalitics.Value.LevelWinGA(ILevelLink.CurrentLevel.Index, ILevelLink.CurrentLevel.DeathPerLevel);
            }
        }
    }
}
