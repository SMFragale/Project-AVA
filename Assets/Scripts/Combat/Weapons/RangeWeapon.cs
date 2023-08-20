using AVA.State;
using AVA.Core;
using UnityEngine;

namespace AVA.Combat
{
    /// <summary>
    /// Implementation of class Weapon. It shoots ranged projectiles
    /// </summary>
    public class RangeWeapon : Weapon
    {
        [SerializeField]
        Transform origin;

        [SerializeField]
        GameObject projectilePrefab;

        [SerializeField]
        AudioClip defaultShootSound;

        ObjectPool<PoolObject> projectilePool;


        public void Awake()
        {
            projectilePool = new ObjectPool<PoolObject>(projectilePrefab);
        }

        /// <summary>
        /// Shoots a projectile in the given direction
        /// </summary>
        /// <param name="direction">The direction in which to shoot the projectile</param>
        /// <param name="characterState">The reference to get the state of the character in the moment of the attack</param>
        public override void Attack(Vector3 direction, CharacterState characterState)
        {
            GameObject projectileInstance = projectilePool.PullGameObject(origin.position, Quaternion.identity);
            projectileInstance.layer = gameObject.layer;
            var projectile = projectileInstance.GetComponent<Projectile>();
            projectile.attackInstance = new AttackInstance(characterState.GetStateInstance(), 20f, new DefaultMultiplier());
            projectile.ShootProjectile(direction);
        }
    }
}