using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class ConveyorsMovementSystem : IEcsRunSystem
    {
        //private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsFilterInject<Inc<Component<ConveyorView>>> _conveyorsFilter = default;

        public void Run(EcsSystems systems)
        {
            //if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var conveyorEntity in _conveyorsFilter.Value)
            {
                _conveyorsFilter.Pools.Inc1.Get(conveyorEntity).Value.MoveSegments();
            }
        }
    }
}
