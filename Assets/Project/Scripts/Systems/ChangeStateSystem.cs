using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks;

namespace Client
{
    class ChangeStateSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        private readonly EcsFilterInject<Inc<ChangeStateEvent>> _stateFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _stateFilter.Value)
            {
                var state = _stateFilter.Pools.Inc1.Get(entity).NewGameState;

                _runtimeData.Value.GameState = state;
                      
                switch (state)
                {
                    case GameState.NONE: 

                        break;
                    case GameState.BEFORE:

                        Service<UI>.Get().ThisUIAnimation.SetStartPosition(UIKEY.LEVEL_LABEL);
                        Service<UI>.Get().LevelLabel.SetText(Progress.CurrentLevel);
                        Service<UI>.Get().DisableSmoothBlackoutUI();

                        break;
                    case GameState.PLAYING:

                        break;
                    case GameState.LEVEL_COMPLETE:
                        
                        Progress.CurrentLevel++;
                        _sceneContext.Value.ConfettiService.ShowConfetti().Forget();
                        Service<UI>.Get().ThisUIAnimation.Hide(UIKEY.LEVEL_LABEL);
                        Service<UI>.Get().WinWindow.SetActive(true);
                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                systems.GetWorld().DelEntity(entity);
            }
        }
    }
}
