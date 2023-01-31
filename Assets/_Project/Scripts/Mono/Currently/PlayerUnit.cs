using UnityEngine;

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
        Entity = EcsWorldEx.GetWorld().NewEntity<Component<PlayerUnit>>();
        EcsWorldEx.GetWorld().GetEntityRef<Component<PlayerUnit>>(Entity).Value = this;
    }
}
