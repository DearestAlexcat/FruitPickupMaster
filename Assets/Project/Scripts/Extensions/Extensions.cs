using Client;
using Leopotam.EcsLite;

public static class FilterExtension
{
    public static bool IsEmpty(this EcsFilter filter)
    {
        return filter.GetEntitiesCount() == 0;
    }
}

public static class EcsWorldExtension
{
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

    public static void DelayAction(this EcsWorld world, float time, System.Action action)
    { 
        ref var delayPool = ref world.GetPool<ExecutionDelay>().Add(world.NewEntity());
        delayPool.time = time;
        delayPool.action = action;
    }

    public static void DelayAction(this EcsWorld world, float time, int entity, System.Action<int> action)
    {
        ref var delayPool = ref world.GetPool<ExecutionDelayCustomT1>().Add(world.NewEntity());
        delayPool.entity = entity;
        delayPool.time = time;
        delayPool.action = action;
    }

    public static void DelayAction(this EcsWorld world, float time, int entity, GrapplingRope gr, System.Action<int, GrapplingRope> action)
    {
        ref var delayPool = ref world.GetPool<ExecutionDelayCustomT2>().Add(world.NewEntity());
        delayPool.entity = entity;
        delayPool.gr = gr;
        delayPool.time = time;
        delayPool.action = action;
    }

    public static void DelayAddEntity<T>(this EcsWorld world, int entity, float time) where T : struct
    {
        ref var delayPool = ref world.GetPool<ExecutionDelayCustom>().Add(world.NewEntity());
        delayPool.time = time;
        delayPool.action = static (world, entity) => world.GetPool<T>().Add(entity);
    }

    public static void DelayDelEntity<T>(this EcsWorld world, int entity, float time) where T : struct
    {
        ref var delayPool = ref world.GetPool<ExecutionDelayCustom>().Add(world.NewEntity());
        delayPool.time = time;
        delayPool.action = static (world, entity) => world.GetPool<T>().Del(entity);
    }

    public static bool Has<T>(this EcsWorld world, int entity) where T : struct
    {
        return world.GetPool<T>().Has(entity);
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