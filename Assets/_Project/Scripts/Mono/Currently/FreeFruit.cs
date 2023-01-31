using UnityEngine;
using Cysharp.Threading.Tasks;

public class FreeFruit : MonoBehaviour
{
    [SerializeField] GameObject thisGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "FreeFruit" && other.tag == "Fruit")
        {
            EcsWorldEx.GetWorld().NewEntityRef<FreeFruitsRequest>().FreeFruit = other.GetComponentInParent<Fruit>();
            return;
        }

        if (gameObject.tag == "SourceRope" && other.tag == "Fruit")
        {
            Fruit hookFruit = other.GetComponentInParent<Fruit>();

            if(hookFruit.IsValid)
            {
                hookFruit.IsValid = false;
                EcsWorldEx.GetWorld().NewEntityRef<AddToCartRequest>().unit = thisGameObject.GetComponent<Unit>();
            }

            return;
        }
    }
}
