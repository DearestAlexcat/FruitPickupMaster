using System.Collections.Generic;
using UnityEngine;

public struct AnimationStateComponent
{
    public Animator unitAnimator;
    public AnimationFlags Value;
    public int TempHash;
}

public struct RopeCreateRequest 
{
    public Unit unit;
}

public struct AddToCartRequest
{
    public Unit unit;
}

public struct LevelLoseRequest { }

public struct LevelCompleteRequest { }

public struct LevelLoadRequest { }

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

public struct Component<T>
{
    public T Value;
}

public struct HookFruitRequest
{
    public Fruit fruit;
}

public struct LevelInitializeRequest { }

public struct PlayerInputRequest { }
