using Leopotam.EcsLite;
using System.Collections;

namespace Client
{
    public class StandUnit : Unit
    {
        public AnimationState state;
        public AnimationFlags flag;

        private IEnumerator Start() // ! Execute after ecs init
        {
            yield return null;

            PlayAnimation(state, flag);
        }

        public override void InitializeUnitEntity()
        {
            Entity = Service<EcsWorld>.Get().NewEntity<Component<StandUnit>>();
            Service<EcsWorld>.Get().GetEntityRef<Component<StandUnit>>(Entity).Value = this;
        }
    }
}