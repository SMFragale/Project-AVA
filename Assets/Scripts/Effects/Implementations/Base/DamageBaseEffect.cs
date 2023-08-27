using UnityEngine;
using UnityEngine.Events;

namespace AVA.Effects {
    public class DamageBaseEffect : IBaseEffect
    {
        public override string Key => "DamageBaseEffect";
        private float _damageAmount;

        private UnityAction<float> _onDamageAction;

        public DamageBaseEffect(float damageAmount, UnityAction<float> onDamageAction = null)
        {
            _damageAmount = damageAmount;
            _onDamageAction = onDamageAction;
        }
        
        public override void Start(CharacterEffectServiceReferences target)
        {
            base.Start(target);
            Proc();
        }

        public override void Proc()
        {
            _target.HPService.TakeDamage(_damageAmount);
            _onDamageAction?.Invoke(_damageAmount);
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

        public override void CancelEffect()
        {
        }
    }

    public class DamageBaseEffectFactory : IBaseEffectFactory
    {
        float damageAmount;
        UnityAction<float> onDamageAction;

        public DamageBaseEffectFactory(float damageAmount, UnityAction<float> onDamageAction = null)
        {
            this.damageAmount = damageAmount;
            this.onDamageAction = onDamageAction;
        }

        public override IBaseEffect CreateEffect()
        {
            return new DamageBaseEffect(damageAmount, onDamageAction);
        }
    }
}
