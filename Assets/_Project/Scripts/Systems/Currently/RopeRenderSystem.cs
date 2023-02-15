using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class RopeRenderSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HookFruitRequest>> _hookFruitFilter = default;
        private readonly EcsFilterInject<Inc<Component<GrapplingRope>>> _ropePointsFilter = default;
        private readonly EcsWorldInject _world = default;

        public void Run(EcsSystems systems)
        {
            foreach (var item in _ropePointsFilter.Value)
            {
                var comp = _ropePointsFilter.Pools.Inc1.Get(item);

                DrawRope(comp.Value);
            }
        }

        Fruit GetHookFruit()
        {
            foreach (var it in _hookFruitFilter.Value)
            {
                return _hookFruitFilter.Pools.Inc1.Get(it).fruit;
            }

            return null;
        }

        void DrawRope(GrapplingRope gr)
        {
            if (gr.lr.positionCount == 0)
            {
                gr.Spring.SetVelocity(gr.velocity);
                gr.lr.positionCount = gr.quality + 1;
            }

            gr.Spring.SetDamper(gr.damper);
            gr.Spring.SetStrength(gr.strength);
            gr.Spring.Update(Time.deltaTime);

            var grapplePoint = gr.grapplePoint.position;
            var gunTipPosition = gr.gunTip.position;

            var up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

            gr.currentGrapplePosition = Vector3.Lerp(gr.currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);

            for (var i = 0; i < gr.quality + 1; i++)
            {
                var delta = i / (float)gr.quality;
                var offset = up * gr.waveHeight * Mathf.Sin(delta * gr.waveCount * Mathf.PI) * gr.Spring.Value *
                             gr.affectCurve.Evaluate(delta);

                gr.lr.SetPosition(i, Vector3.Lerp(gunTipPosition, gr.currentGrapplePosition, delta) + offset);
            }

        }
    }
}