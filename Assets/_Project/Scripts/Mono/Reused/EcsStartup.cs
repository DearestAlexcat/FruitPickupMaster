using Client.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections;
using UnityEngine;

namespace Client 
{
    sealed class EcsStartup : MonoBehaviour {
        
        private EcsWorld _world;
        private EcsSystems _systems;

        public SaveInJson saveInJson;
        public Prefabs prefabs;
        public RuntimeData runtimeData;
        public StaticData staticData;
        public SceneContext sceneContext;
       
        public IEnumerator Start () 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            runtimeData = new RuntimeData();
            saveInJson = new SaveInJson();

            Service<SceneContext>.Set(sceneContext);
            Service<RuntimeData>.Set(runtimeData);
            Service<StaticData>.Set(staticData);
            Service<SaveInJson>.Set(saveInJson);
            Service<EcsWorld>.Set(_world);

            GameInitialization.FullInit();

            _systems
                .Add(new InitializeSystem())
                .Add(new ChangeStateSystem())

                .Add(new CameraInitSystem())

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

               // .Add(new DestroyEntitySystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(staticData, runtimeData, prefabs, sceneContext, saveInJson, Service<UI>.Get(), new AnalyticsManager())
                .Init();

            yield return null;
        }

        public void Update () {
            _systems?.Run();
        }

        public void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _world.Destroy ();
                _systems = null;
                _world = null;
            }
        }
    }
}