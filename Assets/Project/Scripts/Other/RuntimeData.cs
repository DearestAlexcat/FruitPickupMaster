using System;
using UnityEngine;

namespace Client
{
    [Serializable]
    public class RuntimeData
    {
        [field: SerializeField] public GameState GameState { get; set; }
    }
}


