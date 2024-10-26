using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class LookAtSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Participant, InGroup>, Exc<StoppingSelection>> _participantFilter = default;
        private readonly EcsFilterInject<Inc<Component<Fruit>, InGroup, InBotResponseZone>> _fruitsFilter = default;

        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        public void Run(IEcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var entity in _participantFilter.Value)
            {
                var unit = GetUnit(entity);

                ref var time = ref _participantFilter.Pools.Inc1.Get(entity).LookTime;
                time -= Time.deltaTime;

                if (time < 0f)
                {
                    time = Random.Range(_staticData.Value.botDelayChoose.x, _staticData.Value.botDelayChoose.y);
                    unit.riggingManager.HeadLook_InitTarget(GetRandomFruit(entity));
                }

                unit.riggingManager.HeadLook_MoveToTarget();
            }
        }

        Transform GetRandomFruit(int entity)
        {
            int conveyorIndex = _participantFilter.Pools.Inc2.Get(entity).ConveyorIndex;

            Vector3 startPos = GetConveyor(entity).GetStartPosition();
            float dist = float.MaxValue;
            Transform result = null;

            foreach (var item in _fruitsFilter.Value)
            {
                if (conveyorIndex == _fruitsFilter.Pools.Inc2.Get(item).ConveyorIndex)
                {
                    if(Vector3.Distance(startPos, _fruitsFilter.Pools.Inc1.Get(item).Value.transform.localPosition) < dist)
                    {
                        result = _fruitsFilter.Pools.Inc1.Get(item).Value.transform;
                    }
                }
            }

            return result;
        }

        ConveyorView GetConveyor(int entity)
        {
            return _sceneContext.Value
                        .Groups[_participantFilter.Pools.Inc2.Get(entity).ConveyorIndex].Conveyor;
        }

        Unit GetUnit(int entity)
        {
            return _sceneContext.Value
                        .Groups[_participantFilter.Pools.Inc2.Get(entity).ConveyorIndex]
                        .Units[_participantFilter.Pools.Inc1.Get(entity).Index];
        }
    }
}