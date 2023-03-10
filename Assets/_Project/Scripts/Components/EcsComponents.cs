using System.Collections.Generic;
using UnityEngine;

namespace Client
{ 
    public struct AnimationStateComponent
    {
        public Animator unitAnimator;
        public AnimationFlags Value;
        public int TempHash;
    }

    public struct RopeCreateRequest 
    {
        public PlayerUnit unit;
    }

    public struct AddToCartRequest
    {
        public PlayerUnit unit;
    }

    public struct FinalizeRequestComponent 
    {
        public LevelEndState Value;
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

    struct SpawnFruitsRequest { }

    struct FreeFruitsRequest
    {
        public Fruit FreeFruit;
    }

    struct UVScrollingComponent 
    {
        public Vector2 CurrentOffset;
        public Vector2 Speed;
        public MeshRenderer ScrollingObject;
    }

    public struct RopePointsComponent
    {
        public LineRenderer ThisLineRenderer;
        public List<Transform> Points;
    }

    struct ChangeStateEvent
    {
        public GameState NewGameState;
    }

    public struct PlayerInputComponent { }

    public struct Component<T>
    {
        public T Value;
    }

    public struct HookFruitRequest
    {
        public Fruit fruit;
    }
}