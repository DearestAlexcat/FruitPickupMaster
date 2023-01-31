using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [field: SerializeField] public Transform PopupPointer { get; private set; }

    public int Entity { get; protected set; }

    [Header("TASK")]
    [SerializeField] TMP_Text levelTaskText;
    public LevelTask levelTaskData;

    [Header("OTHER LINKS")]
    public Basket basket;
    [SerializeField] protected Animator animator;

    [Header("ROPE")]
    public Rigidbody connectedBody;
    public GrapplingRope gr;

    [Header("RIGGING")]
    public PlayerRiggingManager riggingManager;

    [Header("PARTICLES")]
    public ParticleSystem pistolFireFX;
    public ParticleSystem addToCartFX;

    public void PlayPistolFireFx()
    {
        pistolFireFX.Play(true);
    }

    public void PlayAddToCartFX()
    {
        addToCartFX.Play(true);
    }

    public void InitializeTask(ConveyorElement conveyor)
    {
        levelTaskText.text = levelTaskData.GetTask(conveyor);
        levelTaskText.gameObject.SetActive(levelTaskData.IncludeTask);
    }

    public void PlayAnimation(AnimationState state, AnimationFlags flag)
    {
        ref var animationComponent = ref EcsWorldEx.GetWorld().GetEntityRef<AnimationStateComponent>(Entity);

        if (animationComponent.TempHash != (int)state)
        {
            animationComponent.Value |= flag;
        }
    }

    protected void InitializeAnimationEntity()
    {
        EcsWorldEx.GetWorld().AddEntity<AnimationStateComponent>(Entity);
        EcsWorldEx.GetWorld().GetEntityRef<AnimationStateComponent>(Entity).unitAnimator = animator;
    }
}


