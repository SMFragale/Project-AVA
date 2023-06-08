using UnityEngine;

namespace AVA.Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField]
        protected AudioClip shootSound;

        [SerializeField]
        protected float projectileSpeed = 10f;

        public void ShootProjectile(Vector3 direction)
        {
            OnShootProjectile(direction);
        }

        // Maybe add an explosion effect here
        private void OnCollisionEnter(Collision other)
        {
            OnProjectileCollision(other);
        }

        private void OnDestroy()
        {
            OnDestroyProjectile();
        }

        protected abstract void OnProjectileCollision(Collision other);

        protected abstract void OnDestroyProjectile();

        protected abstract void OnShootProjectile(Vector3 direction);
    }

}