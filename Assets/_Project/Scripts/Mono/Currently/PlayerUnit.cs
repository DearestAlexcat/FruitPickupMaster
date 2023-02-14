using UnityEngine;
using Leopotam.EcsLite;

namespace Client 
{ 
    public class PlayerUnit : Unit
    {
        [field: SerializeField] public Transform CameraZoomTarget { get; private set; }

        //[Header("RIGGING")]
        //public PlayerRiggingManager riggingManager;

        private void Awake()
        {
            riggingManager.InitializeIK();
            InitializeUnitEntity();
            InitializeAnimationEntity();
        }

        private void InitializeUnitEntity()
        {
            Entity = Service<EcsWorld>.Get().NewEntity<Component<PlayerUnit>>();
            Service<EcsWorld>.Get().GetEntityRef<Component<PlayerUnit>>(Entity).Value = this;
        }
    }
}