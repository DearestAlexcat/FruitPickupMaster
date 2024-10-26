using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class PlayerSelectFruitSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<Participant, InGroup>, Exc<Bot, SelectedFruit, StoppingSelection>> _playerFilter = default;
        private readonly EcsFilterInject<Inc<Component<Fruit>, InGroup>, Exc<ReleaseFruitRequest>> _fruitsFilter = default;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run(IEcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var entity in _playerFilter.Value)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, _sceneContext.Value.FruitMask))
                    {
                        int conveyorIndex = _playerFilter.Pools.Inc2.Get(entity).ConveyorIndex;

                        Fruit fruit = raycastHit.collider.gameObject.GetComponent<Fruit>();

                        if (_fruitsFilter.Pools.Inc2.Get(fruit.Entity).ConveyorIndex == conveyorIndex)
                        {
                            if(fruit.SetCapture(entity))
                            {
                                systems.GetWorld().AddEntityRef<SelectedFruit>(entity).fruit = fruit;
                                systems.GetWorld().AddEntity<RopeCreateRequest>(entity);
                            }
                        }
                    }
                }
            }
        } 
    }
}