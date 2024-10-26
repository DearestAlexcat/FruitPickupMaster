using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks;
using static UnityEngine.Rendering.DebugUI;

namespace Client 
{
    sealed class RopeCreatorSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        private readonly EcsFilterInject<Inc<SelectedFruit, RopeCreateRequest>> _createFilter = default;
        private readonly EcsFilterInject<Inc<Participant, InGroup>> _participantFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _createFilter.Value)
            {
                CreateRopeRenderRequest(systems.GetWorld(), entity).Forget();
            }
        }

        public async UniTask CreateRopeRenderRequest(EcsWorld world, int entity)
        {
            Fruit selectedFruit = _createFilter.Pools.Inc1.Get(entity).fruit;
            var unit = GetUnit(entity);

            unit.riggingManager.SetAimTarget_Head(selectedFruit.transform); // The head follows the fruit

            await unit.riggingManager.SetPose_Aiming(); // We take the aiming pose. We extend the left hand with the pistol

            unit.PlayPistolFireFx();

            unit.GetGrapplingRope.RopeLaunchInit(selectedFruit.transform);

            world.AddEntity<RenderRope>(entity);
            world.AddEntity<RenderRopeLaunch>(entity);
        }

        private Unit GetUnit(int entity)
        {
            return _sceneContext.Value
                        .Groups[_participantFilter.Pools.Inc2.Get(entity).ConveyorIndex]
                        .Units[_participantFilter.Pools.Inc1.Get(entity).Index];
        }
    }}