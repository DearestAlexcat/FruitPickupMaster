using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

class GameInitSystem : IEcsInitSystem
{
    private readonly EcsCustomInject<SaveInJson> _saveInJson = default;

    public void Init(EcsSystems systems)
    {
        LoadGameData();

        systems.GetWorld().NewEntity<LevelLoadRequest>();
    }

    private void LoadGameData()
    {
        _saveInJson.Value.Load();
    }
}
