using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks; 
using UnityEngine;

namespace Client
{
    sealed class BasketSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<Component<PlayerUnit>, SelectedFruit, AddToCartRequest>> _addFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _addFilter.Value)
            {
                AddToBasket(it).Forget();
                _world.Value.DelEntity<AddToCartRequest>(it);
            }
        }

        public async UniTask AddToBasket(int playerEntity)
        {
            Fruit selectedFruit = _addFilter.Pools.Inc2.Get(playerEntity).fruit;
            PlayerUnit unit = _addFilter.Pools.Inc1.Get(playerEntity).Value;

            if (!unit.LevelTask.Check—orrectness—hoice(selectedFruit.PoolIndex))
            {
                unit.riggingManager.SetIKWeightForLeftIdle(0f).Forget();

                await unit.riggingManager.SetIKWeightForRightPut(0f);

                _world.Value.DelEntity<SelectedFruit>(playerEntity);

                StartInput(playerEntity);

                return;
            }

            await selectedFruit.FreeFruitFromPhysics();

            await AttachToGun(unit, selectedFruit);

            unit.riggingManager.SetSinglyIKWeightForLeftIdle(0f);

            await unit.riggingManager.SetIKWeightForRightPut(1f);

            await unit.basket.AddTo—art(selectedFruit);

            unit.PlayAddToCartFX();

            ShowPlusOnePopup(unit, selectedFruit);

            await unit.riggingManager.SetIKWeightForRightPut(0f);

            _world.Value.DelEntity<SelectedFruit>(playerEntity);

            _world.Value.DelEntity(selectedFruit.Entity);

            if (!CheckLevelComplete(unit))
            {
                StartInput(playerEntity);
            }
        }

        private async UniTask AttachToGun(PlayerUnit u, Fruit selectedFruit)
        {
            selectedFruit.transform.parent = u.gr.gunTip;
            selectedFruit.transform.localPosition = Vector3.zero;
            await UniTask.NextFrame();
        }

        private void StartInput(int entity)
        {
            _world.Value.DelEntity<StopInput>(entity);
        }

        private bool CheckLevelComplete(PlayerUnit unit)
        {
            bool value = unit.LevelTask.CheckTaskComplete();

            if (value)
            {
                _world.Value.AddEntity<UnitWin>(unit.Entity);
                _world.Value.NewEntity<LevelCompleteRequest>();
            }

            return value;
        }

        private void CreatePopUpText(PlayerUnit unit)
        {
            ref var c = ref _world.Value.NewEntityRef<PopUpRequest>();
            c.SpawnPosition = unit.PopupPointer.position;
            c.SpawnRotation = Quaternion.identity;
            c.Parent = unit.transform;
            c.TextUP = "-1";
        }
 

        private void ShowPlusOnePopup(PlayerUnit unit, Fruit selectedFruit)
        {
            if (unit.LevelTask.Check—orrectness—hoice(selectedFruit.PoolIndex))
            {
                CreatePopUpText(unit);
                unit.DecreaseCollect();
            }
        }
    }
}