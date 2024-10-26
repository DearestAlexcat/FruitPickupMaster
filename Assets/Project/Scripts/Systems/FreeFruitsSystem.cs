using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class FreeFruitsSystem : IEcsRunSystem  
    {
        private readonly EcsFilterInject<Inc<Component<Fruit>, InGroup, ReleaseFruitRequest>> _releaseFilter = default;
        private readonly EcsFilterInject<Inc<InGroup, Timer>> _conveyorsFilter = default;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var it in _releaseFilter.Value)
            {
                ref var fruit = ref _releaseFilter.Pools.Inc1.Get(it).Value;
                var conveyorIndex = _releaseFilter.Pools.Inc2.Get(it).ConveyorIndex;

                FreeFruit(fruit, conveyorIndex);

                systems.GetWorld().DelEntity(it);
            }
        }

        private void FreeFruit(Fruit fruit, int conveyorIndex)
        {
            foreach (var entity in _conveyorsFilter.Value)
            {
                if(_conveyorsFilter.Pools.Inc1.Get(entity).ConveyorIndex == conveyorIndex)
                {
                    _sceneContext.Value.Groups[conveyorIndex].Conveyor.FreeFruit(fruit);
                    return;
                }
            }
        }
    }
}