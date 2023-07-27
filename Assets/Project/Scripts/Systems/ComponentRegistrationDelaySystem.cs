using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class ExecutionDelaySystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<ExecutionDelay>> _delaysFilter = default;
        private readonly EcsWorldInject _world = default;

        public void Run (EcsSystems systems) 
        {
            foreach (var item in _delaysFilter.Value)
            {
                ref var data = ref _delaysFilter.Pools.Inc1.Get(item);

                data.time -= Time.deltaTime;

                if (data.time < 0f)
                {
                    data.action?.Invoke();
                    _world.Value.DelEntity<ExecutionDelay>(item);
                }
            }
        }
    }
}