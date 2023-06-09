using System.Collections;
using UnityEngine;

public interface IWeapon
{
    void Attack(Vector3 direction);

    IEnumerator StartAttacking();

    void StopAttacking();
}
