using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed public class RopeFinalizeRenderer : IEcsRunSystem 
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        private readonly EcsFilterInject<Inc<Participant, InGroup, RenderRope>> _participantFilter = default;

        public void Run(IEcsSystems systems) 
        {
            foreach (var entity in _participantFilter.Value)
            {
                var gr = GetUnit(entity).GetGrapplingRope;

                if (_world.Value.GetPool<RenderRopeLaunch>().Has(entity))
                {
                    if (gr.IsForwardDrawRopeEnded())
                    {
                        _world.Value.DelEntity<RenderRopeLaunch>(entity);

                        // Visual delay before the rope is returned
                        _world.Value.DelayAction(_staticData.Value.delayRopeReturn, entity, gr, RenderRopeReturn);
                    }
                }
                else if (_world.Value.GetPool<RenderRopeReturn>().Has(entity))
                {
                    if (gr.IsBackwardDrawRopeEnded())
                    {
                        _world.Value.DelEntity<RenderRope>(entity);
                        _world.Value.DelEntity<RenderRopeReturn>(entity);

                        gr.ResetState();

                        CompletionOperation(entity, gr);
                    }
                }
            }
        }

        void CompletionOperation(int entity, GrapplingRope gr)
        {
            if (gr.IsPoolThing)
            {
                AttachToGun(entity, gr);
                // Visual delay before the rope is returned
                _world.Value.DelayAction(_staticData.Value.delayAddToCart, entity, AddToCartRequest);
            }
            else
            {
                SetIdlePosition(entity).Forget();
            }

            async UniTask SetIdlePosition(int entity)
            {
                await GetUnit(entity).riggingManager.SetPose_Idle();
                _world.Value.DelEntity<SelectedFruit>(entity);
            }
        }

        Unit GetUnit(int entity)
        {
            return _sceneContext.Value
                        .Groups[_participantFilter.Pools.Inc2.Get(entity).ConveyorIndex]
                        .Units[_participantFilter.Pools.Inc1.Get(entity).Index];
        }

        void AddToCartRequest(int entity)
        {
            _world.Value.AddEntity<AddToCartRequest>(entity);
        }

        void AttachToGun(int entity, GrapplingRope gr)
        {
            var selectedFruit = _world.Value.GetEntityRef<SelectedFruit>(entity).fruit;
            selectedFruit.transform.parent = gr.gunTip;
            selectedFruit.transform.localPosition = Vector3.zero;
        }
 
        void RenderRopeReturn(int entity, GrapplingRope gr)
        {
            GetUnit(entity).riggingManager.LostAimTarget_Head(); // Stop looking at the fruit
            _world.Value.AddEntity<RenderRopeReturn>(entity);
            gr.RopeReturnInit();
            FreeFruit(entity, gr);
        }

        void FreeFruit(int entity, GrapplingRope gr)
        {
            var task = _world.Value.GetEntityRef<Task>(entity);
            var selectedFruit = _world.Value.GetEntityRef<SelectedFruit>(entity).fruit;

            gr.IsPoolThing = selectedFruit.PoolIndex == task.TargetPoolIndex; // Is it possible to drag fruit?

            if (gr.IsPoolThing)
            {
                selectedFruit.FreeFruitFromPhysics();
                _world.Value.DelEntity<FruitMovement>(selectedFruit.Entity);
            }
        }
    }
}