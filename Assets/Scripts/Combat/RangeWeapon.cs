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
        public override IEnumerator StartAttacking()
        {
            isAttacking = true;
            while (isAttacking)
            {
                Attack(transform.forward.normalized);
                yield return new WaitForSeconds(attackRate);
            }
        }

        public override void StopAttacking()
        {
            isAttacking = false;
        }

        public override void Attack(Vector3 direction)
        {
            if (!GetComponentInParent<CharacterState>().isReady()) return;
            //In the future this should be done within a pool
            var projectileInstance = Instantiate(projectilePrefab, origin.position, Quaternion.identity);
            var projectile = projectileInstance.GetComponent<Projectile>();
            projectile.attackInstance = new AttackInstance(GetComponentInParent<CharacterState>().GetStateInstance(), 20f, new DefaultMultiplier());
            projectile.ShootProjectile(direction);
            Destroy(projectile, 5f);
        }
    }

}