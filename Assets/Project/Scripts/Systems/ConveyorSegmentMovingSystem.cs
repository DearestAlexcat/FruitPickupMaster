using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class ConveyorSegmentTranslateSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        //private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsFilterInject<Inc<MoveSegmentReguest>> _moveSeqmentFilter = default;

        public void Run(EcsSystems systems)
        {
            //if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var item in _moveSeqmentFilter.Value)
            {
                var entity = _moveSeqmentFilter.Pools.Inc1.Get(item).ConveyorEntity;
                _world.Value.GetEntityRef<Component<ConveyorView>>(entity).Value.TranslateSegment();
            }
        }
    }
}