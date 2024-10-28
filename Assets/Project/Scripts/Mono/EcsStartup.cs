using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using System.Collections;
using UnityEngine;

namespace Client 
{
    sealed class EcsStartup : MonoBehaviour {
        
        private EcsWorld _world;

        IEcsSystems _update;
        IEcsSystems _fixedUpdate;

        public RuntimeData runtimeData;
       
        public IEnumerator Start() 
        {
            Application.targetFrameRate = 60;

            _world = new EcsWorld();
            _update = new EcsSystems(_world);
            _fixedUpdate = new EcsSystems(_world);
            EcsPhysicsEvents.ecsWorld = _world;

            runtimeData = new RuntimeData();

            Service<EcsWorld>.Set(_world);

            _update
                .Add(new CameraInitSystem())          
                .Add(new ConveyorGroupInitSystem())
                .Add(new InitializeSystem())           
                .Add(new ChangeStateSystem())         

                .Add(new SpawnFruitsSystem())          
                .Add(new FreeFruitsSystem())           

                .Add(new PlayerSelectFruitSystem())
                .Add(new BotSelectFruitSystem())

                .Add(new LookAtSystem())

                .Add(new RopeCreatorSystem())
                .DelHere<RopeCreateRequest>()
                .Add(new RenderRopeSystem())
                .Add(new RopeFinalizeRenderer())

                .Add(new BasketSystem())            

                .Add(new PopupSystem())

                .Add(new TaskCompletedSystem())
                .Add(new LevelCompleteSystem())
                .Add(new CameraZoomSystem())

                .Add(new ExecutionDelaySystem())

#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(runtimeData, Service<StaticData>.Get(), GetComponent<SceneContext>())            
                .Init();

            _fixedUpdate
               .Add(new OnTriggerSystem())
               .Add(new FruitMovementSystem())            

               .Inject(GetComponent<SceneContext>(), runtimeData, Service<StaticData>.Get())

               .DelHerePhysics()
               .Init();


            yield return null;
        }

        public void Update () {
            _update?.Run();
        }

        public void FixedUpdate() {
            _fixedUpdate?.Run();
        }

        public void OnDestroy()
        {
            if (_world != null)
            {
                EcsPhysicsEvents.ecsWorld = null;
                _world.Destroy();
                _world = null;
            }

            if (_update != null)
            {
                _update.Destroy();
                _update = null;
            }

            if (_fixedUpdate != null)
            {
                _fixedUpdate.Destroy();
                _fixedUpdate = null;
            }
        }
    }
}