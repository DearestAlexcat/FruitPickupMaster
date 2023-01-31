using UnityEngine;
using System;

public sealed class DirectionalParticles : MonoBehaviour
{
    [HideInInspector] public Transform Target;

    public ParticleSystem System => _system;
        
    [SerializeField] private ParticleSystem _system;
        
    private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1000];

    private int _count;

    public Action actionEffect;
    bool actionLoop = true;

    private void Start() => _system.Play();

    private void Update()
    {
        if (_system.isPlaying)
        {   
            _count = _system.GetParticles(_particles);

            for (var i = 0; i < _count; i++)
            {
                var particle = _particles[i];

                var v1 = _system.transform.TransformPoint(particle.position);
                var v2 = Target.transform.position;

                var targetPos = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime);
                    
                particle.position = _system.transform.InverseTransformPoint(v2 - targetPos);
                _particles[i] = particle;

                if (actionLoop && (particle.remainingLifetime / particle.startLifetime) < 0.95f)
                {
                    actionLoop = false;
                    actionEffect.Invoke();
                }
            }

            _system.SetParticles(_particles, _count);
        }
        else
            FinalizeOperation();
    }

    private void FinalizeOperation()
    {
        Destroy(gameObject);
    }
}
