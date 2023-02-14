using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using DG.Tweening;

namespace Client 
{
    sealed class CameraInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneContext> _sceneContext = default;
        private readonly EcsCustomInject<StaticData> _staticData = default;

        private float initialSize;
        private float targetAspect;
        private float initialFov;
        private float horizontalFov = 120f;

        public void Init(EcsSystems systems)
        {
            Init();
            SetFOV();
            InitOrientation();
        }

        private void InitOrientation()
        {
            _staticData.Value.camStartPosition = _sceneContext.Value.Camera.transform.position;
            _staticData.Value.camStartRotation = _sceneContext.Value.Camera.transform.rotation;
        }

        private void Monitoring()
        {
            SetFOV();
            Debug.Log(_sceneContext.Value.Camera.fieldOfView);
        }

        public void Run(EcsSystems systems)
        {
           // Monitoring();
        }

        //private void SetFOV()
        //{
        //    float hFOVrad = _staticData.Value.hFOV * Mathf.Deg2Rad;
        //    float camH = Mathf.Tan(hFOVrad * 0.5f) / _sceneContext.Value.Camera.aspect;
        //    float vFOVrad = Mathf.Atan(camH) * 2f;

        //    var fov = vFOVrad * Mathf.Rad2Deg;

        //    _sceneContext.Value.Camera.fieldOfView = /*fov < 60f ? 60f :*/ fov; //?

        //    var halfFrustumHeight = _staticData.Value.desiredDistanceOrtho * Mathf.Tan(_sceneContext.Value.Camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        //    _sceneContext.Value.CameraOrtho.orthographicSize = Mathf.Abs(halfFrustumHeight);
        //}

        private void Init()
        {
           // var halfFrustumHeight = _staticData.Value.desiredDistanceOrtho * Mathf.Tan(_sceneContext.Value.Camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
           // _sceneContext.Value.CameraOrtho.orthographicSize = Mathf.Abs(halfFrustumHeight);
           
            initialSize = _sceneContext.Value.Camera.orthographicSize;

            targetAspect = _staticData.Value.DefaultResolution.x / _staticData.Value.DefaultResolution.y;

            initialFov = _sceneContext.Value.Camera.fieldOfView;
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
            if (_sceneContext.Value.Camera.orthographic)
            {
                float constantWidthSize = initialSize * (targetAspect / _sceneContext.Value.Camera.aspect);
                _sceneContext.Value.Camera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, _staticData.Value.WidthOrHeight);
            }
            else
            {
                float constantWidthFov = CalcVerticalFov(horizontalFov, _sceneContext.Value.Camera.aspect);
                _sceneContext.Value.Camera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, _staticData.Value.WidthOrHeight);
            }
        }

    }
}