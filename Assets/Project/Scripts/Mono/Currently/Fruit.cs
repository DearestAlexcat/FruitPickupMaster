using UnityEngine;

namespace Client
{
    public class Fruit : MonoBehaviour
    {
        public int Entity { get; set; }
        public int PoolIndex { get; set; }

        int whoCaptured = -1;

        [SerializeField] MeshCollider meshCollider;
        [SerializeField] Rigidbody thisRigidbody;

        public Rigidbody ThisRigidbody => thisRigidbody;

        public bool SetCapture(int unitEntity)
        {
            if(whoCaptured == -1)
            {
                whoCaptured = unitEntity;
                return true;
            }

            return false;
        }

        public void FreeFruitFromPhysics()
        {
            thisRigidbody.isKinematic = true;
            meshCollider.enabled = false;
        }
    }
}