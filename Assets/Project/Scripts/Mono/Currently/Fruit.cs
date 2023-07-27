using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Client
{
    public class Fruit : MonoBehaviour
    {
        public int Entity { get; set; }
        public int PoolIndex { get; set; }

        [SerializeField] public Transform fruitObj;
        [SerializeField] MeshCollider meshCollider;
        [SerializeField] Rigidbody thisRigidbody;

        public Rigidbody ThisRigidbody => thisRigidbody;

        [HideInInspector] public bool IsValid;

        public async UniTask FreeFruitFromPhysics()
        {
            thisRigidbody.isKinematic = true;
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

        public void SetMeshColliderEnabled(bool value)
        {
            meshCollider.isTrigger = value;
        }

        public void EnableIsKinematic(bool value)
        {
            thisRigidbody.isKinematic = value;
        }
    }
}