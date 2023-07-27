using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class RopeRenderSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<Component<GrapplingRope>>> _ropePointsFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var item in _ropePointsFilter.Value)
            {
                GrapplingRope gr = _ropePointsFilter.Pools.Inc1.Get(item).Value;

                if(_world.Value.GetPool<RenderGrapplingForward>().Has(item))
                {
                    gr.ForwardRope(Time.deltaTime);
                }

                if(_world.Value.GetPool<RenderGrapplingBackward>().Has(item))
                {
                    gr.BackwardRope(Time.deltaTime);
                    gr.PullGrapplingThing();
                }
            }
        } 
    }
}