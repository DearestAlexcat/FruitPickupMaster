using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class FreeFruitsSystem : IEcsRunSystem  
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<Component<Fruit>, InGroup, FreeFruitsRequest>> _freeFilter = default;
        private readonly EcsFilterInject<Inc<Component<ConveyorView>, InGroup>> _conveyorsFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _freeFilter.Value)
            {
                ref var fruit = ref _freeFilter.Pools.Inc1.Get(it).Value;
                var groupIndex = _freeFilter.Pools.Inc2.Get(it).GroupIndex;

                _world.Value.AddEntity<Destroyed>(it);

                fruit.ThisRigidbody.DOKill();

                FreeFruit(fruit, groupIndex);
            }
        }

        private void FreeFruit(Fruit fruit, int groupIndex)
        {
            foreach (var it in _conveyorsFilter.Value)
            {
                if(groupIndex == _conveyorsFilter.Pools.Inc2.Get(it).GroupIndex)
                {
                    _conveyorsFilter.Pools.Inc1.Get(it).Value.FreeFruit(fruit);
                    return;
                }
            }
        }
    }
}