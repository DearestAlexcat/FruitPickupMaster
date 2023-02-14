using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    class ChangeStateSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
      //  private readonly EcsCustomInject<UI> _ui = default;

        private readonly EcsFilterInject<Inc<ChangeStateEvent>> _stateFilter = default;

        private readonly EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _stateFilter.Value)
            {
                var state = _stateFilter.Pools.Inc1.Get(entity).NewGameState;

                _runtimeData.Value.GameState = state;
                      
                switch (state)
                {
                    case GameState.BEFORE:
                     //   _ui.Value.MenuScreen.Show(true);
                     //   _ui.Value.MenuScreen.Level.text = $"Level {_runtimeData.Value.LevelId + 1}";
                        
                      //  _ui.Value.GameScreen.Show(false);
                        break;
                    case GameState.PLAYING:
                     //   _runtimeData.Value.LevelStartedTime = Time.realtimeSinceStartup;
                        
                      //  _ui.Value.MenuScreen.Show(false);
                        
                     //   _ui.Value.GameScreen.Level.text = $"Level {_runtimeData.Value.LevelId + 1}";
                      //  _ui.Value.GameScreen.Show(true);
                        break;
                    case GameState.WIN:
                        Progress.CurrentLevel++;
                        
                       // _ui.Value.GameScreen.Show(false);
                      //  _ui.Value.WinScreen.Show(true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _world.Value.DelEntity(entity);
            }
        }
    }
}
