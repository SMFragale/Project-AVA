using UnityEngine;

public class HitVFX : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _onCollisionParticles;

    public void EmitParticle()
    {
        _onCollisionParticles.Emit(1);
    }

    private void OnDisable()
    {
        _onCollisionParticles.Clear();
    }
}
