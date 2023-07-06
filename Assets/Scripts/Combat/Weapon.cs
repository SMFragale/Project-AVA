using System.Collections;
using UnityEngine;

namespace AVA.Combat
{


    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 10)]
        protected float attackRate = 0.5f;
        protected bool isAttacking = true;

        public abstract void Attack(Vector3 direction);

        public abstract IEnumerator StartAttacking();

        public abstract void StopAttacking();
    }
}