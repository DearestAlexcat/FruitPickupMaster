using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{

    // PickupFruitSystem

    sealed class PlayerSelectFruitSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<Player, Component<PlayerUnit>>, Exc<StopInput>> _playerFilter = default;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        private readonly EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var item in _playerFilter.Value)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, _sceneContext.Value.FruitMask))
                    {
                        Fruit fruit = raycastHit.collider.gameObject.GetComponent<Fruit>();

                        if (fruit.IsValid)
                        {
                            StopInput(item);
                            SelectedFruit(fruit, item);
                        }
                    }
                }
            }
        } 

        private void SelectedFruit(Fruit f, int entity)
        {
            _world.Value.AddEntityRef<SelectedFruit>(entity).fruit = f;
            _world.Value.AddEntity<RopeCreateRequest>(entity);
        }

        private void StopInput(int entity)
        {
            _world.Value.AddEntity<StopInput>(entity);
        }
    }
}