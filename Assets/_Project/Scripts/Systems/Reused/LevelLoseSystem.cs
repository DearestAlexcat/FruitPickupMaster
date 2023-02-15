using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class LevelLoseSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<Services.AnalyticsManager> _gameAnalitics = default;

        public void Init(EcsSystems systems)
        {
            //Service<UI>.Get().DieWindow.buttonRestart.onClick.AddListener(() =>
            //{
            //    Service<UI>.Get().blackout.SmoothBlackout();
            //    Service<UI>.Get().DieWindow.SetActiveWindow(false, () => systems.GetWorld().GetPool<FinalizeRequestComponent>().Add(systems.GetWorld().NewEntity()).Value = LevelEndState.LOSE);           
            //});
        }

        public void Run(EcsSystems systems)
        {
           
        }
    }
}