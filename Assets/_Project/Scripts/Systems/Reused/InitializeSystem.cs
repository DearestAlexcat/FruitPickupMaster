using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using DG.Tweening;

namespace Client
{
    class InitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        
        private readonly EcsCustomInject<SaveInJson> _saveInJson = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;

        private readonly EcsFilterInject<Inc<Component<PlayerUnit>, Component<ConveyorElement>>> _playerfilter = default;

        public void Init(EcsSystems systems) 
        {
            _runtimeData.Value.LevelId = Progress.CurrentLevel;
            
            _world.Value.ChangeState(GameState.BEFORE);

            LoadGameData();

            InitializeUnitsEntity();
            InitializeConveyorsEntity();
            InitializeSpawnFruits();
            InitializeCameraOrientation();
            InitializeUI();
            InitializeButtonForward();
            InitializePlayerInput();

            DisableSmoothBlackoutUI();
        }

        private void LoadGameData()
        {
            _saveInJson.Value.Load();
        }

        private void InitializeUnitsEntity()
        {
            for (int i = 0; i < _sceneContext.Value.UnitsEntityInitializeList.Count; i++)
            {
                _sceneContext.Value.UnitsEntityInitializeList[i].InitializeUnitEntity();
                _sceneContext.Value.UnitsEntityInitializeList[i].InitializeAnimationEntity();
            }
        }

        private void InitializeConveyorsEntity()
        {
            var conveyors = _sceneContext.Value.Conveyors;

            for (int i = 0; i < conveyors.Count; i++)
            {
                // conveyor data pack
                _world.Value.AddEntityRef<Component<ConveyorElement>>(conveyors[i].unit.Entity).Value = conveyors[i];

                // conveyor line data pack
                ref var c = ref _world.Value.NewEntityRef<UVScrollingComponent>();
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

        private void InitializePlayingState()
        {
            _world.Value.ChangeState(GameState.PLAYING);
        }

        private void InitializeSpawnFruits()
        {
            _world.Value.NewEntity<SpawnFruitsRequest>();
        }

        private void InitializePlayerInput()
        {
            _world.Value.NewEntity<PlayerInputComponent>();
        }

        private void InitializeCameraOrientation()
        {
            Camera.main.transform.position = _staticData.Value.camStartPosition;
            Camera.main.transform.rotation = _staticData.Value.camStartRotation;
        }

        private void InitializeUI()
        {
            Service<UI>.Get().LevelProgress.InitSliderBorder(0f, GetTargetCollect(), true);
            Service<UI>.Get().ThisUIAnimation.SetStartPosition("SliderProgress");

            Service<UI>.Get().LevelLabel.SetText(_saveInJson.Value.GetData.LevelIndex);
            Service<UI>.Get().ThisUIAnimation.SetEndPosition("LevelLabel");
        }

        private void InitializeButtonForward()
        {
            Service<UI>.Get().WinWindow.buttonForward.onClick.AddListener(() =>
            {
                Service<UI>.Get().WinWindow.SetActiveWindow(false, () =>
                {
                    Service<UI>.Get().blackout.SmoothBlackout(() =>
                    {
                        Levels.LoadCurrentWithSkip();
                    });
                });
            }
            );
        }

        private int GetTargetCollect()
        {
            foreach (var item in _playerfilter.Value)
            {
                return _playerfilter.Pools.Inc1.Get(item).Value.levelTaskData.targetCollect;
            }

            return 0;
        }

        private void DisableSmoothBlackoutUI()
        {
            Service<UI>.Get().blackout.DisableSmoothBlackout(() => EnvironmentInitialize());
        }

        private void EnvironmentInitialize()
        {
            DOTween.Sequence()
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    Service<UI>.Get().ThisUIAnimation.Hide("LevelLabel", () =>
                    {
                        Service<UI>.Get().ThisUIAnimation.Show("SliderProgress");
                        InitializePlayingState();
                    });
                });
        }
    }
}