using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class ConveyorGroupInitSystem : IEcsInitSystem 
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        public void Init(IEcsSystems systems) 
        {
            var world = systems.GetWorld();
            var targetCollect = UnityEngine.Random.Range(_staticData.Value.collect.x, _staticData.Value.collect.y + 1);

            for (int i = 0; i < _sceneContext.Value.Groups.Count; i++)
            {
                // Conveyor's Init

                int entity = world.NewEntity<InGroup>();
                world.GetEntityRef<InGroup>(entity).ConveyorIndex = i;
                world.AddEntity<Timer>(entity);

                // Unit's Init

                var conveyor = _sceneContext.Value.Groups[i].Conveyor;
               
                var units = _sceneContext.Value.Groups[i].Units;
                var icons = _staticData.Value.fruitIcons;

                for (int j = 0; j < units.Count; j++)
                {
                    entity = world.NewEntity<Participant>();
                    
                    // Any participant excluding the Bot tag is a player
                    if (units[j] is BotUnit)
                    {
                        world.AddEntityRef<Bot>(entity).Time = UnityEngine.Random.Range(0.5f, 2f);
                    }

                    world.GetEntityRef<Participant>(entity).Index = j;
                    world.AddEntityRef<InGroup>(entity).ConveyorIndex = i;

                    // Init participant task
                    ref var task = ref world.AddEntityRef<Task>(entity);
                    task.TargetCollect = targetCollect;
                    task.TargetPoolIndex = conveyor.GetRandomPoolIndex();

                    units[j].SetActiveTaskHolder(true);
                    units[j].SetTaskText(targetCollect);
                    units[j].SetTaskImage(icons.GetIcon(conveyor.GetTaskName(task.TargetPoolIndex)));
                }
            }
        }
    }
}