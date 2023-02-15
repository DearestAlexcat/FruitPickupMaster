using System;
using UnityEngine;

namespace Client
{
    [Serializable]
    public class RuntimeData
    {
        [field: SerializeField] public int LevelId { get; set; }
        [field: SerializeField] public float LevelProgress { get; set; } = 1f;
        [field: SerializeField] public int DeathPerLevel { get; set; } = 0;
        [field: SerializeField] public GameState GameState { get; set; }
    }
}


