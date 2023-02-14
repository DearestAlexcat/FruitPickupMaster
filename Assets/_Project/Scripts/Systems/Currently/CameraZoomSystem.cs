using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Client
{
    sealed class CameraZoomSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsFilterInject<Inc<CameraZoomRequest>> _zoomFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _zoomFilter.Value)
            {
                var c = _zoomFilter.Pools.Inc1.Get(it);

                ZoomCamera2(c.EndZoomCallback);

                systems.GetWorld().DelEntity(it);
            }
        }

        private float EaseOutQuart(float t)
        {
            return 1f - Mathf.Pow(1f - t, 4f);
        }

        private void ZoomCamera2(System.Action callback)
        {
            DOTween.Sequence()
                .Append(Camera.main.transform.DOMove(_staticData.Value.camFinPosition, _staticData.Value.camFinDuration).SetEase(_staticData.Value.camFinEase).OnComplete(() => callback?.Invoke()))
                .Join(Camera.main.transform.DORotate(_staticData.Value.camFinRotation, _staticData.Value.camFinDuration).SetEase(_staticData.Value.camFinEase).OnComplete(() => callback?.Invoke()));
        }

        private async UniTask ZoomCamera(System.Action callback)
        {
            float t = 0;

            Vector3 targetDirection = Vector3.zero; // (ILevelLink.CurrentLevel.ThisUnit.CameraZoomTarget.position - Camera.main.transform.position);
            Vector3 cameraStartPos = Camera.main.transform.position;
            Vector3 cameraStartDir = Camera.main.transform.forward;

            while (t < 1f)
            {
                Camera.main.transform.position = Vector3.Lerp(cameraStartPos, Vector3.zero /*ILevelLink.CurrentLevel.ThisUnit.CameraZoomTarget.position*/, Mathf.Lerp(0f, _staticData.Value.zoomFactor, EaseOutQuart(t)));
                Camera.main.transform.forward = Vector3.Lerp(cameraStartDir, targetDirection, t);
                t += Time.deltaTime / _staticData.Value.zoomDuration;

                await UniTask.NextFrame();
            }

            callback?.Invoke();
        }
    }
}