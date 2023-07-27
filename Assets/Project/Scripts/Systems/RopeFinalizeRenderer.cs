using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using static UnityEngine.EventSystems.EventTrigger;

namespace Client 
{
    sealed class RopeFinalizeRenderer : IEcsRunSystem 
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<Component<GrapplingRope>, SelectedFruit>> _ropePointsFilter = default;

        public void Run(EcsSystems systems) 
        {
            foreach (var entity in _ropePointsFilter.Value)
            {
                GrapplingRope gr = _ropePointsFilter.Pools.Inc1.Get(entity).Value;

                if (_world.Value.GetPool<CheckGrapplingForward>().Has(entity))
                {
                    FinalizeForwardRender(entity, gr);
                }

                if (_world.Value.GetPool<CheckGrapplingBackward>().Has(entity))
                {
                    FinalizeBackwardRender(entity, gr);
                }
            }
        }

        public void FinalizeForwardRender(int entity, GrapplingRope gr)
        {
            if (gr.IsForwardDrawRopeEnded())
            {
                _world.Value.DelayAction(0.15f, () => DoFinalizeForwardRender(entity, gr));
                _world.Value.DelEntity<CheckGrapplingForward>(entity);
            }
        }

        public void FinalizeBackwardRender(int entity, GrapplingRope gr)
        {
            if (gr.IsBackwardDrawRopeEnded())
            {
                _world.Value.DelayAction(0.15f, () => DoFinalizeBackwardRender(entity, gr));
                _world.Value.DelEntity<CheckGrapplingBackward>(entity);
            }
        }

        public void DoFinalizeForwardRender(int entity, GrapplingRope gr)
        {
            _world.Value.DelEntity<RenderGrapplingForward>(entity);

            _world.Value.AddEntity<RenderGrapplingBackward>(entity);
            _world.Value.AddEntity<CheckGrapplingBackward>(entity);
            gr.SetBackward();

            FreeFruit(entity, gr);
        }

        public void DoFinalizeBackwardRender(int entity, GrapplingRope gr)
        {
            var selectedFruit = _ropePointsFilter.Pools.Inc2.Get(entity).fruit;
            _world.Value.GetEntityRef<Component<Fruit>>(selectedFruit.Entity).Value.ThisRigidbody.DOKill();

            _world.Value.DelEntity<RenderGrapplingBackward>(entity);
            _world.Value.AddEntity<AddToCartRequest>(entity);

            RemoveRope(entity, gr);
        }

        private void RemoveRope(int entity, GrapplingRope gr)
        {
            gr.Spring.Reset();
            gr.lr.positionCount = 0;
            _world.Value.DelEntity<Component<GrapplingRope>>(entity);
        }

        private void FreeFruit(int entity, GrapplingRope gr)
        {
            var unit = _world.Value.GetPool<Component<PlayerUnit>>().Get(entity).Value;
            var selectedFruit = _ropePointsFilter.Pools.Inc2.Get(entity).fruit;
            bool result = unit.LevelTask.Check—orrectness—hoice(selectedFruit.PoolIndex);
          
            gr.IsPoolThing = result;
          
            if (result)
            {
                _world.Value.DelEntity<FruitMovementSettings>(selectedFruit.Entity);
                _world.Value.GetEntityRef<Component<Fruit>>(selectedFruit.Entity).Value.ThisRigidbody.DOKill();
            }       
        }
    }
}