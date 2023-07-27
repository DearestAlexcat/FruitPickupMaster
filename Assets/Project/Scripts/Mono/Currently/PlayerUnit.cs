using UnityEngine;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine.UI;

namespace Client 
{ 
    public class PlayerUnit : Unit
    {
        [field: SerializeField] public Transform PopupPointer { get; private set; }
        [field: SerializeField] public Transform CameraZoomTarget { get; private set; }

        [Header("TASK")]
        [SerializeField] GameObject levelTaskCloud;
        [SerializeField] Image levelTaskImage;
        [SerializeField] TMP_Text levelTaskText;
        [SerializeField] LevelTask levelTaskData;

        [Header("OTHER LINKS")]
        public Basket basket;
        public ConveyorView conveyor;

        [Header("BOT")]
        public bool IsBot = false;
        [HideInInspector] public float time; 
        public Vector2 delayChoose = new Vector2(1f, 2f);

        [Header("ROPE")]
        public Rigidbody connectedBody;
        public GrapplingRope gr;

        [Header("RIGGING")]
        public PlayerRiggingManager riggingManager;

        [Header("PARTICLES")]
        public ParticleSystem pistolFireFX;
        public ParticleSystem addToCartFX;

        public LevelTask LevelTask => levelTaskData;

        private void Start()
        {
            time = Random.Range(delayChoose.x * 3, delayChoose.y * 3);
            riggingManager.InitializeIK();
            InitializeTask();
        }

        public void DecreaseCollect()
        {
            levelTaskText.text = --levelTaskData.targetCollect + "";
        }

        public void PlayPistolFireFx()
        {
            pistolFireFX.Play(true);
        }

        public void PlayAddToCartFX()
        {
            addToCartFX.Play(true);
        }

        public void InitializeTask()
        {
            var returns = levelTaskData.GetTask(conveyor);

            levelTaskImage.sprite = returns.Item2;
            levelTaskText.text = returns.Item1;
            levelTaskText.gameObject.SetActive(levelTaskData.IncludeTask);
        }

        public void HideLevelTaskCloud()
        {
            levelTaskCloud.SetActive(false);
        }

        public override void InitializeUnitEntity()
        {
            var world = Service<EcsWorld>.Get();

            Entity = world.NewEntity<Component<PlayerUnit>>();

            world.GetEntityRef<Component<PlayerUnit>>(Entity).Value = this;

            if (IsBot)
                world.AddEntity<Bot>(Entity);
            else
                world.AddEntity<Player>(Entity);
        }
    }
}