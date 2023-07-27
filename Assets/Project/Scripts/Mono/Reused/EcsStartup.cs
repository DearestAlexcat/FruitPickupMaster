using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using System.Collections;
using UnityEngine;

namespace Client 
{
    sealed class EcsStartup : MonoBehaviour {
        
        private EcsWorld _world;
        private EcsSystems _systems;

        public Prefabs prefabs;

        public RuntimeData runtimeData;
        public StaticData staticData;
        public SceneContext sceneContext;
       
        public IEnumerator Start() 
        {
            Application.targetFrameRate = 60;

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            runtimeData = new RuntimeData();

            Service<EcsWorld>.Set(_world);
            Service<SceneContext>.Set(sceneContext);
            Service<RuntimeData>.Set(runtimeData);
            Service<StaticData>.Set(staticData);

            GameInitialization.FullInit(); // after reloading the scene

            _systems
                .Add(new CameraInitSystem())
                .Add(new ConveyorsInitSystem())
                .Add(new InitializeSystem())
                .Add(new ChangeStateSystem())

                .Add(new ConveyorSegmentTranslateSystem())
                .Add(new ConveyorsMovementSystem())

                .Add(new SpawnFruitsSystem())          
                .Add(new FruitMovementSystem())
                .Add(new FreeFruitsSystem())
                .Add(new FruitViewDestroySystem())

                .DelHere<MoveSegmentReguest>()
                .DelHere<New>()

                .Add(new PlayerSelectFruitSystem())
                .Add(new BotSelectFruitSystem())

                .Add(new RopeCreatorSystem())
                .Add(new RopeRenderSystem())
                .Add(new RopeFinalizeRenderer())

                .Add(new BasketSystem())

                .Add(new PopUpSystem())
                .Add(new AnimationSystem())

                .Add(new CameraZoomSystem())
                .Add(new LevelCompleteSystem())

                .Add(new ExecutionDelaySystem())

#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(staticData, runtimeData, prefabs, sceneContext, Service<UI>.Get())            
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