using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class LevelLoseSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<Services.AnalyticsManager> _gameAnalitics = default;

        private readonly EcsFilterInject<Inc<LevelLoseRequest>> _filter = default;

        public void Init(EcsSystems systems)
        {
            //_sceneContext.Value.DieWindow.buttonRestart.onClick.AddListener(() =>
            //{
            //    _sceneContext.Value.blackout.SmoothBlackout();
            //    _sceneContext.Value.DieWindow.SetActiveWindow(false, () => systems.GetWorld().GetPool<FinalizeRequestComponent>().Add(systems.GetWorld().NewEntity()).Value = LevelEndState.LOSE);           
            //});
        }

        public void Run(EcsSystems systems)
        {
            foreach (var levelLose in _filter.Value)
            {
                //systems.GetWorld().DelEntity(levelLose);

                //_sceneContext.Value.DieWindow.SetActiveWindow(true);

                //_gameAnalitics.Value.LevelLoseGA(ILevelLink.CurrentLevel.Index);
            }
        }
    }
}