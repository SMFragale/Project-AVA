using UnityEngine;
using AVA.Core;

namespace AVA.Combat
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField]
        protected AudioClip shootSound;

        [SerializeField]
        protected float projectileSpeed = 10f;

        [SerializeField]
        protected float destroyTimer = 5f;

        [SerializeField]
        [Range(0, 100)]
        protected int pierceCount = 0;

        public AttackInstance attackInstance;

        private void Start()
        {
            //TODO Handle with object pool
            Destroy(gameObject, destroyTimer);
        }

        public void ShootProjectile(Vector3 direction)
        {
            OnShootProjectile(direction);
        }

        private void OnTriggerEnter(Collider other)
        {

            if (LayerManager.IsInLayerMask(LayerManager.environmentLayer, other.gameObject.layer))
            {
                Destroy(gameObject); // TODO Manage con object pooling
                Debug.Log("Projectile collided with environment");
                return;
            }
            if (LayerManager.IsInLayerMask(LayerManager.pierceLayer, other.gameObject.layer))
            {
                if (pierceCount > 0)
                {
                    pierceCount--;
                    OnProjectilePierce(other);
                    Debug.Log("Projectile pierced " + other.name + " : " + pierceCount);
                }
                else
                    Destroy(gameObject); // TODO Manage con object pooling
            }
        }

        private void OnDestroy()
        {
            OnDestroyProjectile();
        }

        protected abstract void OnProjectilePierce(Collider other);

        protected abstract void OnDestroyProjectile();

        protected abstract void OnShootProjectile(Vector3 direction);
    }

}