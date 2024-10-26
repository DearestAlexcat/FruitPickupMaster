using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed public class TaskCompletedSystem : IEcsRunSystem 
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        private readonly EcsFilterInject<Inc<TaskCompleted, InGroup>, Exc<StoppingSelection>> _taskCompletedFilter = default;
        private readonly EcsFilterInject<Inc<Participant, InGroup>> _participantFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _taskCompletedFilter.Value)
            {
                int conveyorIndex = _taskCompletedFilter.Pools.Inc2.Get(entity).ConveyorIndex;
                GroupStop(conveyorIndex, entity);
            }
        }

        void GroupStop(int conveyorIndex, int unitWin)
        {
            foreach (var entity in _participantFilter.Value)
            {
                var group = _participantFilter.Pools.Inc2.Get(entity);

                if (group.ConveyorIndex == conveyorIndex)
                {
                    _world.Value.AddEntity<StoppingSelection>(entity);

                    var unit = GetUnit(entity);

                    if (unitWin == entity)
                    {
                        unit.ReplaceTaskOnEmoji(_staticData.Value.emojiCool);
                        unit.riggingManager.SetPose_Dancing();
                        unit.PlayAnimation(PlayerUnit.AnimationFlags.SILLY_DANCING);
                    }
                    else
                    {
                        unit.ReplaceTaskOnEmoji(_staticData.Value.emojiTearyEyes);
                    }
                }
            }

            if (_sceneContext.Value.Groups[conveyorIndex].Conveyor.isGeneral)
            {
                _world.Value.NewEntity<LevelCompleteRequest>();
            }
        }

        private Unit GetUnit(int entity)
        {
            return _sceneContext.Value
                        .Groups[_participantFilter.Pools.Inc2.Get(entity).ConveyorIndex]
                        .Units[_participantFilter.Pools.Inc1.Get(entity).Index];
        }
    }
}