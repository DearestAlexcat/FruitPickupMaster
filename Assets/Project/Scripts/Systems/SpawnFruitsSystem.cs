using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SpawnFruitsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<InGroup, Timer>> _conveyorFilter = default;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Init(IEcsSystems systems)
        {
            foreach (var item in _conveyorFilter.Value)
            {
                var conveyorIndex = _conveyorFilter.Pools.Inc1.Get(item).ConveyorIndex;
                var conveyor = _sceneContext.Value.Groups[conveyorIndex].Conveyor;

                for (float t = 0; t < 1; t += 0.1f)
                {
                    for (int i = 0; i < conveyor.quantityAtInit; i++)
                    {
                        InitializeFruitComponents(conveyor.GetFruit(t), conveyor, conveyorIndex);
                    }
                }
            }
        }
       
        public void Run(IEcsSystems systems)
        {
            foreach (var item in _conveyorFilter.Value)
            {
                ref var time = ref _conveyorFilter.Pools.Inc2.Get(item).Time;

                var conveyorIndex = _conveyorFilter.Pools.Inc1.Get(item).ConveyorIndex;
                var conveyor = _sceneContext.Value.Groups[conveyorIndex].Conveyor;
                time -= Time.deltaTime / conveyor.spawnDelay;

                if (time <= 0f)
                {
                    time = conveyor.spawnDelay;
                    InitializeFruits(conveyor, conveyorIndex);
                }
            }
        }

        void InitializeFruits(ConveyorView conveyor, int conveyorIndex)
        {
            for (int i = 0; i < conveyor.quantityAtTime; i++)
            {
                InitializeFruitComponents(conveyor.GetFruit(), conveyor, conveyorIndex);
            }
        }

        void InitializeFruitComponents(Fruit fruit, ConveyorView conveyor, int conveyorIndex)
        {
            var entity = _world.Value.NewEntity<Component<Fruit>>();
            fruit.Entity = entity;

            _world.Value.AddEntity<FruitMovement>(entity);
            _world.Value.AddEntityRef<InGroup>(entity).ConveyorIndex = conveyorIndex;

            _world.Value.GetEntityRef<Component<Fruit>>(entity).Value = fruit;
            _world.Value.GetEntityRef<FruitMovement>(entity).Speed = conveyor.Speed;
            _world.Value.GetEntityRef<FruitMovement>(entity).TargetPosition = conveyor.GetFinishPosition();
        }
    }
}
