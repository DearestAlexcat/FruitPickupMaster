using Leopotam.EcsLite;

public static class FilterEx
{
    public static bool IsEmpty(this EcsFilter filter)
    {
        return filter.GetEntitiesCount() == 0;
    }
}

public static class EcsWorldEx
{
    static EcsWorld BaseWorld;

    public static EcsWorld GetWorld()
    {
        return BaseWorld == null ? BaseWorld = new EcsWorld() : BaseWorld;
    }

    public static ref T GetEntityRef<T>(this EcsWorld world, int entity) where T : struct
    {
        return ref world.GetPool<T>().Get(entity);
    }

    public static void DelEntity<T>(this EcsWorld world, int entity) where T : struct
    {
        world.GetPool<T>().Del(entity);
    }

    public static void AddEntity<T>(this EcsWorld world, int entity) where T : struct
    {
        world.GetPool<T>().Add(entity);
    }

    public static ref T AddEntityRef<T>(this EcsWorld world, int entity) where T : struct
    {
        return ref world.GetPool<T>().Add(entity);
    }

    public static ref T NewEntityRef<T>(this EcsWorld world) where T : struct
    {
        return ref world.GetPool<T>().Add(world.NewEntity());
    }

    public static int NewEntity<T>(this EcsWorld world) where T : struct
    {
        int entity = world.NewEntity();
        world.GetPool<T>().Add(entity);
        return entity;
    }
}