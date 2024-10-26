using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class RenderRopeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RenderRope, Participant, InGroup>> _renderFilter = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _renderFilter.Value)
            {
                if (systems.GetWorld().Has<RenderRopeLaunch>(entity))
                {
                    GetUnit(entity).ForwardRope(Time.deltaTime);
                }
                else if (systems.GetWorld().Has<RenderRopeReturn>(entity))
                {
                    GetUnit(entity).BackwardRope(Time.deltaTime);
                }
            }
        }

        private Unit GetUnit(int entity)
        {
            return _sceneContext.Value
                        .Groups[_renderFilter.Pools.Inc3.Get(entity).ConveyorIndex]
                        .Units[_renderFilter.Pools.Inc2.Get(entity).Index];
        }
    }
}