using System.Collections.Generic;
using UnityEngine;

namespace Client
{ 
    public class SceneContext : MonoBehaviour
    {
        [field: SerializeField] public List<ConveyorGroup> Groups { get; private set; }
        [field: SerializeField] public ConfettiService ConfettiService { get; private set; }

        [HideInInspector] public int FruitMask { get; private set; }

        private void Awake()
        {
            FruitMask = LayerMask.GetMask("FruitMask");
        }
    }
}