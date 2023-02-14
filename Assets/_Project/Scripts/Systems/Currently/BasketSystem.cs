using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Client
{
    sealed class BasketSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        private readonly EcsFilterInject<Inc<AddToCartRequest>> _addFilter = default;
        private readonly EcsFilterInject<Inc<HookFruitRequest>> _hookFruitFilter = default;
        private readonly EcsFilterInject<Inc<Component<GrapplingRope>>> _ropeFilter = default;
        private readonly EcsFilterInject<Inc<Component<ConveyorElement>>> _conveyorFilter = default;
        private readonly EcsFilterInject<Inc<SpawnFruitsRequest>> _spawnFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _addFilter.Value)
            {
                AddToBasket(_addFilter.Pools.Inc1.Get(it).unit).Forget();

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

        private async UniTask AttachToGun(Unit u)
        {
            Fruit hookFruit = GetHookFruit();

            hookFruit.transform.parent = u.gr.gunTip;
            hookFruit.transform.localPosition = Vector3.zero;

            await UniTask.NextFrame();
        }

        private void StartPlayerInput()
        {
            _world.Value.NewEntity<PlayerInputRequest>();
        }

        private void RemoveFruitHook()
        {
            foreach (var it in _hookFruitFilter.Value)
            {
                _hookFruitFilter.Pools.Inc1.Del(it);
            }
        }

        private void RemoveRope(int entity)
        {
            var gr = _ropeFilter.Pools.Inc1.Get(entity).Value;
            gr.currentGrapplePosition = gr.gunTip.position;
            gr.Spring.Reset();
            if (gr.lr.positionCount > 0)
                gr.lr.positionCount = 0;
             _ropeFilter.Pools.Inc1.Del(entity);
        }

        public async UniTask AddToBasket(Unit u)
        {
            Fruit hookFruit = GetHookFruit();

            if (!u.levelTaskData.Check—orrectness—hoice(hookFruit.PoolIndex))
            {
                RemoveRope(u.Entity);

                hookFruit.DestroyConfigurableJoint();
                hookFruit.SetMeshColliderEnabled(false);
                hookFruit.Hide(2f);

                u.riggingManager.SetSinglyIKWeightForLeftIdle(0f);

                await u.riggingManager.SetIKWeightForRightPut(0f);

                RemoveFruitHook();
                StartPlayerInput();

                return;
            }

            await hookFruit.FreeFruitFromPhysics();

            await AttachToGun(u);

            RemoveRope(u.Entity);

            u.riggingManager.SetSinglyIKWeightForLeftIdle(0f);

            await u.riggingManager.SetIKWeightForRightPut(1f);

            await u.basket.AddTo—art(hookFruit);

            u.PlayAddToCartFX();

            ShowPlusOnePopup(u);

            await u.riggingManager.SetIKWeightForRightPut(0f);

            RemoveFruitHook();

            if(!CheckLevelComplete(u))
            {
                StartPlayerInput();
            }
        }

        private void HideObjectsFromFinalScene()
        {
            _sceneContext.Value.ThisUIAnimation.Hide("SliderProgress");

            foreach (var item in _conveyorFilter.Value)
            {
                _conveyorFilter.Pools.Inc1.Get(item).Value.conveyorObj.SetActive(false);
            }
        }

        private void StopSpawnFruit()
        {
            foreach (var item in _spawnFilter.Value)
            {
                _world.Value.DelEntity(item);
            }
        }

        private bool CheckLevelComplete(Unit u)
        {
            bool value = u.levelTaskData.CheckLevelComplete();

            if (value)
            {
                // simple delay
                DOTween.Sequence()
                    .AppendInterval(_staticData.Value.pauseBeforeEnd)
                    .AppendCallback(() =>
                    {
                        StopSpawnFruit();
                        HideObjectsFromFinalScene();
                        PlaySillyDancingAnimation(u);

                        _world.Value.NewEntity<LevelCompleteRequest>();
                        _world.Value.NewEntityRef<CameraZoomRequest>();

                        // _world.Value.NewEntityRef<CameraZoomRequest>().EndZoomCallback = () => _world.Value.NewEntity<LevelCompleteRequest>();
                    });
            }

            return value;
        }

        private void PlaySillyDancingAnimation(Unit u)
        {
             u.PlayAnimation(AnimationState.SILLYDANCING, AnimationFlags.SILLYDANCING);
        }

        private void CreatePopUpText(Unit u)
        {
            ref var c = ref _world.Value.NewEntityRef<PopUpRequest>();
            c.SpawnPosition = u.PopupPointer.position;
            c.SpawnRotation = Quaternion.identity;
            c.Parent = u.transform;
            c.TextUP = "+1";
        }

        private void IncrementCollect(Unit u)
        {
            u.levelTaskData.IncrementCollect();
        }

        private void ShowPlusOnePopup(Unit u)
        {
            if (u.levelTaskData.Check—orrectness—hoice(GetHookFruit().PoolIndex))
            {
                CreatePopUpText(u);
                IncrementCollect(u);
            }
        }
    }
}