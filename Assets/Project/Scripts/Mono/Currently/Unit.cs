using UnityEngine;
using Leopotam.EcsLite;

namespace Client
{
    public abstract class Unit : MonoBehaviour
    {
        public int Entity { get; protected set; }

        [SerializeField] protected Animator animator;

        public void PlayAnimation(AnimationState state, AnimationFlags flag)
        {
            ref var animationComponent = ref Service<EcsWorld>.Get().GetEntityRef<AnimationStateComponent>(Entity);

            if (animationComponent.TempHash != (int)state)
            {
                animationComponent.Value |= flag;
            }
        }

        public virtual void InitializeAnimationEntity() 
        {
            Service<EcsWorld>.Get().AddEntityRef<AnimationStateComponent>(Entity).unitAnimator = animator;
        }

        public virtual void InitializeUnitEntity() { }
    }
}
