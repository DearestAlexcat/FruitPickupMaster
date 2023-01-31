using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using DG.Tweening;

namespace Client 
{
    sealed class PickupFruitSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<PlayerInputRequest>> _inputFilter = default;
        private readonly EcsFilterInject<Inc<Component<PlayerUnit>>> _playerFilter = default;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            if (_inputFilter.Value.IsEmpty()) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, _sceneContext.Value.FruitMask)) 
                {
                    Fruit f = raycastHit.collider.gameObject.GetComponentInParent<Fruit>();

                    if (f.IsValid)
                    {
                        StopPlayerInput();
                        HookFruit(f);
                    }
                }
            }
        }

        private Unit GetPlayer()
        {
            foreach (var item in _playerFilter.Value)
            {
                return _playerFilter.Pools.Inc1.Get(item).Value;
            }

            return null;
        }

        private void HookFruit(Fruit f)
        {
            _world.Value.NewEntityRef<HookFruitRequest>().fruit = f;
            _world.Value.NewEntityRef<RopeCreateRequest>().unit = GetPlayer();
        }

        private void StopPlayerInput()
        {
            foreach (var it in _inputFilter.Value)
            {
                _world.Value.DelEntity(it);
            }
        }
    }
}