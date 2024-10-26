using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class BotSelectFruitSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Bot, InGroup, Task>, Exc<SelectedFruit, StoppingSelection>> _participantFilter = default;
        private readonly EcsFilterInject<Inc<Component<Fruit>, InGroup, InBotResponseZone>> _fruitsFilter = default;

        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        private readonly EcsWorldInject _world = default;

        public void Run(IEcsSystems systems)
        {
            if (_runtimeData.Value.GameState == GameState.PLAYING || _runtimeData.Value.GameState == GameState.LEVEL_COMPLETE)
            {
                foreach (var entity in _participantFilter.Value)
                {
                    ref var time = ref _participantFilter.Pools.Inc1.Get(entity).Time;
                    time -= Time.deltaTime;

                    if (time < 0f)
                    {
                        time = Random.Range(_staticData.Value.botDelayChoose.x, _staticData.Value.botDelayChoose.y);

                        int iteration = 10;

                        while (!SelectedFruit(GetRandomFruit(entity), entity) && iteration > 0) { iteration--; }
                    }
                }
            }
        }

        bool SelectedFruit(Fruit fruit, int entity)
        {
            if (fruit != null)
            {
                if (fruit.SetCapture(entity))
                {
                    _world.Value.AddEntityRef<SelectedFruit>(entity).fruit = fruit;
                    _world.Value.AddEntity<RopeCreateRequest>(entity);

                    return true;
                }
            }

            return false;
        }

        Fruit GetRandomFruit(int entity)
        {
            int conveyorIndex = _participantFilter.Pools.Inc2.Get(entity).ConveyorIndex;
            
            if (Random.value < 0.37863f) // Select a fruit according to the task
            {
                Fruit selectedFruit;

                int taskindex = _participantFilter.Pools.Inc3.Get(entity).TargetPoolIndex;

                foreach (var item in _fruitsFilter.Value)
                {
                    if(conveyorIndex == _fruitsFilter.Pools.Inc2.Get(item).ConveyorIndex)
                    {
                        selectedFruit = _fruitsFilter.Pools.Inc1.Get(item).Value;
                        if (selectedFruit.PoolIndex == taskindex)
                        {
                            return selectedFruit;
                        }
                    }
                }
            }
            else // Otherwise, another fruit
            {
                foreach (var item in _fruitsFilter.Value)
                {
                    if (conveyorIndex == _fruitsFilter.Pools.Inc2.Get(item).ConveyorIndex)
                    {
                        return _fruitsFilter.Pools.Inc1.Get(item).Value;
                    }
                }
            }

            return null;
        }    
    }
}
