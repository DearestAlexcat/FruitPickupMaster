using UnityEngine;

namespace Client
{
    public class GrapplingRope : MonoBehaviour
    {
        private Spring spring;
        [HideInInspector] public Vector3 currentGrapplePosition;

        public LineRenderer lr;
        public Transform gunTip;
        [HideInInspector] public Transform grapplePoint;

        [Space]
        public int quality;
        public float damperForward;
        public float damperBackward;
        public float strengthForward;
        public float strengthBackward;
        public float velocity;
        public float waveCount;
        public float waveHeight;
        public float drawSpeed;
        [Space]
        public AnimationCurve affectForwardCurve;
        public AnimationCurve affectBackwardCurve;

        float progress;

        void Awake()
        {
            spring = new Spring();
            spring.SetTarget(0);
            currentGrapplePosition = gunTip.position;
        }

        public Spring Spring => spring;

        [HideInInspector] public bool IsPoolThing = false;

        public void RopeReturnInit()
        {
            progress = 0f;
            Spring.SetValue(0f);
            Spring.SetVelocity(velocity);
            currentGrapplePosition = grapplePoint.position;
        }

        public void ResetState()
        {
            lr.positionCount = 0;
            Spring.Reset();
        }

        public void RopeLaunchInit(Transform grapplePoint)
        {
            if (lr.positionCount == 0)
            {          
                Spring.SetVelocity(velocity);
                lr.positionCount = quality + 1;
            }

            this.grapplePoint = grapplePoint;

            progress = 0f;
            currentGrapplePosition = gunTip.position;
        }

        public void ForwardRope(float deltatime)
        {
            progress += Time.deltaTime * drawSpeed;
            currentGrapplePosition = Vector3.Lerp(gunTip.position, grapplePoint.position, progress); // Replace with exp?!
            DrawRope(deltatime, affectForwardCurve, damperForward, strengthForward);
        }

        public void BackwardRope(float deltatime)
        {
            progress += Time.deltaTime * drawSpeed;
            currentGrapplePosition = Vector3.Lerp(grapplePoint.position, gunTip.position, progress);
            DrawRope(deltatime, affectBackwardCurve, damperBackward, strengthBackward);
        }

        public bool IsForwardDrawRopeEnded()
        {
            return Vector3.SqrMagnitude(grapplePoint.position - lr.GetPosition(quality)) < 0.01f;
        }

        public bool IsBackwardDrawRopeEnded()
        {
            return Vector3.SqrMagnitude(lr.GetPosition(quality) - gunTip.position) < 0.01f;
        }

        public void PullGrapplingThing()
        {
            if(IsPoolThing)
            {
                grapplePoint.position = lr.GetPosition(quality);
            }
        }

        void DrawRope(float deltatime, AnimationCurve affectCurve, float damper, float strength)
        {
            Spring.SetDamper(damper);
            Spring.SetStrength(strength);
            Spring.Update(deltatime);                               // Calculate Spring.Value

            var grapplePointPosition = grapplePoint.position;       // end pos 
            var gunTipPosition = gunTip.position;                   // start pos

            // Determine the angle of inclination
            var up = Quaternion.LookRotation((grapplePointPosition - gunTipPosition).normalized) * Vector3.up;

            for (var i = 0; i < quality + 1; i++)
            {
                var delta = i / (float)quality; // 0...1
                var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * Spring.Value *
                             affectCurve.Evaluate(delta);

                lr.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offset);
            }
        }
    }

    public class Spring
    {
        private float strength;
        private float damper;
        private float target;
        private float velocity;
        private float value;

        public void Update(float deltaTime)
        {
            var direction = target - value >= 0 ? 1f : -1f;
            var force = Mathf.Abs(target - value) * strength;
            velocity += (force * direction - velocity * damper) * deltaTime;
            value += velocity * deltaTime;
        }

        public void Reset()
        {
            velocity = 0f;
            value = 0f;
        }

        public void SetValue(float value)
        {
            this.value = value;
        }

        public void SetTarget(float target)
        {
            this.target = target;
        }

        public void SetDamper(float damper)
        {
            this.damper = damper;
        }

        public void SetStrength(float strength)
        {
            this.strength = strength;
        }

        public void SetVelocity(float velocity)
        {
            this.velocity = velocity;
        }

        public float Value => value;
    }
}