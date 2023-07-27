using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class FruitMovementSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<Component<Fruit>, FruitMovementSettings>, Exc<New, FreeFruitsRequest>> _fruitsFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _fruitsFilter.Value)
            {
                ref var fruit = ref _fruitsFilter.Pools.Inc1.Get(it);
                var fruitSettings = _fruitsFilter.Pools.Inc2.Get(it);

                if (!DOTween.IsTweening(fruit.Value.ThisRigidbody))
                {
                    var fruitView = fruit.Value;
                    fruitView.ThisRigidbody.DOComplete();
                    fruitView.ThisRigidbody.DOMoveX(fruitSettings.TargetPosition.x, fruitSettings.Speed)
                        .SetEase(Ease.Linear)
                        .SetSpeedBased()
                        .SetLink(fruitView.gameObject)
                        .OnComplete(() => 
                        {
                            _world.Value.AddEntity<FreeFruitsRequest>(it);
                        });
                }
            }
        }
    }
}