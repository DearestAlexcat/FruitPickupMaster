using UnityEngine.Animations.Rigging;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class PlayerRiggingManager
{
    public Transform targetMover;
    [Space]
    public TwoBoneIKConstraint ArmLeftMoverIdle;
    public MultiAimConstraint ArmLeftRotatorIdle;
    public TwoBoneIKConstraint ArmLeftMoverPut;
    public MultiAimConstraint ArmLeftRotatorPut;
    [Space]
    public TwoBoneIKConstraint ArmRightLeftMoverPut;
    public MultiAimConstraint HeadAimPut;
    [Space]
    public float IKLeftMoveDuration;

    bool targetActive;

    public void SetLeftTwoBoneIKTarget(Vector3 target)
    {
        targetMover.position = target;
    }

    public async UniTask SetTarget(Transform target)
    {
        targetActive = true;

        while (targetActive)
        {
            targetMover.position = target.position;
            await UniTask.NextFrame();
        }
    }

    public void DropTarget()
    {
        targetActive = false;
    }

    public async UniTask SetIKWeightForRightPut(float weight)
    {
        float time = 0f;

        while (time < 1f)
        {
            ArmRightLeftMoverPut.weight = Mathf.Lerp(0f, weight, time);
            HeadAimPut.weight = Mathf.Lerp(0f, weight, time);
            ArmLeftMoverPut.weight = Mathf.Lerp(0f, weight, time);
            ArmLeftRotatorPut.weight = Mathf.Lerp(0f, weight, time);

            time += Time.deltaTime / IKLeftMoveDuration;

            await UniTask.NextFrame();
        }

        ArmLeftMoverPut.weight = weight;
        ArmLeftRotatorPut.weight = weight;
        ArmRightLeftMoverPut.weight = weight;
        HeadAimPut.weight = weight;
    }

    public void SetSinglyIKWeightForLeftIdle(float weight)
    {
        ArmLeftMoverIdle.weight = weight;
        ArmLeftRotatorIdle.weight = weight;
    }

    public async UniTask SetIKWeightForLeftIdle(float weight)
    {
        float time = 0f;

        while (time < 1f)
        {
            ArmLeftMoverIdle.weight = Mathf.Lerp(0f, weight, time);
            ArmLeftRotatorIdle.weight = Mathf.Lerp(0f, weight, time);

            time += Time.deltaTime / IKLeftMoveDuration;

            await UniTask.NextFrame();
        }

        ArmLeftMoverIdle.weight = weight;
        ArmLeftRotatorIdle.weight = weight;
    }

    public void InitializeIK()
    {
        SetIKWeightForLeftIdle(0f).Forget();
    }
}
