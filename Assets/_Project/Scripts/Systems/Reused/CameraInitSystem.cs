using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class CameraInitSystem : IEcsInitSystem/*, IEcsRunSystem*/
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        private float initialSize;
        private float targetAspect;
        private float initialFov;
        private float horizontalFov;

        public void Init(EcsSystems systems)
        {
            Init();
            SetFOV();
            InitOrientation();
        }

        private void InitOrientation()
        {
            _staticData.Value.camStartPosition = Camera.main.transform.position;
            _staticData.Value.camStartRotation = Camera.main.transform.rotation;
        }

        private void Monitoring()
        {
            Init();
            SetFOV();
            Debug.Log(Camera.main.fieldOfView);
        }

        //public void Run(EcsSystems systems)
        //{
        //    Monitoring();
        //}

        private void Init()
        {
            initialSize = _staticData.Value.orthoSize;
            targetAspect = _staticData.Value.DefaultResolution.x / _staticData.Value.DefaultResolution.y;

            initialFov = _staticData.Value.hFOV;
            horizontalFov = CalcVerticalFov(initialFov, 1f / targetAspect);
        }

        private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            float hFovInRads = hFovInDeg * Mathf.Deg2Rad;
            float vFovInRads = 2f * Mathf.Atan(Mathf.Tan(hFovInRads / 2f) / aspectRatio);
            return vFovInRads * Mathf.Rad2Deg;
        }

        private void SetFOV()
        {
            if (Camera.main.orthographic)
            {
                float constantWidthSize = initialSize * (targetAspect / Camera.main.aspect);
                Camera.main.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, _staticData.Value.WidthOrHeight);
            }
            else
            {
                float constantWidthFov = CalcVerticalFov(horizontalFov, Camera.main.aspect);
                Camera.main.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, _staticData.Value.WidthOrHeight);
            }
        }

    }
}