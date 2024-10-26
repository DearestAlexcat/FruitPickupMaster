using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

[System.Serializable]
public class PlayerRiggingManager : MonoBehaviour // Temp class. To do: Make animation
{
    [SerializeField] MultiAimConstraint head_AimConstrain;
    [SerializeField] MultiAimConstraint head_LookConstrain;
    [SerializeField] PositionConstraint head_PosConstrain;
    [SerializeField] Transform headLook_PosConstrain;

    [Space]
    [SerializeField] TwoBoneIKConstraint laPose_TwoBoneIK;
    [SerializeField] ChainIKConstraint laPose_ChainIK;
    [SerializeField] MultiAimConstraint laRotator_Aim;
    [SerializeField] TwoBoneIKConstraint raPose_TwoBoneIK;
    [SerializeField] ChainIKConstraint raPose_ChainIK;
    [SerializeField] TwistCorrection body_TwistCorrection;
    [Space]
    [SerializeField] MultiOrientation laTarget;
    [SerializeField] MultiOrientation laim;
    [SerializeField] MultiOrientation raTarget;
    [SerializeField] MultiOrientation raim;

    public Transform target;

    public void InitializeIK()
    {
        SetPose_Idle().Forget();
    }

    public void SetAimTarget_Head(Transform target)
    {
        head_PosConstrain.AddSource(new ConstraintSource() { sourceTransform = target, weight = 1f });
        head_PosConstrain.locked = true;
        head_PosConstrain.constraintActive = true;
    }

    public void LostAimTarget_Head()
    {
        head_PosConstrain.RemoveSource(0);
    }

    public void HeadLook_InitTarget(Transform value)
    {
        target = value;
    }

    public void HeadLook_MoveToTarget()
    {
        headLook_PosConstrain.localPosition = Vector3.MoveTowards(
                  headLook_PosConstrain.localPosition,
                  headLook_PosConstrain.parent.InverseTransformPoint(target.position),
                  Time.deltaTime * 5f);
    }

    async public UniTask SetPose_Laydown()
    {
        laTarget.MoveTo("laydown").Forget();
        laim.MoveTo("laydown").Forget();
        raTarget.MoveTo("laydown").Forget();
        raim.MoveTo("laydown").Forget();

        float time = 0f;

        float startWeight1 = laPose_TwoBoneIK.weight;
        float startWeight2 = laPose_ChainIK.weight;
        float startWeight3 = raPose_TwoBoneIK.weight;
        float startWeight4 = raPose_ChainIK.weight;

        while (time < 1f)
        {
            time += Time.deltaTime / 0.14f;

            laPose_TwoBoneIK.weight = Mathf.Lerp(startWeight1, 1f,   time);
            raPose_TwoBoneIK.weight = Mathf.Lerp(startWeight3, 1f,   time);
            laPose_ChainIK.weight   = Mathf.Lerp(startWeight2, 0.5f, time);
            raPose_ChainIK.weight   = Mathf.Lerp(startWeight4, 0.5f, time);

            await UniTask.NextFrame();
        }
    }

    async public UniTask SetPose_Idle()
    {
        laim.MoveTo("idle").Forget();
        raim.MoveTo("idle").Forget();

        float time = 0f;

        float startWeight1 = laPose_TwoBoneIK.weight;
        float startWeight2 = laPose_ChainIK.weight;
        float startWeight3 = raPose_TwoBoneIK.weight;
        float startWeight4 = raPose_ChainIK.weight;
        float startWeight5 = head_AimConstrain.weight;
        float startWeight6 = body_TwistCorrection.weight;
        float startWeight7 = head_LookConstrain.weight;

        while (time < 1f)
        {
            time += Time.deltaTime / 0.14f;

            laPose_TwoBoneIK.weight     = Mathf.Lerp(startWeight1, 0f, time);
            laPose_ChainIK.weight       = Mathf.Lerp(startWeight2, 1f, time);
            raPose_TwoBoneIK.weight     = Mathf.Lerp(startWeight3, 0f, time);
            raPose_ChainIK.weight       = Mathf.Lerp(startWeight4, 1f, time);
            head_AimConstrain.weight    = Mathf.Lerp(startWeight5, 0f, time);
            body_TwistCorrection.weight = Mathf.Lerp(startWeight6, 0f, time);
            head_LookConstrain.weight   = Mathf.Lerp(startWeight7, 1f, time);

            await UniTask.NextFrame();
        }
    }

    async public UniTask SetPose_Aiming()
    {
        raim.MoveTo("idle").Forget();
        laim.RotateTo("aiming").Forget();

        float time = 0f;

        float startWeight1 = laPose_TwoBoneIK.weight;
        float startWeight2 = laPose_ChainIK.weight;
        float startWeight3 = raPose_TwoBoneIK.weight;
        float startWeight4 = raPose_ChainIK.weight;
        float startWeight5 = head_AimConstrain.weight;
        float startWeight6 = body_TwistCorrection.weight;
        float startWeight7 = head_LookConstrain.weight;

        while (time < 1f)
        {
            time += Time.deltaTime / 0.14f;

            laPose_TwoBoneIK.weight     = Mathf.Lerp(startWeight1, 0f, time);
            laPose_ChainIK.weight       = Mathf.Lerp(startWeight2, 1f, time);
            raPose_TwoBoneIK.weight     = Mathf.Lerp(startWeight3, 0f, time);
            raPose_ChainIK.weight       = Mathf.Lerp(startWeight4, 1f, time);
            head_AimConstrain.weight    = Mathf.Lerp(startWeight5, 1f, time);
            body_TwistCorrection.weight = Mathf.Lerp(startWeight6, 1f, time);
            head_LookConstrain.weight   = Mathf.Lerp(startWeight7, 0f, time);

            await UniTask.NextFrame();
        }
    }

    public void SetPose_Dancing()
    {
        laPose_ChainIK.weight = 0.5f;
        raPose_ChainIK.weight = 0.5f;
        laRotator_Aim.weight = 0f;
        body_TwistCorrection.weight = 0f;
        head_AimConstrain.weight = 0f;
        head_LookConstrain.weight = 0f;
    }
}
