using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class FruitMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Component<Fruit>, FruitMovement>, Exc<ReleaseFruitRequest>> _fruitsFilter = default;
      
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
     
        float gravitationalAcceleration = Physics.gravity.magnitude;
     
        public void Run(IEcsSystems systems)
        {
            //if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var it in _fruitsFilter.Value)
            {
                ref var fruit = ref _fruitsFilter.Pools.Inc1.Get(it);
                var cfg = _fruitsFilter.Pools.Inc2.Get(it);

                fruit.Value.ThisRigidbody.velocity = (cfg.TargetPosition - fruit.Value.transform.position).normalized * _staticData.Value.fruitSpeed;
                fruit.Value.ThisRigidbody.velocity += Vector3.down * fruit.Value.ThisRigidbody.mass * gravitationalAcceleration * Time.deltaTime;
            }
        }
    }
}