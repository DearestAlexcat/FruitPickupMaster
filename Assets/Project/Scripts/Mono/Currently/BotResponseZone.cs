using Client;
using Leopotam.EcsLite;
using UnityEngine;

public class BotResponseZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fruit"))
        {
            Service<EcsWorld>.Get().AddEntity<InBotResponseZone>(other.gameObject.GetComponent<Fruit>().Entity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            Service<EcsWorld>.Get().DelEntity<InBotResponseZone>(other.gameObject.GetComponent<Fruit>().Entity);
        }
    }
}
