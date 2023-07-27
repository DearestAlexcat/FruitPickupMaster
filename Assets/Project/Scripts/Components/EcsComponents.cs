using UnityEngine;

namespace Client
{ 
    public struct AnimationStateComponent
    {
        public Animator unitAnimator;
        public AnimationFlags Value;
        public int TempHash;
    }

    public struct CameraZoomRequest 
    {
        public System.Action EndZoomCallback;
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

    public struct PullBack
    { 
        public bool Value;
    }

    public struct StopInput { }

    public struct RenderGrapplingForward { }

    public struct RenderGrapplingBackward { }

    public struct CheckGrapplingForward{ }

    public struct CheckGrapplingBackward { }

    public struct SelectedFruit
    {
        public Fruit fruit;
    }

    public struct FinalizeRequest { }

    public struct RopeCreateRequest { }

    public struct AddToCartRequest { }

    struct Player { }

    struct Bot { }

    struct New { }

    struct Destroyed { }

    struct FreeFruitsRequest { }

    struct LevelCompleteRequest { }

    struct InGroup
    {
        public int GroupIndex;
    }

    public struct FruitMovementSettings
    {
        public Vector3 TargetPosition;
        public float Speed;
    }

    public struct InBotResponseZone { }

    public struct ExecutionDelay
    {
        public float time;
        public System.Action action;
    }

    public struct UnitWin { }

    public struct MoveSegmentReguest 
    {
        public int ConveyorEntity;
    }

    public struct Component<T>
    {
        public T Value;
    }
}