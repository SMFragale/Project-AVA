using System.Collections;
using AVA.Core;
using UnityEngine;

namespace AVA.Combat
{
    public class RangeWeaponVFX : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] _onShootParticles;

        [SerializeField]
        private GameObject _hitEffect;

        public ObjectPool<PoolObject> EffectPool { get; private set; }

        [SerializeField]
        private float effectDisableTimer = 1f;

        private void Start()
        {
            GetComponent<RangeWeapon>().OnShoot.AddListener(MuzzleFlashEffect);
            EffectPool = new ObjectPool<PoolObject>(_hitEffect);
            GetComponent<RangeWeapon>().OnProjectileHit.AddListener(HitEffect);
        }

        public void MuzzleFlashEffect()
        {
            foreach (var particleSystem in _onShootParticles)
            {
                particleSystem.Emit(1);
            }
        }

        public void HitEffect(ProjectileHitInfo hitInfo)
        {
            var effect = EffectPool.PullGameObject(hitInfo.ContactPoint, Quaternion.LookRotation(hitInfo.ContactNormal));
            if (effect.TryGetComponent<PoolableVisualEffects>(out var hitEffect))
            {
                hitEffect.PlayEffect(hitInfo.ContactPoint, hitInfo.ContactNormal);
            }
            StartCoroutine(TimedDisable(effect));
        }

        private IEnumerator TimedDisable(GameObject gameObject)
        {
            yield return new WaitForSeconds(effectDisableTimer);
            gameObject.SetActive(false);
        }
    }
}
