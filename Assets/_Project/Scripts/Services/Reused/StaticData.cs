using DG.Tweening;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Required prefabs")]
        public UI UI;

        [Header("CAMERA")]
        public float hFOV = 30f;
        public float orthoSize = 10f;
        public Vector2 DefaultResolution = new Vector2(1920, 1080);
        [Space]
        [Range(0f, 1f)] public float WidthOrHeight = 0;
        public float zoomDuration = 1f;
        public float zoomSpeed = 1f;
        [Range(0f, 1f)] public float zoomFactor = 0.5f;
        [Space]
        public Vector3 camStartPosition;
        public Quaternion camStartRotation;
        [Space]
        public Vector3 camFinPosition;
        public Vector3 camFinRotation;
        public float camFinDuration;
        public Ease camFinEase;

        [Header("LEVEL")]
        public float pauseBeforeEnd = 1f;

        [Header("Levels")]
        public Levels ThisLevels;
    }
}
