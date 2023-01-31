
public static class LevelLauncher
{
    public static void ResetLevel()
    {
        EcsWorldEx.GetWorld().GetPool<FinalizeRequestComponent>().Add(EcsWorldEx.GetWorld().NewEntity()).Value = LevelEndState.LOSE;
    }

    public static void GoToNextLevel()
    {
        EcsWorldEx.GetWorld().GetPool<FinalizeRequestComponent>().Add(EcsWorldEx.GetWorld().NewEntity()).Value = LevelEndState.NEXT;
    }

    public static void GoToPreviousLevel()
    {
        EcsWorldEx.GetWorld().GetPool<FinalizeRequestComponent>().Add(EcsWorldEx.GetWorld().NewEntity()).Value = LevelEndState.PREV;
    }
}
