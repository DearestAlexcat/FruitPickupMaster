using System;
using System.Collections.Generic;

namespace Client 
{
    public class PlayerUnit : Unit
    {
        Dictionary<AnimationFlags, int> animationState = new()
        {
            { AnimationFlags.BREATHING_IDLE, 240973668 },
            { AnimationFlags.SILLY_DANCING, -890657550 }
        };

        public enum AnimationFlags
        {
            BREATHING_IDLE = 1,
            SILLY_DANCING = 2,
        }

        int tempHash = -1;

        public override void PlayAnimation(ValueType flag)
        {
            AnimationFlags value = (AnimationFlags)flag;

            if (animationState.ContainsKey(value) && tempHash == animationState[value]) return;
            animator.ResetTrigger(tempHash);

            tempHash = animationState[value];
            animator.SetTrigger(tempHash);
        }
    }
}