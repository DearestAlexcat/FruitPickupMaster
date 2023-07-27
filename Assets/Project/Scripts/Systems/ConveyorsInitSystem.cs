using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class ConveyorsInitSystem : IEcsInitSystem 
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Init(EcsSystems systems) 
        {
            foreach (var conveyorView in _sceneContext.Value.ConveyorsView)
            {
                var entity = _world.Value.NewEntity<Component<ConveyorView>>();
                _world.Value.GetEntityRef<Component<ConveyorView>>(entity).Value = conveyorView;
                _world.Value.AddEntityRef<InGroup>(entity).GroupIndex = entity;
            }
        }
    }
}