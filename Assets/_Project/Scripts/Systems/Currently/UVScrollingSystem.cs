using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class UVScrollingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UVScrollingComponent>> _uvScrollFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _uvScrollFilter.Value)
            {
                ref var c = ref _uvScrollFilter.Pools.Inc1.Get(it);
                
                c.CurrentOffset.x += Time.deltaTime * c.Speed.x;
                c.CurrentOffset.y += Time.deltaTime * c.Speed.y;

                c.ScrollingObject.sharedMaterial.SetTextureOffset("_MainTex", c.CurrentOffset);
            }
        }
    }
}
