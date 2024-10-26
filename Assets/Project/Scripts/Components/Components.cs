using UnityEngine;
using System;
using Leopotam.EcsLite;

namespace Client
{ 
    public struct Timer
    {
        public float Time;
    }

    public struct FruitMovement
    {
        public Vector3 TargetPosition;
        public float Speed;
    }
    public struct StoppingSelection { }

    struct TaskCompleted { }

    struct InGroup
    {
        public int ConveyorIndex;
    }

    public struct Task
    {
        public int TargetPoolIndex;
        public int TargetCollect;
    }

    struct Participant
    {
        public int Index;
        public float LookTime;
    }

    struct Bot 
    {
        public float Time;
    }

    public struct CameraZoomRequest 
    {
        public Action Callback;
    }

    struct PopUpRequest 
    {
        public string TextUP;
        public Vector3 SpawnPosition;
        public Quaternion SpawnRotation;
        public Transform Parent;
    }

    struct ChangeStateEvent
    {
        public GameState NewGameState;
    }


    public struct SelectedFruit
    {
        public Fruit fruit;
    }

    public struct RopeCreateRequest { }
    public struct RenderRope { }
    public struct RenderRopeLaunch { }
    public struct RenderRopeReturn { }

    public struct AddToCartRequest { }
    
    struct ReleaseFruitRequest { }

    struct LevelCompleteRequest { }

    public struct InBotResponseZone { }

    public struct ExecutionDelayCustom
    {
        public float time;
        public Action<EcsWorld, int>  action;
    }

    public struct ExecutionDelay
    {
        public float time;
        public Action action;
    }

    public struct ExecutionDelayCustomT1
    {
        public float time;
        public int entity;
        public Action<int> action;
    }

    public struct ExecutionDelayCustomT2
    {
        public float time;
        public int entity;
        public GrapplingRope gr;
        public Action<int, GrapplingRope> action;
    }

    public struct Component<T>
    {
        public T Value;
    }
}