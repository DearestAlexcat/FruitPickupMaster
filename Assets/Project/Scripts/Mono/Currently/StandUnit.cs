using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class StandUnit : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] AnimationFlags flag;

        public void Start()
        {
            PlayAnimation(flag);
        }

        Dictionary<AnimationFlags, int> animationState = new()
        {
            { AnimationFlags.SITTING_CLAP,          -1053750580 },
            { AnimationFlags.SITTING_DISAPPROVAL,   -145981872 },
            { AnimationFlags.SITTING_IDLE,           2028741046 },
            { AnimationFlags.SITTING_TALKING,       -1039877749 },
            { AnimationFlags.SITTING_YELL,           1357620410 },
            { AnimationFlags.VICTORY,               -1090111034 },
            { AnimationFlags.VICTORY2,               1752031153 }
        };

        public enum AnimationFlags
        {
            SITTING_CLAP = 1,
            SITTING_DISAPPROVAL = 2,
            SITTING_IDLE = 4,
            SITTING_TALKING = 8,
            SITTING_YELL = 16,
            VICTORY = 32,
            VICTORY2 = 64
        }

        int tempHash = -1;

        public void PlayAnimation(ValueType flag)
        {
            AnimationFlags value = (AnimationFlags)flag;

            if (animationState.ContainsKey(value) && tempHash == animationState[value]) return;
            animator.ResetTrigger(tempHash);

            tempHash = animationState[value];
            animator.SetTrigger(tempHash);
        }
    }
}