using Leopotam.EcsLite;

namespace Client
{
    class InitializeSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            systems.GetWorld().ChangeState(GameState.BEFORE);
        }
    }
}