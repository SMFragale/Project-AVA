using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using AVA.Combat;
using AVA.Core;

namespace AVA.Effects {
    public class HealBaseEffect : IBaseEffect
    {
        public override string Key => "HealBaseEffect";

        private float _healAmount;

        private UnityAction<float> _onHealAction;

        public HealBaseEffect(float healAmount, UnityAction<float> onHealAction = null)
        {
            
            _healAmount = healAmount;
            _onHealAction = onHealAction;
        }

        public override void Proc()
        {
            _target.HPService.HealDamage(_healAmount);
            _onHealAction?.Invoke(_healAmount);
        }

        public override void Start(CharacterEffectServiceReferences target)
        {
            base.Start(target);
            Proc();
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

        public override void CancelEffect()
        {
            
        }
    }

    public class HealBaseEffectFactory : IBaseEffectFactory
    {
        float healAmount;
        UnityAction<float> onHealAction;

        public HealBaseEffectFactory(float healAmount, UnityAction<float> onHealAction = null)
        {
            this.healAmount = healAmount;
            this.onHealAction = onHealAction;
        }

        public override IBaseEffect CreateEffect()
        {
            return new HealBaseEffect(healAmount, onHealAction);
        }
    }
}
