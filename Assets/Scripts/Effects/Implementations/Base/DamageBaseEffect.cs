using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using AVA.Combat;
using AVA.Core;

public class DamageBaseEffect : IBaseEffect
{

    public override string Key => "DamageBaseEffect";
    private float _damageAmount;

    private UnityAction<float> _onDamageAction;

    public DamageBaseEffect(float damageAmount, CombatTarget source, UnityAction<float> onDamageAction = null) : base(source)
    {
        _damageAmount = damageAmount;
        _onDamageAction = onDamageAction;
    }
    
    public override void Start(CombatTarget target)
    {
        base.Start(target);
        Proc();
    }

    public override void Proc()
    {
        AttackInstance attackInstance = new(_source.StateInstance, _damageAmount, new DefaultMultiplier());
        var damageDone = _target.TakeDamage(attackInstance);
        _onDamageAction?.Invoke(damageDone);
    }

    protected override int Compare(IBaseEffect other)
    {
        if (other is DamageBaseEffect otherEffect)
        {
            return _damageAmount.CompareTo(otherEffect._damageAmount);
        }
        Debug.LogWarning("Trying to compare DamageBaseEffect with a different effect type. This is not the desired behaviour, The compare should be used only on the same effect type.");
        return -1;
    }

    public override void DisposeSelf()
    {
    }
}
