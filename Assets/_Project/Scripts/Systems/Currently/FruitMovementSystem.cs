using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class FruitMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Component<Fruit>>> _fruitsFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _fruitsFilter.Value)
            {
                ref var f = ref _fruitsFilter.Pools.Inc1.Get(it);

                f.Value.transform.Translate(f.Value.conveyor.conveyorLine.right * Time.deltaTime * f.Value.conveyor.FruitSpeed);
            }
        }
    }
}