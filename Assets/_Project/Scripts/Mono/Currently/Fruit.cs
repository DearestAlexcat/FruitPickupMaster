using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class Fruit : MonoBehaviour
{
    [HideInInspector] public int Entity;
    [HideInInspector] public int PoolIndex;
    [HideInInspector] public ConveyorElement conveyor;

    [SerializeField] public Transform fruitObj;
    [SerializeField] MeshCollider meshCollider;
    [SerializeField] Rigidbody thisRigidbody;
    [SerializeField] ConfigurableJoint configurableJoint;

    public bool IsValid = true; /*=> gameObject.GetComponents<ConfigurableJoint>() != null;*/
    public bool IsConnectedBody = false;

    public async UniTask FreeFruitFromPhysics()
    {
        thisRigidbody.isKinematic = true;
        configurableJoint.connectedBody = null;
        meshCollider.enabled = false;

        await UniTask.NextFrame();
    }

    public void Hide(float delay)
    {
        // simple delay
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(transform.DOScale(0f, 0.5f))
            .OnComplete(() => Destroy(gameObject));
    }

    public void DestroyConfigurableJoint()
    {
        Destroy(configurableJoint);
    }

    public void SetMeshColliderEnabled(bool value)
    {
        meshCollider.isTrigger = value;
    }

    public void SetConnectedBody(Rigidbody connectedBody)
    {
        IsConnectedBody = true;
        configurableJoint.connectedBody = connectedBody;
    }

    public void EnableIsKinematic(bool value)
    {
        thisRigidbody.isKinematic = value;
    }
}
