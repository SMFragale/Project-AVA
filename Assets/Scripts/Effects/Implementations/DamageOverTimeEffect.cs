using UnityEditor;
using AVA.Core;
using AVA.Combat;
using UnityEngine;

public class DamageOverTimeEffect : IBaseEffect
{
    public override string Key => "DamageOverTimeEffect";

    private float _damagePerProc;
    private float _intervalTime;
    private int _totalRepetitions;

    private GUID _timerGUID;

    private HealBaseEffect _healBaseEffect;

    //TODO revisar cual es la mejor manera de construir este efecto, hay varias formas
    public DamageOverTimeEffect(CombatTarget source, float damagePerProc, float intervalTime, int totalRepetitions) : base(source) 
    {
        _intervalTime = intervalTime;
        _damagePerProc = damagePerProc;
        _totalRepetitions = totalRepetitions;
    }

    public override void Start(CombatTarget target)
    {
        base.Start(target);
        TimingEvents timingEvents = new TimingEvents()
        .AddOnReset((int r) => Proc())
        .AddOnReset((int r) => Debug.Log($"HealOverTime reset-> Remaining Resets: {r}") );
        _timerGUID = TimingManager.StartOverTimeTimer(_intervalTime, timingEvents, _totalRepetitions);
        _healBaseEffect = new HealBaseEffect(_damagePerProc, _source);
    }

    public override void Proc()
    {
        _healBaseEffect.Start(_target);
    }
    public override void DisposeSelf()
    {
        TimingManager.CancelTimer(_timerGUID);
        _healBaseEffect.DisposeSelf();
    }

    public float GetTotalDamageAmount() //USed for the compare, maybe its best to do the compare with the heal per tick amount? idk
    {
        return _damagePerProc * _totalRepetitions;
    }

    protected override int Compare(IBaseEffect other)
    {
        if (other is DamageOverTimeEffect otherEffect)
        {
            return GetTotalDamageAmount().CompareTo(otherEffect.GetTotalDamageAmount());
        }
        Debug.LogWarning("Trying to compare HealOverTimeEffect with a different effect type. This is not the desired behaviour, The compare should be used only on the same effect type.");
        return -1;
    }
}
