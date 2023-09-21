using UnityEngine;

namespace AVA.Combat
{
    /// <summary>
    /// Rigidbody based projectile that moves in a straight line
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RigidProjectile : ObjectProjectile
    {
        [SerializeField]
        private Vector3 _fireAngle = Vector3.zero;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            rb = GetComponent<Rigidbody>();
        }

        protected override void OnShoot()
        {
            base.OnShoot();
            Launch();
        }

        private void Launch()
        {
            rb.velocity = (Direction + _fireAngle) * projectileSpeed;
        }

        private void OnDisable()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
    }
}