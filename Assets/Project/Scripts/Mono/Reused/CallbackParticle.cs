using UnityEngine;

public class CallbackParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem thisparticle;

    private void OnParticleSystemStopped()
    {
    }
}
