using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class DestroyEntitySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsCustomInject<SaveInJson> _saveInJson = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        private readonly EcsFilterInject<Inc<FinalizeRequestComponent>> _destroyFilter = default;
        private readonly EcsFilterInject<Inc<UVScrollingComponent>> _uvScrollFilter = default;
        private readonly EcsFilterInject<Inc<Component<Fruit>>> _fruitsFilter = default;

        private readonly EcsFilterInject<Inc<Component<ConveyorElement>>> _conveyorFilter = default;

        private readonly EcsFilterInject<Inc<Component<PlayerUnit>>> _playerFilter = default;
        private readonly EcsFilterInject<Inc<Component<StandUnit>>> _standUnitFilter = default;


        public void Run(EcsSystems systems)
        {
            foreach (var item in _destroyFilter.Value)
            {
                foreach (var entity in _standUnitFilter.Value)
                {
                    _world.Value.DelEntity(entity);
                }

                foreach (var entity in _uvScrollFilter.Value)
                {
                    _world.Value.DelEntity(entity);
                }

                foreach (var entity in _fruitsFilter.Value)
                {
                    _world.Value.DelEntity(entity);
                }

                foreach (var entity in _conveyorFilter.Value)
                {
                    _world.Value.DelEntity(entity);
                }

                foreach (var entity in _playerFilter.Value)
                {
                    _world.Value.DelEntity(entity);
                }

                switch (_destroyFilter.Pools.Inc1.Get(item).Value)
                {
                    case LevelEndState.WIN:
                        NextLevel();
                        break;
                    case LevelEndState.LOSE:
                        ResetLevel();
                        break;
                }

                _world.Value.DelEntity(item);
            }
        }

        private void ResetLevel()
        {
            //ILevelLink.CurrentLevel.DeathPerLevel++;
            //_saveInJson.Value.Save();
            //_world.Value.NewEntity<LevelLoadRequest>();
        }

        private void NextLevel()
        {
            //ILevelLink.CurrentLevel.DeathPerLevel = 0;
            //_saveInJson.Value.GetData.LevelIndex++;
            //_saveInJson.Value.GetData.SortLevels(_staticData.Value);
            //_saveInJson.Value.Save();
            //_world.Value.NewEntity<LevelLoadRequest>();
        }
    }
}