using UnityEngine;

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
        ref var animationComponent = ref EcsWorldEx.GetWorld().GetEntityRef<AnimationStateComponent>(Entity);

        if (animationComponent.TempHash != (int)state)
        {
            animationComponent.Value |= flag;
        }
    }

    private void InitializeUnitEntity()
    {
        Entity = EcsWorldEx.GetWorld().NewEntity<Component<StandUnit>>();
        EcsWorldEx.GetWorld().GetEntityRef<Component<StandUnit>>(Entity).Value = this;
    }

    protected void InitializeAnimationEntity()
    {
        EcsWorldEx.GetWorld().AddEntity<AnimationStateComponent>(Entity);
        EcsWorldEx.GetWorld().GetEntityRef<AnimationStateComponent>(Entity).unitAnimator = animator;
    }
}