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

        protected override void OnProjectileCollision(Collider other)
        {
            if (other.gameObject.tag == "Entity")
            {
                Debug.Log("Projectile collided with " + other.gameObject.name);
                Destroy(gameObject);
            }
        }

        protected override void OnShootProjectile(Vector3 direction)
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = projectileSpeed * direction;
        }
    }

}