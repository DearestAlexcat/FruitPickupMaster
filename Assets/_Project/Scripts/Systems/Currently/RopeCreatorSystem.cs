using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Client 
{
    sealed class RopeCreatorSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<RopeCreateRequest>> _createFilter = default;
        private readonly EcsFilterInject<Inc<HookFruitRequest>> _hookFruitFilter = default;
        private readonly EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _createFilter.Value)
            {
                var c = _createFilter.Pools.Inc1.Get(it);
                
                CreateRope(c.unit).Forget();

                systems.GetWorld().DelEntity(it);
            }
        }

        Fruit GetHookFruit()
        {
            foreach (var it in _hookFruitFilter.Value)
            {
                return _hookFruitFilter.Pools.Inc1.Get(it).fruit;
            }

            return null;
        }

        public async UniTask CreateRope(Unit unit)
        {
            unit.riggingManager.SetTarget(GetHookFruit().transform).Forget();

            await unit.riggingManager.SetIKWeightForLeftIdle(1f);

            unit.riggingManager.DropTarget();

            unit.PlayPistolFireFx();

            InitializeRopeEntity(unit);

            GetHookFruit().EnableIsKinematic(false);
            GetHookFruit().SetConnectedBody(unit.connectedBody);
            UntieFruitFromConveyor();
        }

        private void UntieFruitFromConveyor()
        {
            _world.Value.DelEntity<Component<Fruit>>(GetHookFruit().Entity);
        }

        private void InitializeRopeEntity(Unit unit)
        {
            ref var component = ref _world.Value.AddEntityRef<Component<GrapplingRope>>(unit.Entity);
            component.Value = unit.gr;

            unit.gr.grapplePoint = GetHookFruit().transform;
            unit.gr.unit = unit;
        }

        //private void InitializeRopeEntity(Unit unit)
        //{
        //    ref var component = ref _world.Value.AddEntityRef<RopePointsComponent>(unit.Entity);    
        //    component.ThisLineRenderer = unit.ropeRenderer;
        //    component.Points = new List<Transform>(2);

        //    // init
        //    component.ThisLineRenderer.positionCount = 2;
        //    component.Points.Add(unit.sourceRope);
        //    component.Points.Add(GetHookFruit().transform);
        //}
    }
}