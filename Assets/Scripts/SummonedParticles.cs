using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SummonedParticles : MonoBehaviour {
    new ParticleSystem particleSystem;

    private void Start() {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (!particleSystem.IsAlive())
            Destroy(gameObject);
    }
}