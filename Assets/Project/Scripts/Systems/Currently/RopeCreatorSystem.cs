using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks;

namespace Client 
{
    sealed class RopeCreatorSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<Component<PlayerUnit>, SelectedFruit, RopeCreateRequest>> _createFilter = default;
        private readonly EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _createFilter.Value)
            {
                CreateRope(it).Forget();

                _world.Value.DelEntity<RopeCreateRequest>(it);
            }
        }

        public async UniTask CreateRope(int entity)
        {
            Fruit selectedFruit = _createFilter.Pools.Inc2.Get(entity).fruit;
            PlayerUnit unit = _createFilter.Pools.Inc1.Get(entity).Value;

            unit.riggingManager.SetTarget(selectedFruit.transform).Forget();

            await unit.riggingManager.SetIKWeightForLeftIdle(1f);

            unit.riggingManager.DropTarget();

            unit.PlayPistolFireFx();

            InitializeRopeEntity(unit, selectedFruit);

            selectedFruit.EnableIsKinematic(false);        
        }

        private void InitializeRopeEntity(PlayerUnit unit, Fruit selectedFruit)
        {
            _world.Value.AddEntityRef<Component<GrapplingRope>>(unit.Entity).Value = unit.gr;

            unit.gr.SetForward(selectedFruit.transform, selectedFruit.ThisRigidbody);

            _world.Value.AddEntity<RenderGrapplingForward>(unit.Entity); 
            _world.Value.AddEntity<CheckGrapplingForward>(unit.Entity);
        }
    }
}