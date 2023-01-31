using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    
    sealed class EcsStartup : MonoBehaviour {
        
        EcsSystems _systems;

        [SerializeField] private Config _config = default;
        [SerializeField] private Prefabs _prefabs = default;
        
        private SceneContext _sceneContext = default;
        private SaveInJson _saveInJson = new SaveInJson();

        public Prefabs Prefab => _prefabs;
        public Config Config => _config;
        public SaveData SaveInJson => _saveInJson.GetData;
        public SceneContext SceneContext => _sceneContext;

        static public EcsStartup Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void Start () 
        {
            _sceneContext = GetComponent<SceneContext>();

            _systems = new EcsSystems (EcsWorldEx.GetWorld());
            _systems
                .Add(new GameInitSystem())
                .Add(new CameraInitSystem())

                .Add(new LevelCreatorSystem())
                .Add(new LevelInitializeSystem())

                .Add(new UVScrollingSystem())

                .Add(new SpawnFruitsSystem())
                .Add(new FruitMovementSystem())
                .Add(new FreeFruitsSystem())
                .Add(new PickupFruitSystem())

                .Add(new BasketSystem())

                .Add(new RopeCreatorSystem())
                .Add(new RopeRenderSystem())

                .Add(new PopUpSystem())
                .Add(new AnimationSystem())
                
                // .Add(new FxSystem())

                .Add(new CameraZoomSystem())

                .Add(new LevelCompleteSystem())

                .Add(new DestroyEntitySystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(_config, _prefabs, _sceneContext, new AnalyticsManager(), _saveInJson)
                .Init();
        }

        public void Update () {
            _systems?.Run();
        }

        public void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems.GetWorld ().Destroy ();
                _systems = null;
            }
        }
    }
}