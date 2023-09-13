using AVA.Combat;
using UnityEngine;


[RequireComponent(typeof(RangeWeapon))]
public class RangeWeaponVFX : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _onShootParticles;

    private void Start()
    {
        GetComponent<RangeWeapon>().OnShoot.AddListener(MuzzleFlashEffect);
    }

    public void MuzzleFlashEffect()
    {
        foreach (var particleSystem in _onShootParticles)
        {
            particleSystem.Emit(1);
        }
    }
}