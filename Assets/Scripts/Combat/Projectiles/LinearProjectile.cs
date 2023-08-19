
using UnityEngine;

namespace AVA.Combat
{
    /// <summary>
    /// Projectile that moves in a straight line
    /// </summary>
    public class LinearProjectile : Projectile
    {
        private Vector3 direction = Vector3.zero;

        protected override void OnDestroyProjectile()
        {

        }

        protected override void OnProjectilePierce(Collider other)
        {

        }

        protected override void OnShootProjectile(Vector3 direction)
        {
            this.direction = direction.normalized;
        }

        private void Update()
        {
            if (direction != Vector3.zero)
                transform.position += direction * projectileSpeed * Time.deltaTime;
        }

    }

}