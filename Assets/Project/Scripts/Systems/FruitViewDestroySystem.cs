using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Client;

public class FruitViewDestroySystem :  IEcsRunSystem
{
    private readonly EcsWorldInject _world = default;
    private readonly EcsFilterInject<Inc<Destroyed>> _destroyedFilter = default;

    public void Run(EcsSystems systems)
    {
        foreach (var item in _destroyedFilter.Value)
        {
            _world.Value.DelEntity(item);
        }
    }
}
