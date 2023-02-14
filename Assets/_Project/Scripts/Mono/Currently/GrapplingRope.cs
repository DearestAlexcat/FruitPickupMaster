using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class GrapplingRope : MonoBehaviour
    {
        private Spring spring;
        [HideInInspector] public Vector3 currentGrapplePosition;
        [HideInInspector] public Unit unit;

        public LineRenderer lr;
        public Transform gunTip;
        [HideInInspector] public Transform grapplePoint;
        [Space]
        public int quality;
        public float damper;
        public float strength;
        public float velocity;
        public float waveCount;
        public float waveHeight;
        [Space]
        public AnimationCurve affectCurve;

        void Awake()
        {
            spring = new Spring();
            spring.SetTarget(0);
            currentGrapplePosition = gunTip.position;
        }

        public Spring Spring => spring;
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