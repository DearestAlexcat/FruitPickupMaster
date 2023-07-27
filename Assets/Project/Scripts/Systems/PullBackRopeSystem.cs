using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PullBackRopeSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<Component<GrapplingRope>, SelectedFruit, PullBack>> _pullbackFilter = default;
    private readonly EcsWorldInject _world = default;

    public void Run(EcsSystems systems)
    {
        
    }
}
