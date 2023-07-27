using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class BotSelectFruitSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Bot, Component<PlayerUnit>>, Exc<StopInput>> _playerFilter = default;
        private readonly EcsFilterInject<Inc<Component<Fruit>, InBotResponseZone>, Exc<New, FreeFruitsRequest, SelectedFruit>> _fruitsFilter = default;

        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var item in _playerFilter.Value)
            {
                ref var bot = ref _playerFilter.Pools.Inc2.Get(item).Value;

                bot.time -= Time.deltaTime;

                if (bot.time < 0f)
                {
                    bot.time = Random.Range(bot.delayChoose.x, bot.delayChoose.y);

                    Fruit fruit = GetRandomFruit(bot.LevelTask.TaskIndex);

                    StopInput(item);
                    SelectedFruit(fruit, item);
                }
            }
        }

        private Fruit GetRandomFruit(int taskindex)
        {
            if (Random.value * 100f <= 0.37863f)
            {
                Fruit selectedFruit;

                foreach (var item in _fruitsFilter.Value)
                {
                    selectedFruit = _fruitsFilter.Pools.Inc1.Get(item).Value;
                    if (selectedFruit.PoolIndex == taskindex)
                    {
                        return selectedFruit;
                    }
                }
            }
            else
            {
                int selectIndex = Random.Range(0, _fruitsFilter.Value.GetEntitiesCount());
                int index = 0;

                foreach (var item in _fruitsFilter.Value)
                {
                    if (selectIndex == index)
                    {
                        return _fruitsFilter.Pools.Inc1.Get(item).Value;
                    }

                    index++;
                }
            }

            return null;
        }

        private void SelectedFruit(Fruit f, int entity)
        {
            if(f != null)
            {
                _world.Value.AddEntityRef<SelectedFruit>(entity).fruit = f;
                _world.Value.AddEntity<RopeCreateRequest>(entity);
            }
        }

        private void StopInput(int entity)
        {
            _world.Value.AddEntity<StopInput>(entity);
        }
    }
}
