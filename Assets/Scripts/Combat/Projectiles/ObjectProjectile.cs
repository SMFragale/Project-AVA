using System.Collections;
using UnityEngine;

namespace AVA.Combat
{

    /// <summary>
    /// Projectiles attached to an object in motion
    /// </summary>
    public class ObjectProjectile : Projectile
    {
        [SerializeField]
        protected float projectileSpeed = 10f;

        [SerializeField]
        protected float destroyTimer = 5f;

        private IEnumerator ProjectileTimeout()
        {
            yield return new WaitForSeconds(destroyTimer);
            ReturnToPool();
        }

        private void OnTriggerEnter(Collider other)
        {
            OnCollision(new ProjectileHitInfo(other.gameObject, transform.position, other.transform.up));
        }

        private void OnCollisionEnter(Collision other)
        {
            OnCollision(new ProjectileHitInfo(other.gameObject, transform.position, other.transform.up));
        }


        /// <summary>
        /// Clears the projectile trail
        /// </summary>
        public void ClearTrailRenderer()
        {
            var particleSystem = GetComponentInChildren<TrailRenderer>();
            if (particleSystem != null)
            {
                particleSystem.Clear();
            }
        }

        protected override void OnStart()
        {
            var particleSystem = GetComponentInChildren<TrailRenderer>();
            if (particleSystem == null) Debug.LogWarning("No particle system found in projectile");
            else
                particleSystem.Clear();
        }

        protected override void OnShoot()
        {
            ClearTrailRenderer();
            StartCoroutine(ProjectileTimeout());
        }
    }
}
