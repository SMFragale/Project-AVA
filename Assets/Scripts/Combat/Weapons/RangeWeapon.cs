using AVA.State;
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

        public override void Attack(Vector3 direction, CharacterState characterState)
        {
            var projectileInstance = Instantiate(projectilePrefab, origin.position, Quaternion.identity);
            projectileInstance.layer = gameObject.layer;
            var projectile = projectileInstance.GetComponent<Projectile>();
            projectile.attackInstance = new AttackInstance(characterState.GetStateInstance(), 20f, new DefaultMultiplier());
            projectile.ShootProjectile(direction);
        }
    }
}