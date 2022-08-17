using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    
    private void Start()
    {
        StartCoroutine(DestroyAfterDuration());
    }

    private IEnumerator DestroyAfterDuration()
    {
        yield return new WaitForSeconds(particle.main.duration);
        Destroy(gameObject);
    }
}