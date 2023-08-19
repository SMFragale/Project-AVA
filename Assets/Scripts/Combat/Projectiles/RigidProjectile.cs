using UnityEngine;

namespace AVA.Combat
{
    /// <summary>
    /// Rigidbody based projectile that moves in a straight line
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RigidProjectile : Projectile
    {
        Rigidbody rb;

        protected override void OnDestroyProjectile()
        {


        }

        protected override void OnProjectilePierce(Collider other)
        {
        }

        protected override void OnShootProjectile(Vector3 direction)
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = projectileSpeed * direction;
        }
    }

}