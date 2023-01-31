using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class AnimationSystem : IEcsRunSystem, IEcsInitSystem
{
    private readonly EcsFilterInject<Inc<AnimationStateComponent>> _filter = default;

    public void Init(EcsSystems systems)
    {
        //Debug.Log("SittingClap: " + Animator.StringToHash("SittingClap"));
        //Debug.Log("SittingDisapproval: " + Animator.StringToHash("SittingDisapproval"));
        //Debug.Log("SittingIdle: " + Animator.StringToHash("SittingIdle"));
        //Debug.Log("SittingTalking: " + Animator.StringToHash("SittingTalking"));
        //Debug.Log("SittingYell: " + Animator.StringToHash("SittingYell"));
        Debug.Log("Victory: " + Animator.StringToHash("Victory"));
        //Debug.Log("Victory2: " + Animator.StringToHash("Victory2"));
    }

    public void Run(EcsSystems systems)
    {
        foreach (int entity in _filter.Value)
        {
            ref var animationStateComponent = ref _filter.Pools.Inc1.Get(entity);

            if (animationStateComponent.Value != AnimationFlags.NONE)
            {
                animationStateComponent.unitAnimator.ResetTrigger(animationStateComponent.TempHash);

                if ((animationStateComponent.Value & AnimationFlags.BREATHINGIDLE) == AnimationFlags.BREATHINGIDLE)
                {
                    animationStateComponent.TempHash = (int)AnimationState.BREATHINGIDLE;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.BREATHINGIDLE);
                }

                if ((animationStateComponent.Value & AnimationFlags.SILLYDANCING) == AnimationFlags.SILLYDANCING)
                {
                    animationStateComponent.TempHash = (int)AnimationState.SILLYDANCING;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.SILLYDANCING);
                }

                //

                if ((animationStateComponent.Value & AnimationFlags.SITTINGCLAP) == AnimationFlags.SITTINGCLAP)
                {
                    animationStateComponent.TempHash = (int)AnimationState.SITTINGCLAP;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.SITTINGCLAP);
                }


                if ((animationStateComponent.Value & AnimationFlags.SITTINGDISAPPROVAL) == AnimationFlags.SITTINGDISAPPROVAL)
                {
                    animationStateComponent.TempHash = (int)AnimationState.SITTINGDISAPPROVAL;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.SITTINGDISAPPROVAL);
                }


                if ((animationStateComponent.Value & AnimationFlags.SITTINGIDLE) == AnimationFlags.SITTINGIDLE)
                {
                    animationStateComponent.TempHash = (int)AnimationState.SITTINGIDLE;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.SITTINGIDLE);
                }


                if ((animationStateComponent.Value & AnimationFlags.SITTINGTALKING) == AnimationFlags.SITTINGTALKING)
                {
                    animationStateComponent.TempHash = (int)AnimationState.SITTINGTALKING;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.SITTINGTALKING);
                }


                if ((animationStateComponent.Value & AnimationFlags.SITTINGYELL) == AnimationFlags.SITTINGYELL)
                {
                    animationStateComponent.TempHash = (int)AnimationState.SITTINGYELL;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.SITTINGYELL);
                }


                if ((animationStateComponent.Value & AnimationFlags.VICTORY) == AnimationFlags.VICTORY)
                {
                    animationStateComponent.TempHash = (int)AnimationState.VICTORY;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.VICTORY);
                }


                if ((animationStateComponent.Value & AnimationFlags.VICTORY2) == AnimationFlags.VICTORY2)
                {
                    animationStateComponent.TempHash = (int)AnimationState.VICTORY2;
                    animationStateComponent.unitAnimator.SetTrigger((int)AnimationState.VICTORY2);
                }

                animationStateComponent.Value = AnimationFlags.NONE;
            }
        }
    }
}
