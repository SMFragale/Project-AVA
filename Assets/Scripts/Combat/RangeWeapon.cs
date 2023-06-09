using System.Collections;
using UnityEngine;

namespace AVA.Combat
{
    public class RangeWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField]
        Transform origin;

        [SerializeField]
        GameObject projectilePrefab;

        [SerializeField]
        [Range(0, 10)]
        private float attackRate = 0.5f;
        private bool isAttacking = false;

        private void Awake()
        {
            isAttacking = true;
        }

        //Make a method that is a couroutine that will shoot every attackRate seconds (use WaitForSeconds) use the shooter.Shoot
        public IEnumerator StartAttacking()
        {
            isAttacking = true;
            while (isAttacking)
            {
                Attack(transform.forward.normalized);
                yield return new WaitForSeconds(attackRate);
            }
        }

        public void StopAttacking()
        {
            isAttacking = false;
        }

        public void Attack(Vector3 direction)
        {
            //In the future this should be done within a pool
            var projectile = Instantiate(projectilePrefab, origin.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().ShootProjectile(direction);
            Destroy(projectile, 5f);
        }
    }

}