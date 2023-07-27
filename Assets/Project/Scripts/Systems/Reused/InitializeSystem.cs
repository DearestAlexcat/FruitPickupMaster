using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    class InitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<UI> _ui = default;

        private readonly EcsFilterInject<Inc<Component<PlayerUnit>>> _playerfilter = default;

        public void Init(EcsSystems systems)
        {
            _runtimeData.Value.LevelId = Progress.CurrentLevel;
            _world.Value.ChangeState(GameState.BEFORE);

            UnitsEntityInitialize();
            UIInitialize();
        }

        private void UnitsEntityInitialize()
        {
            for (int i = 0; i < _sceneContext.Value.UnitsEntityInitializeList.Count; i++)
            {
                _sceneContext.Value.UnitsEntityInitializeList[i].InitializeUnitEntity();
                _sceneContext.Value.UnitsEntityInitializeList[i].InitializeAnimationEntity();
            }
        }

        private void UIInitialize()
        {
            //_ui.Value.LevelProgress.InitSliderBorder(0f, GetTargetCollect(), true);
            _ui.Value.ThisUIAnimation.SetStartPosition("SliderProgress");

            _ui.Value.LevelLabel.SetText(_runtimeData.Value.LevelId);
            _ui.Value.ThisUIAnimation.SetStartPosition("LevelLabel");

            _ui.Value.DisableSmoothBlackoutUI();
        }

        private int GetTargetCollect()
        {
            foreach (var item in _playerfilter.Value)
            {
                return _playerfilter.Pools.Inc1.Get(item).Value.LevelTask.targetCollect;
            }

            return 0;
        }
    }
}