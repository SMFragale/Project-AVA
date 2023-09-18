using AVA.Core;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _onHitParticles;

    private void OnEnable()
    {
        _onHitParticles.Stop();
    }

    private void OnDisable()
    {
        _onHitParticles.Stop();
        _onHitParticles.Clear();
    }

    //Need a method to check wether the particleSystem is playing, need a method to play the particleSystem, need a method to reset the particleSystem
    public void PlayHitEffect(Vector3 position, Vector3 normal)
    {
        _onHitParticles.transform.position = position;
        _onHitParticles.transform.up = normal;
        _onHitParticles.Play();
    }

    public void ResetHitEffect()
    {
        _onHitParticles.Clear();
    }

    public bool IsPlaying()
    {
        return _onHitParticles.isPlaying;
    }


}
