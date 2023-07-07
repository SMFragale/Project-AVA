using System.Collections;
using UnityEngine;

namespace AVA.Combat
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        protected Transform characterTransform;

        [SerializeField]
        [Range(0, 10)]
        protected float attackRate = 0.5f;
        protected bool isAttacking = true;

        public abstract void Attack(Vector3 direction);

        public IEnumerator StartAttacking()
        {
            isAttacking = true;
            while (isAttacking)
            {
                Attack(characterTransform.forward.normalized);
                yield return new WaitForSeconds(attackRate);
            }
        }

        public void StopAttacking()
        {
            isAttacking = false;
        }
    }
}