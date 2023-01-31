using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class FreeFruitsSystem : IEcsRunSystem  
    {
        private readonly EcsFilterInject<Inc<FreeFruitsRequest>> _freeFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _freeFilter.Value)
            {
                ref var f = ref _freeFilter.Pools.Inc1.Get(it).FreeFruit;

                systems.GetWorld().DelEntity<Component<Fruit>>(f.Entity);

                f.conveyor.FreeFruit(f);

                systems.GetWorld().DelEntity(it);
            }
        }
    }
}