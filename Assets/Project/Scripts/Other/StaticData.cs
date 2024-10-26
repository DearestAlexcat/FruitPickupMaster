using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        [Header("Required prefabs")]
        public UI UI;
        public PopupText popup;
        public GameObject emojiCool;
        public GameObject emojiTearyEyes;

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
        public Vector3 camStartRotation;
        public Vector3 camFinPosition;
        public Vector3 camFinRotation;

        [Space]
        public float camFinDuration;
        public Ease camFinEase;

        [Header("LEVEL")]
        public Vector2Int collect;
        public Vector2 botDelayChoose;
        public float pauseBeforeEnd = 1f;
        public float shiftAmplitude = 3f;
        public float fruitSpeed = 1.2f;
        public float delayRopeReturn = 0.15f;
        public float delayAddToCart = 0.15f;

        [Header("FRUIT ICONS")]
        public List<FruitIcons> fruitIcons;

        [Header("Levels")]
        public Levels ThisLevels;
    }
}
