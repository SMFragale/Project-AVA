using AVA.Combat;
using AVA.Core;
using UnityEngine;

public class HealBaseEffect : IBaseEffect
{
    public override string Key => "HealBaseEffect";

    private float _healAmount;
    public HealBaseEffect(float healAmount, CombatTarget source) : base(source)
    {
        _healAmount = healAmount;
    }

    public override void Proc()
    {
        _target.HealDamage(_healAmount);
    }

    public override void Start(CombatTarget target)
    {
        Debug.Log($"Starting HealBaseEffect");
        base.Start(target);
        TimingEvents timingEvents = new TimingEvents()
        .AddOnReset((int i) => Proc());
        TimingManager.StartOverTimeTimer(3f, timingEvents, 10);
    }

    protected override int Compare(IBaseEffect other)
    {
        var otherEffect = other as HealBaseEffect;
        if (otherEffect != null)
        {
            return _healAmount.CompareTo(otherEffect._healAmount);
        }
        Debug.LogWarning("Trying to compare HealBaseEffect with a different effect type. This is not the desired behaviour, The compare should be used only on the same effect type.");
        return -1;
    }

    protected override void DisposeSelf()
    {
        throw new System.NotImplementedException(); //TODO DOTHIS
    }
}
