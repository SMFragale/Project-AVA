using System.Collections;
using UnityEngine;

namespace AVA.Combat {
    public interface IWeapon
    {
        void Attack(Vector3 direction);

        IEnumerator StartAttacking();

        void StopAttacking();
    }
}

