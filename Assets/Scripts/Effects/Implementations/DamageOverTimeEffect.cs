using UnityEditor;
using AVA.Core;
using AVA.Combat;
using UnityEngine;

namespace AVA.Effects{
    
    public class DamageOverTimeEffect : IBaseEffect
    {
        public override string Key => "DamageOverTimeEffect";

        private float _damagePerProc;
        private float _intervalTime;
        private int _totalRepetitions;

        private GUID _timerGUID;

        private DamageBaseEffect _damageBaseEffect;

        //TODO revisar cual es la mejor manera de construir este efecto, hay varias formas
        public DamageOverTimeEffect(float damagePerProc, float intervalTime, int totalRepetitions)
        {
            _intervalTime = intervalTime;
            _damagePerProc = damagePerProc;
            _totalRepetitions = totalRepetitions;
        }

        public override void Start(CharacterEffectServiceReferences target)
        {
            base.Start(target);
            TimingEvents timingEvents = new TimingEvents()
            .AddOnReset((int r) => Proc())
            .AddOnEnd(() => End());
            _timerGUID = TimingManager.StartOverTimeTimer(_intervalTime, timingEvents, _totalRepetitions);
            _damageBaseEffect = new DamageBaseEffect(_damagePerProc);
        }

        public override void Proc()
        {
            _damageBaseEffect.Start(_target);
        }
        public override void CancelEffect()
        {
            TimingManager.CancelTimer(_timerGUID);
            _damageBaseEffect.CancelEffect();
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

    public class DamageOverTimeEffectFactory : IBaseEffectFactory
    {
        float damagePerProc;
        float intervalTime;
        int totalRepetitions;

        public DamageOverTimeEffectFactory(float damagePerProc, float intervalTime, int totalRepetitions)
        {
            this.damagePerProc = damagePerProc;
            this.intervalTime = intervalTime;
            this.totalRepetitions = totalRepetitions;
        }

        public override IBaseEffect CreateEffect()
        {
            return new DamageOverTimeEffect(damagePerProc, intervalTime, totalRepetitions);
        }
    }

}