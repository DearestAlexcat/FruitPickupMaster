using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using DG.Tweening;
using UnityEngine;

namespace Client 
{
    sealed class LevelInitializeSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<SaveInJson> _saveInJson = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<Config> _config = default;

        private readonly EcsFilterInject<Inc<LevelInitializeRequest>> _initializefilter = default;
        private readonly EcsFilterInject<Inc<Component<PlayerUnit>, Component<ConveyorElement>>> _playerfilter = default;

        public void Run(EcsSystems systems) 
        {
            foreach (var item in _initializefilter.Value)
            {
                InitializeConveyorsEnity(systems);
                InitializeSpawnFruits(systems);
                InitializeCameraOrientation();
                InitializeUI();
                
                DisableSmoothBlackoutUI(systems);
                
                systems.GetWorld().DelEntity(item);
            }
        }

        private void InitializeConveyorsEnity(EcsSystems systems)
        {
            var conveyors = ILevelLink.CurrentLevel.Conveyors;

            for (int i = 0; i < conveyors.Count; i++)
            {
                // conveyor data pack
                systems.GetWorld().AddEntityRef<Component<ConveyorElement>>(conveyors[i].unit.Entity).Value = conveyors[i];

                // conveyor line data pack
                ref var c = ref systems.GetWorld().NewEntityRef<UVScrollingComponent>();
                c.CurrentOffset = new Vector2();
                c.Speed = new Vector2(conveyors[i].Speed, 0);
                c.ScrollingObject = conveyors[i].ScrollingConveyorLine;

                // pool
                conveyors[i].InitializeFruits();

                // level task
                InitializeUnitsTask(conveyors[i]);
            }
        }
        
        private void InitializeUnitsTask(ConveyorElement conveyor)
        {
            conveyor.unit.InitializeTask(conveyor);
        }

        private void InitializePlayerInput(EcsSystems systems)
        {
            systems.GetWorld().NewEntity<PlayerInputRequest>();
        }

        private void InitializeSpawnFruits(EcsSystems systems)
        {
            systems.GetWorld().NewEntity<SpawnFruitsRequest>();
        }


        private void InitializeCameraOrientation()
        {
            _sceneContext.Value.Camera.transform.position = _config.Value.camStartPosition;
            _sceneContext.Value.Camera.transform.rotation = _config.Value.camStartRotation;
        }

        private void InitializeUI()
        {
            _sceneContext.Value.LevelProgress.InitSliderBorder(0f, GetTargetCollect(), true);
            _sceneContext.Value.ThisUIAnimation.SetStartPosition("SliderProgress");

            _sceneContext.Value.LevelLabel.SetText(_saveInJson.Value.GetData.LevelIndex);
            _sceneContext.Value.ThisUIAnimation.SetEndPosition("LevelLabel");
        }

        private int GetTargetCollect()
        {
            foreach (var item in _playerfilter.Value)
            {
                return _playerfilter.Pools.Inc1.Get(item).Value.levelTaskData.targetCollect;
            }

            return 0;
        }


        private void DisableSmoothBlackoutUI(EcsSystems systems)
        {
            _sceneContext.Value.blackout.DisableSmoothBlackout(() => EnvironmentInitialize(systems));
        }

        private void EnvironmentInitialize(EcsSystems systems)
        {
            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    EcsStartup.Instance.SceneContext.ThisUIAnimation.Hide("LevelLabel", () =>
                    {
                        _sceneContext.Value.ThisUIAnimation.Show("SliderProgress");
                        InitializePlayerInput(systems);
                    });
                });
        }
    }
}