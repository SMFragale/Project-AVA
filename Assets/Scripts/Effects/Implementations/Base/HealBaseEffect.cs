using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using AVA.Combat;
using AVA.Core;

public class HealBaseEffect : IBaseEffect
{
    public override string Key => "HealBaseEffect";

    private float _healAmount;
    private GUID _timerGUID;

    private UnityAction<float> _onHealAction;

    public HealBaseEffect(float healAmount, CombatTarget source, UnityAction<float> onHealAction = null) : base(source)
    {
        _healAmount = healAmount;
        _onHealAction = onHealAction;
    }

    public override void Proc()
    {
        _target.HealDamage(_healAmount);
        _onHealAction?.Invoke(_healAmount);
    }

    public override void Start(CombatTarget target)
    {
        base.Start(target);
        TimingEvents timingEvents = new TimingEvents()
        .AddOnEnd(Proc);
        _timerGUID = TimingManager.StartDelayTimer(0.1f, timingEvents);
    }

    protected override int Compare(IBaseEffect other)
    {
        if (other is HealBaseEffect otherEffect)
        {
            return _healAmount.CompareTo(otherEffect._healAmount);
        }
        Debug.LogWarning("Trying to compare HealBaseEffect with a different effect type. This is not the desired behaviour, The compare should be used only on the same effect type.");
        return -1;
    }

    public override void DisposeSelf()
    {
        TimingManager.CancelTimer(_timerGUID);
    }
}
