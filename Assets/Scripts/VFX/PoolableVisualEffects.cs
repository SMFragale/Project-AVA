using UnityEngine;

public class PoolableVisualEffects : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] particles;

    private void OnEnable()
    {
        foreach (ParticleSystem particleSystem in particles)
        {
            particleSystem.Stop();
        }
    }

    private void OnDisable()
    {
        foreach (ParticleSystem particleSystem in particles)
        {
            particleSystem.Stop();
            particleSystem.Clear();
        }
    }

    //Need a method to check wether the particleSystem is playing, need a method to play the particleSystem, need a method to reset the particleSystem
    public void PlayEffect(Vector3 position, Vector3 normal)
    {
        foreach (ParticleSystem particleSystem in particles)
        {
            particleSystem.transform.position = position;
            particleSystem.transform.up = normal;
            particleSystem.Play();
        }
    }

    public void ResetEffect()
    {
        foreach (ParticleSystem particleSystem in particles)
        {
            particleSystem.Stop();
            particleSystem.Clear();
        }
    }

    public bool IsPlaying()
    {
        foreach (ParticleSystem particleSystem in particles)
        {
            if (particleSystem.isPlaying)
            {
                return true;
            }
        }
        return false;
    }


}
