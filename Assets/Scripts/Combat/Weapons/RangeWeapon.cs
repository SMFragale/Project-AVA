using AVA.State;
using AVA.Core;
using UnityEngine;

namespace AVA.Combat
{
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