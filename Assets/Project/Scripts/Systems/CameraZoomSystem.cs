using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using DG.Tweening;

namespace Client
{
    sealed class CameraZoomSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsFilterInject<Inc<CameraZoomRequest>> _zoomFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var it in _zoomFilter.Value)
            {
                var callback = _zoomFilter.Pools.Inc1.Get(it).Callback;

                Camera.main.transform.DOMove(_staticData.Value.camFinPosition, _staticData.Value.camFinDuration)
                                     .SetEase(_staticData.Value.camFinEase)
                                     .SetLink(Camera.main.gameObject)
                                     .OnKill(callback != null ? callback.Invoke : null);

                Camera.main.transform.DORotate(_staticData.Value.camFinRotation, _staticData.Value.camFinDuration)
                                     .SetEase(_staticData.Value.camFinEase)
                                     .SetLink(Camera.main.gameObject);

                systems.GetWorld().DelEntity(it);
            }
        }
    }
}