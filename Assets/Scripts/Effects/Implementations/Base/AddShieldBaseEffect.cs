using AVA.Combat;
using UnityEngine.Events;

namespace AVA.Effects {
    public class AddShieldBaseEffect : IBaseEffect
    {
        public override string Key => "AddShieldBaseEffect";
        private float _shieldAmount;
        private UnityAction<float> _onAddShield;

        public AddShieldBaseEffect(float shieldAmount, UnityAction<float> onAddShield = null)
        {
            
            _shieldAmount = shieldAmount;
            _onAddShield = onAddShield;
        }

        public override void CancelEffect()
        {

        }

        public override void Start(CharacterEffectServiceReferences target)
        {
            base.Start(target);
            Proc();
        }

        public override void Proc()
        {
            _target.HPService.AddShield(_shieldAmount);
            _onAddShield?.Invoke(_shieldAmount);
        }

        protected override int Compare(IBaseEffect other)
        {
            if (other is AddShieldBaseEffect otherEffect)
            {
                return _shieldAmount.CompareTo(otherEffect._shieldAmount);
            }
            return -1;
        }
    }

    public class AddShieldBaseEffectFactory : IBaseEffectFactory
    {
        float shieldAmount;
        UnityAction<float> onAddShield;

        public AddShieldBaseEffectFactory(float shieldAmount, UnityAction<float> onAddShield = null)
        {
            this.shieldAmount = shieldAmount;
            this.onAddShield = onAddShield;
        }

        public override IBaseEffect CreateEffect()
        {
            return new AddShieldBaseEffect(shieldAmount, onAddShield);
        }
    }
}
