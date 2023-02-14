using Leopotam.EcsLite;
using UnityEngine;

namespace Client
{
    public class StandUnit : MonoBehaviour // ?
    {
        [HideInInspector] public int Entity;

        public Animator animator;

        public AnimationState state;
        public AnimationFlags flag;

        private void Awake()
        {
            InitializeUnitEntity();
            InitializeAnimationEntity();
            PlayAnimation(state, flag);
        }

        public void PlayAnimation(AnimationState state, AnimationFlags flag)
        {
            ref var animationComponent = ref Service<EcsWorld>.Get().GetEntityRef<AnimationStateComponent>(Entity);

            if (animationComponent.TempHash != (int)state)
            {
                animationComponent.Value |= flag;
            }
        }

        private void InitializeUnitEntity()
        {
            Entity = Service<EcsWorld>.Get().NewEntity<Component<StandUnit>>();
            Service<EcsWorld>.Get().GetEntityRef<Component<StandUnit>>(Entity).Value = this;
        }

        protected void InitializeAnimationEntity()
        {
            Service<EcsWorld>.Get().AddEntityRef<AnimationStateComponent>(Entity).unitAnimator = animator;
        }
    }
}