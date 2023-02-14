using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    class InitializeSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
       // private readonly EcsCustomInject<UI> _ui = default;
        private readonly EcsWorldInject _world = default;

        public void Init(EcsSystems systems) 
        {
          //  _ui.Value.CloseAll();
            _runtimeData.Value.LevelId = Progress.CurrentLevel;
            _world.Value.ChangeState(GameState.BEFORE);

            //todo continue intialization logic

        }
    }
}