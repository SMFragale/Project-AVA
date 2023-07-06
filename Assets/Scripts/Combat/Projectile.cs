using UnityEngine;

namespace AVA.Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField]
        protected AudioClip shootSound;

        [SerializeField]
        protected float projectileSpeed = 10f;

        public AttackInstance attackInstance;

        public void ShootProjectile(Vector3 direction)
        {
            OnShootProjectile(direction);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnProjectileCollision(other);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnDestroyProjectile();
        }

        protected abstract void OnProjectileCollision(Collider other);

        protected abstract void OnDestroyProjectile();

        protected abstract void OnShootProjectile(Vector3 direction);
    }

}