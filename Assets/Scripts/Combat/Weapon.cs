using System.Collections;
using UnityEngine;

namespace AVA.Combat {
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void Attack(Vector3 direction);

        public abstract IEnumerator StartAttacking();

        public abstract void StopAttacking();
    }
}