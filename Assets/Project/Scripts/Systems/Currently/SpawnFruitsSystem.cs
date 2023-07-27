using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Client
{
    sealed class SpawnFruitsSystem : IEcsRunSystem
    {
        //private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsFilterInject<Inc<Component<ConveyorView>, InGroup>> _conveyorFilter = default;

        public void Run(EcsSystems systems)
        {
            //if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var item in _conveyorFilter.Value)
            {
                ref var conveyor = ref _conveyorFilter.Pools.Inc1.Get(item).Value;

                conveyor.time -= Time.deltaTime;

                if (conveyor.time < 0f)
                {
                    conveyor.time = conveyor.spawnDelay;

                    var conveyorGroupIndex = _conveyorFilter.Pools.Inc2.Get(item).GroupIndex;

                    InitializeFruits(conveyor, conveyorGroupIndex, systems.GetWorld()).Forget();
                }
            }
        }

        public async UniTask InitializeFruits(ConveyorView conveyor, int groupIndex, EcsWorld world)
        {
            for (int i = 0; i < conveyor.quantityAtTime; i++)
            {
                var fruit = await conveyor.GetFruit();

                var entity = world.NewEntity<Component<Fruit>>();
                fruit.Entity = entity;

                world.AddEntity<FruitMovementSettings>(entity);
                world.AddEntityRef<InGroup>(entity).GroupIndex = groupIndex;
                world.AddEntity<New>(entity);

                world.GetEntityRef<Component<Fruit>>(entity).Value = fruit;
                world.GetEntityRef<FruitMovementSettings>(entity).Speed = conveyor.Speed;
                world.GetEntityRef<FruitMovementSettings>(entity).TargetPosition = conveyor.GetActualFinishPosition();
            }
        }

    }
}
