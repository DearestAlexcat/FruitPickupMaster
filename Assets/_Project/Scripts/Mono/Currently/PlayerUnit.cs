using UnityEngine;
using Leopotam.EcsLite;
using TMPro;

namespace Client 
{ 
    public class PlayerUnit : Unit
    {
        [field: SerializeField] public Transform PopupPointer { get; private set; }
        [field: SerializeField] public Transform CameraZoomTarget { get; private set; }

        [Header("TASK")]
        [SerializeField] TMP_Text levelTaskText;
        public LevelTask levelTaskData;

        [Header("OTHER LINKS")]
        public Basket basket;

        [Header("ROPE")]
        public Rigidbody connectedBody;
        public GrapplingRope gr;

        [Header("RIGGING")]
        public PlayerRiggingManager riggingManager;

        [Header("PARTICLES")]
        public ParticleSystem pistolFireFX;
        public ParticleSystem addToCartFX;

        private void Awake()
        {
            riggingManager.InitializeIK();
        }

        public void PlayPistolFireFx()
        {
            pistolFireFX.Play(true);
        }

        public void PlayAddToCartFX()
        {
            addToCartFX.Play(true);
        }

        public void InitializeTask(ConveyorElement conveyor)
        {
            levelTaskText.text = levelTaskData.GetTask(conveyor);
            levelTaskText.gameObject.SetActive(levelTaskData.IncludeTask);
        }

        public override void InitializeUnitEntity()
        {
            Entity = Service<EcsWorld>.Get().NewEntity<Component<PlayerUnit>>();
            Service<EcsWorld>.Get().GetEntityRef<Component<PlayerUnit>>(Entity).Value = this;
        }
    }
}