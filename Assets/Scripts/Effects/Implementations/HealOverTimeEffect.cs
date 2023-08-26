using UnityEditor;
using AVA.Core;
using AVA.Combat;
using UnityEngine;
using System;

namespace AVA.Effects{

    public class HealOverTimeEffect : IBaseEffect
    {
        public override string Key => "HealOverTimeEffect";

        private float _healPerProc;
        private float _intervalTime;
        private int _totalRepetitions;

        private GUID _timerGUID;

        private HealBaseEffect _healBaseEffect;

        //TODO revisar cual es la mejor manera de construir este efecto, hay varias formas
        public HealOverTimeEffect(float healPerProc, float intervalTime, int totalRepetitions)
        {
            _intervalTime = intervalTime;
            _healPerProc = healPerProc;
            _totalRepetitions = totalRepetitions;
        }

        public override void Start(CharacterEffectServiceReferences target)
        {
            base.Start(target);
            TimingEvents timingEvents = new TimingEvents()
            .AddOnReset((int r) => Proc())
            .AddOnReset((int r) => Debug.Log($"HealOverTime reset-> Remaining Resets: {r}") )
            .AddOnEnd(() => End());
            _timerGUID = TimingManager.StartOverTimeTimer(_intervalTime, timingEvents, _totalRepetitions);
            Debug.Log("GUID Generated in HealOverTimeEffect: " + _timerGUID.ToString());
            _healBaseEffect = new HealBaseEffect(_healPerProc);
        }

        public override void Proc()
        {
            _healBaseEffect.Start(_target);
        }
        public override void CancelEffect()
        {
            TimingManager.CancelTimer(_timerGUID);
            _healBaseEffect.CancelEffect();
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

    public class HealOverTimeEffectFactory : IBaseEffectFactory
    {
        float healPerProc;
        float intervalTime;
        int totalRepetitions;

        public HealOverTimeEffectFactory(float healPerProc, float intervalTime, int totalRepetitions)
        {
            this.healPerProc = healPerProc;
            this.intervalTime = intervalTime;
            this.totalRepetitions = totalRepetitions;
        }

        public override IBaseEffect CreateEffect()
        {
            return new HealOverTimeEffect(healPerProc, intervalTime, totalRepetitions);
        }
    }
    
}
