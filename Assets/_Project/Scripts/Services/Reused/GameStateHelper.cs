using Leopotam.EcsLite;

namespace Client
{
    public static class GameStateHelper
    {
        public static void ChangeState(this EcsWorld world, GameState gameState)
        {
            world.NewEntityRef<ChangeStateEvent>().NewGameState = gameState;
        }
    }
}