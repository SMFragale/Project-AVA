using UnityEngine;

namespace AVA.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidProjectile : Projectile
    {
        Rigidbody rb;

        protected override void OnDestroyProjectile()
        {
            
        }

        protected override void OnProjectileCollision(Collision other)
        {
            
        }

        protected override void OnShootProjectile(Vector3 direction)
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = projectileSpeed * direction;
        }
    }

}