using LeoEcsPhysics;
using Leopotam.EcsLite;

namespace Client 
{
    sealed class OnTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsFilter _filterEnter;
        EcsPool<OnTriggerEnterEvent> _poolEnter;

        EcsFilter _filterExit;
        EcsPool<OnTriggerExitEvent> _poolExit;

        public void Init(IEcsSystems systems)
        {
            _filterEnter = systems.GetWorld().Filter<OnTriggerEnterEvent>().End();
            _poolEnter = systems.GetWorld().GetPool<OnTriggerEnterEvent>();

            _filterExit = systems.GetWorld().Filter<OnTriggerExitEvent>().End();
            _poolExit = systems.GetWorld().GetPool<OnTriggerExitEvent>();
        }

        public void Run(IEcsSystems systems) 
        {
            foreach (var entity in _filterEnter)
            {
                ref var eventData = ref _poolEnter.Get(entity);
                var other = eventData.collider;
                
                if (other == null) continue;

                if (eventData.senderGameObject.CompareTag("ConveyorTrigger") && other.CompareTag("Fruit"))
                {
                    systems.GetWorld().AddEntity<ReleaseFruitRequest>(other.gameObject.GetComponent<Fruit>().Entity);
                }

                if (eventData.senderGameObject.CompareTag("BotZone") && other.CompareTag("Fruit"))
                {
                    systems.GetWorld().AddEntity<InBotResponseZone>(other.gameObject.GetComponent<Fruit>().Entity);
                }
            }

            foreach (var entity in _filterExit)
            {
                ref var eventData = ref _poolExit.Get(entity);
                var other = eventData.collider;

                if (other == null) continue;

                if (eventData.senderGameObject.CompareTag("BotZone") && other.CompareTag("Fruit"))
                {
                    systems.GetWorld().DelEntity<InBotResponseZone>(other.gameObject.GetComponent<Fruit>().Entity);
                }
            }
        }
    }
}