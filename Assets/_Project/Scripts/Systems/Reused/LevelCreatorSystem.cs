using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    interface ILevelLink
    {
        static LevelView CurrentLevel { get; set; }
    }

    class LevelCreatorSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<SaveInJson> _saveInJson = default;

        private readonly EcsFilterInject<Inc<LevelLoadRequest>> _levelLoadFilter = default;
        private readonly EcsCustomInject<Services.AnalyticsManager> _gameAnalitics = default;

        public void Run(EcsSystems systems)
        {
            foreach (int entity in _levelLoadFilter.Value)
            {
                _levelLoadFilter.Pools.Inc1.Del(entity);

                SpawnLevel(systems);

                _gameAnalitics.Value.LevelStartGA(ILevelLink.CurrentLevel.Index);
            }
        }

        private void SpawnLevel(EcsSystems systems)
        {
            //   LevelView levelPrefab = _staticData.Value.levelDatas[_saveInJson.Value.GetData.ActualLevel];
            // ILevelLink.CurrentLevel = Object.Instantiate(levelPrefab, levelPrefab.transform.position, levelPrefab.transform.rotation);
        }
    }
}
