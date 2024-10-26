using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class ExecutionDelaySystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<ExecutionDelay>> _delaysFilter = default;
        private readonly EcsFilterInject<Inc<ExecutionDelayCustomT2>> _delaysT2Filter = default;
        private readonly EcsFilterInject<Inc<ExecutionDelayCustomT1>> _delaysT1Filter = default;

        public void Run (IEcsSystems systems) 
        {
            foreach (var item in _delaysFilter.Value)
            {
                ref var data = ref _delaysFilter.Pools.Inc1.Get(item);
                data.time -= Time.deltaTime;

                if (data.time < 0f)
                {
                    data.action?.Invoke();
                    systems.GetWorld().DelEntity<ExecutionDelay>(item);
                }
            }

            foreach (var item in _delaysT2Filter.Value)
            {
                ref var data = ref _delaysT2Filter.Pools.Inc1.Get(item);
                data.time -= Time.deltaTime;

                if (data.time < 0f)
                {
                    data.action?.Invoke(data.entity, data.gr);
                    systems.GetWorld().DelEntity<ExecutionDelayCustomT2>(item);
                }
            }

            foreach (var item in _delaysT1Filter.Value)
            {
                ref var data = ref _delaysT1Filter.Pools.Inc1.Get(item);
                data.time -= Time.deltaTime;

                if (data.time < 0f)
                {
                    data.action?.Invoke(data.entity);
                    systems.GetWorld().DelEntity<ExecutionDelayCustomT1>(item);
                }
            }
        }
    }
}