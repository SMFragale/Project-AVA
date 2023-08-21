using UnityEditor;
using AVA.Core;
using AVA.Combat;
using UnityEngine;
using System;

public class HealOverTimeEffect : IBaseEffect
{
    public override string Key => "HealOverTimeEffect";

    private float _healPerProc;
    private float _intervalTime;
    private int _totalRepetitions;

    private GUID _timerGUID;

    private HealBaseEffect _healBaseEffect;

    //TODO revisar cual es la mejor manera de construir este efecto, hay varias formas
    public HealOverTimeEffect(CombatTarget source, float healPerProc, float intervalTime, int totalRepetitions) : base(source) 
    {
        _intervalTime = intervalTime;
        _healPerProc = healPerProc;
        _totalRepetitions = totalRepetitions;
    }

    public override void Start(CombatTarget target)
    {
        base.Start(target);
        TimingEvents timingEvents = new TimingEvents()
        .AddOnReset((int r) => Proc())
        .AddOnReset((int r) => Debug.Log($"HealOverTime reset-> Remaining Resets: {r}") );
        _timerGUID = TimingManager.StartOverTimeTimer(_intervalTime, timingEvents, _totalRepetitions);
        _healBaseEffect = new HealBaseEffect(_healPerProc, _source);
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

    public float GetTotalHealAmount() //USed for the compare, maybe its best to do the compare with the heal per tick amount? idk
    {
        return _healPerProc * _totalRepetitions;
    }

    protected override int Compare(IBaseEffect other)
    {
        if (other is HealOverTimeEffect otherEffect)
        {
            return GetTotalHealAmount().CompareTo(otherEffect.GetTotalHealAmount());
        }
        Debug.LogWarning("Trying to compare HealOverTimeEffect with a different effect type. This is not the desired behaviour, The compare should be used only on the same effect type.");
        return -1;
    }
}
