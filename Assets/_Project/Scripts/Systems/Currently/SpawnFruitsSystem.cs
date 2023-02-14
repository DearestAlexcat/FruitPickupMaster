using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SpawnFruitsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnFruitsRequest>> _spawnFilter = default;
        private readonly EcsFilterInject<Inc<Component<ConveyorElement>>> _conveyorFilter = default;

        public void Run(EcsSystems systems)
        {
            if (_spawnFilter.Value.IsEmpty()) return;

            foreach (var item in _conveyorFilter.Value)
            {
                ref var c = ref _conveyorFilter.Pools.Inc1.Get(item);

                c.Value.time -= Time.deltaTime;

                if (c.Value.time < 0f)
                {
                    c.Value.time = c.Value.spawnDelay;

                    Fruit fruit = c.Value.GetFruit();

                    int entity = systems.GetWorld().NewEntity<Component<Fruit>>();

                    fruit.Entity = entity;
                    fruit.conveyor = c.Value;

                    fruit.fruitObj.transform.rotation = Random.rotation;

                    systems.GetWorld().GetEntityRef<Component<Fruit>>(entity).Value = fruit;
                }
            }
        }
    }
}
