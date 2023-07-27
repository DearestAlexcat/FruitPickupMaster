using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        public Vector3 camStartRotation;
        public Vector3 camFinPosition;
        public Vector3 camFinRotation;

        [Space]
        public float camFinDuration;
        public Ease camFinEase;

        [Header("LEVEL")]
        public float pauseBeforeEnd = 1f;
        public float shiftAmplitude = 3f;   // fruit spawn in sphere

        [Header("FRUIT ICONS")]
        public List<FruitIcons> fruitIcons;

        [Header("Levels")]
        public Levels ThisLevels;

        // --------------------------------------------------------------------------------

        [System.Serializable]
        public class FruitIcons
        {
            public Sprite icon;
            public string key;
        }

        public Sprite GetIcon(string key)
        {
            foreach (var item in fruitIcons)
            {
                if(item.key == key)
                {
                    return item.icon;
                }
            }

            return null;
        }
    }
}
