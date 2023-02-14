using Leopotam.EcsLite;

namespace Client
{
    public static class LevelLauncher
    {
        public static void ResetLevel()
        {
            Service<EcsWorld>.Get().GetPool<FinalizeRequestComponent>().Add(Service<EcsWorld>.Get().NewEntity()).Value = LevelEndState.LOSE;
        }

        public static void GoToNextLevel()
        {
            Service<EcsWorld>.Get().GetPool<FinalizeRequestComponent>().Add(Service<EcsWorld>.Get().NewEntity()).Value = LevelEndState.NEXT;
        }

        public static void GoToPreviousLevel()
        {
            Service<EcsWorld>.Get().GetPool<FinalizeRequestComponent>().Add(Service<EcsWorld>.Get().NewEntity()).Value = LevelEndState.PREV;
        }
    }
}


