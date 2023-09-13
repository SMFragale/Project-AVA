using System.Collections;
using AVA.Core;
using AVA.State;
using UnityEngine;

namespace AVA.Combat
{
    public class RaycastWeapon : RangeWeapon
    {
        [SerializeField]
        private float _hitLifeTime = 1f;

        [SerializeField]
        private GameObject _hitPrefab;

        private ObjectPool<PoolObject> _hitPool;

        public void Awake()
        {
            _hitPool = new ObjectPool<PoolObject>(_hitPrefab);
        }

        public override void Shoot(Vector3 direction, CharacterState characterState)
        {
            if (Physics.Raycast(origin.position, direction, out RaycastHit hit))
            {
                var hitObject = _hitPool.PullGameObject(hit.point);
                hitObject.transform.forward = hit.normal;

                if (hitObject.TryGetComponent<HitVFX>(out var hitVFX))
                {
                    hitVFX.EmitParticle();
                }
                StartCoroutine(ReturnToPool(hitObject));
                if (hit.collider.TryGetComponent<CombatTarget>(out var combatTarget))
                {
                    combatTarget.TakeDamage(new AttackInstance(characterState.GetStateInstance(), baseAttackDamage, new DefaultMultiplier()));
                }
            }
        }

        private IEnumerator ReturnToPool(GameObject hitObject)
        {
            yield return new WaitForSeconds(_hitLifeTime);
            hitObject.SetActive(false);
        }
    }
}
