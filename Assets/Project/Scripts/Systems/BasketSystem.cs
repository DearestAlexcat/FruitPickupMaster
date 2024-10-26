using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks; 
using UnityEngine;

namespace Client
{
    sealed class BasketSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsFilterInject<Inc<AddToCartRequest, Task, SelectedFruit>> _taskFilter = default;
        private readonly EcsFilterInject<Inc<Participant, InGroup>> _participantFilter = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _taskFilter.Value)
            {
                AddToBasket(entity).Forget();
                _world.Value.DelEntity<AddToCartRequest>(entity);
            }
        }

        public async UniTask AddToBasket(int entity)
        {
            var selectedFruit = _taskFilter.Pools.Inc3.Get(entity).fruit;
            var task = _taskFilter.Pools.Inc2.Get(entity);
            var unit = GetUnit(entity);

            await unit.riggingManager.SetPose_Laydown();
            await unit.basket.AddTo—art(selectedFruit);

            unit.PlayAddToCartFX();
            ShowPopup(entity, unit);

            await unit.riggingManager.SetPose_Idle();

            _world.Value.DelEntity<SelectedFruit>(entity);
            _world.Value.DelEntity<InBotResponseZone>(entity);
            _world.Value.DelEntity(selectedFruit.Entity);

            if (IsTaskComplete(entity))
            {
                _world.Value.AddEntity<TaskCompleted>(entity);
            }
        }

        Unit GetUnit(int entity)
        {
            return _sceneContext.Value
                        .Groups[_participantFilter.Pools.Inc2.Get(entity).ConveyorIndex]
                        .Units[_participantFilter.Pools.Inc1.Get(entity).Index];
        }

        bool IsTaskComplete(int entity)
        {
            return _taskFilter.Pools.Inc2.Get(entity).TargetCollect == 0;
        }

        private void ShowPopup(int entity, Unit unit)
        {
            ref var task = ref _taskFilter.Pools.Inc2.Get(entity);

            CreatePopUpText(unit);
            task.TargetCollect--;
            unit.SetTaskText(task.TargetCollect);
        }

        void CreatePopUpText(Unit unit)
        {
            ref var c = ref _world.Value.NewEntityRef<PopUpRequest>();
            c.SpawnPosition = unit.PopupPointer.position;
            c.SpawnRotation = Quaternion.identity;
            c.Parent = unit.transform;
            c.TextUP = "-1";
        }
    }
}