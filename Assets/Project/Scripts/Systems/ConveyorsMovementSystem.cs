using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public class ConveyorsMovementSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var group in _sceneContext.Value.Groups)
            {
                group.Conveyor.Move();
            }
        }
    }
}
