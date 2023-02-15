using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    class LevelCreatorSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<Services.AnalyticsManager> _gameAnalitics = default;

        public void Run(EcsSystems systems)
        {
           
        }

        private void SpawnLevel(EcsSystems systems)
        {
            //   LevelView levelPrefab = _staticData.Value.levelDatas[_saveInJson.Value.GetData.ActualLevel];
            // ILevelLink.CurrentLevel = Object.Instantiate(levelPrefab, levelPrefab.transform.position, levelPrefab.transform.rotation);
        }
    }
}
