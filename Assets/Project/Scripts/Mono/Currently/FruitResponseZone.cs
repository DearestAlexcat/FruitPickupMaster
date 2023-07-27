using Client;
using Leopotam.EcsLite;
using UnityEngine;

public class FruitResponseZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            Fruit fruit = other.gameObject.GetComponent<Fruit>();
            Service<EcsWorld>.Get().AddEntity<FreeFruitsRequest>(fruit.Entity);
        }
    }
}
