using AVA.State;
using AVA.Core;
using UnityEngine;
using UnityEngine.Events;

namespace AVA.Combat
{
    /// <summary>
    /// Implementation of class Weapon. It shoots ranged projectiles
    /// </summary>
    public class ProjectileWeapon : RangeWeapon
    {
        [SerializeField]
        GameObject projectilePrefab;

        [SerializeField]
        AudioClip defaultShootSound;

        ObjectPool<PoolObject> projectilePool;

        public UnityEvent<ProjectileHitInfo> OnProjectileHit { get; private set; } = new();


        public void Start()
        {
            projectilePool = new ObjectPool<PoolObject>(projectilePrefab);

        }

        /// <summary>
        /// Shoots a projectile in the given direction
        /// </summary>
        /// <param name="direction">The direction in which to shoot the projectile</param>
        /// <param name="characterState">The reference to get the state of the character in the moment of the attack</param>
        public override void Shoot(Vector3 direction, CharacterState characterState)
        {
            GameObject projectileInstance = projectilePool.PullGameObject(origin.position, transform.rotation);
            projectileInstance.layer = gameObject.layer;
            var projectile = projectileInstance.GetComponent<Projectile>();
            projectile.LaunchProjectile(direction, new AttackInstance(characterState.GetStateInstance(), baseAttackDamage, new DefaultMultiplier()));
            projectile.OnProjectileHit.AddListener(OnProjectileHit.Invoke);
        }
    }
}