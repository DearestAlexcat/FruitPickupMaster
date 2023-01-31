using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Game/Config")]
public class Config : ScriptableObject
{
    [Header("CAMERA")]
    public float hFOV = 30f;
    public float orthoSize = 10f;
    public float desiredDistanceOrtho = -15f;
    public Vector2 DefaultResolution = new Vector2(1920, 1080);
    [Range(0f, 1f)] public float WidthOrHeight = 0;
    public float zoomDuration = 1f;
    public float zoomSpeed = 1f;
    [Range(0f, 1f)]
    public float zoomFactor = 0.5f;
    [Space] 
    //?
    public Vector3 camStartPosition;
    public Quaternion camStartRotation;
    [Space] 
    // ?
    public Vector3 camFinPosition;
    public Vector3 camFinRotation;
    public float camFinDuration;
    public Ease camFinEase;

    [Header("LEVEL")]
    public float pauseBeforeEnd = 1f;

    [Space]
    public LevelView[] levelDatas;
}

