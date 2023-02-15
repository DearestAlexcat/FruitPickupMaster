using UnityEngine;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;

namespace Client
{
    public class FreeFruit : MonoBehaviour
    {
        [SerializeField] GameObject thisGameObject;

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.tag == "FreeFruit" && other.tag == "Fruit")
            {
                Service<EcsWorld>.Get().NewEntityRef<FreeFruitsRequest>().FreeFruit = other.GetComponentInParent<Fruit>();
                return;
            }

            if (gameObject.tag == "SourceRope" && other.tag == "Fruit")
            {
                Fruit hookFruit = other.GetComponentInParent<Fruit>();

                if (hookFruit.IsValid)
                {
                    hookFruit.IsValid = false;
                    Service<EcsWorld>.Get().NewEntityRef<AddToCartRequest>().unit = thisGameObject.GetComponent<PlayerUnit>();
                }

                return;
            }
        }
    }

}
