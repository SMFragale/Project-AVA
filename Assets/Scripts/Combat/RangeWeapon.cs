using System.Collections;
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

        //Make a method that is a couroutine that will shoot every attackRate seconds (use WaitForSeconds) use the shooter.Shoot
        public override void Attack(Vector3 direction)
        {
            var isReady = GetComponentInParent<CharacterState>()?.isReady();
            if (isReady == null || !isReady.Value) return;
            //In the future this should be done within a pool
            var projectileInstance = Instantiate(projectilePrefab, origin.position, Quaternion.identity);
            projectileInstance.layer = gameObject.layer;
            var projectile = projectileInstance.GetComponent<Projectile>();
            projectile.attackInstance = new AttackInstance(characterTransform.GetComponent<CharacterState>().GetStateInstance(), 20f, new DefaultMultiplier());
            projectile.ShootProjectile(direction);
            Destroy(projectile, 2f);
        }
    }
}