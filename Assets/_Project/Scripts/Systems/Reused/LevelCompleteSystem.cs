using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class LevelCompleteSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<Services.AnalyticsManager> _gameAnalitics = default;

        public void Init(EcsSystems systems)
        {
            Service<UI>.Get().WinWindow.buttonForward.onClick.AddListener(() =>
                {
                    Service<UI>.Get().WinWindow.SetActiveWindow(false, () =>
                    {
                        Service<UI>.Get().blackout.SmoothBlackout(() =>
                        {
                            systems.GetWorld().GetPool<FinalizeRequestComponent>().Add(systems.GetWorld().NewEntity()).Value = LevelEndState.WIN;
                        });
                    });
                }
            );
        }

        public void Run(EcsSystems systems)
        {
           
        }
    }
}
