using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

namespace Client
{
    public class LevelView : MonoBehaviour
    {
        public float LevelProgress { get; set; } = 1f;
        public int DeathPerLevel { get; set; } = 0;
        [field: SerializeField] public int Index { get; private set; }

        [Header("CONVEYORS")]
        [SerializeField] List<ConveyorElement> conveyors;
        public List<ConveyorElement> Conveyors => conveyors;

        private void Awake()
        {
            Service<EcsWorld>.Get().NewEntity<LevelInitializeRequest>();
        }
    }
}


