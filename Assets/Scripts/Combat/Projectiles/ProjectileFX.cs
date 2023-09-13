using UnityEngine;

namespace AVA.Combat
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileFX : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _onCollisionParticles;

        private Projectile _projectile;

        private void Awake()
        {
            _projectile = GetComponent<Projectile>();
        }

        private void Start()
        {
            _projectile.OnProjectileHit.AddListener(HitEffect);
        }

        //TODO figure out why this is not being called when the projectile hits the environment
        public void HitEffect(ProjectileHitInfo hitInfo)
        {
            Debug.Log("Hit effect");
            _onCollisionParticles.transform.position = hitInfo.ContactPoint;
            _onCollisionParticles.transform.up = hitInfo.ContactNormal;
            _onCollisionParticles.Emit(1);
        }
    }
}