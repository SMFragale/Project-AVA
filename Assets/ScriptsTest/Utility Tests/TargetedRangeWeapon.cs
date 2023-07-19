using System.Collections;
using System.Collections.Generic;
using AVA.Combat;
using UnityEditor.Recorder.Input;
using UnityEngine;

public class TargetedRangeWeapon : RangeWeapon
{
    public Transform target;

    public Transform CharacterTransform
    {
        get => characterTransform;
        set => characterTransform = value;
    }

    public float AttackRate
    {
        get => attackRate;
        set => attackRate = value;
    }


    private void Start()
    {
        StartCoroutine(StartTargetedAttack());
    }

    public IEnumerator StartTargetedAttack()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = true;
        while (isAttacking)
        {
            Attack(Vector3.ProjectOnPlane((target.position - transform.position).normalized * 0.5f, Vector3.up));
            yield return new WaitForSeconds(attackRate);
        }
    }
}
