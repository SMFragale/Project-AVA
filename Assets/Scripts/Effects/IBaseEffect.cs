using System;
using AVA.Combat;
using UnityEngine.Events;

namespace AVA.Effects {
    
    public abstract class IBaseEffect : IComparable<IBaseEffect>
    {
        public abstract string Key { get; }
        protected CharacterEffectServiceReferences _target;
        public UnityEvent OnEnd { get; private set; } = new UnityEvent();

        public void End()
        {
            OnEnd?.Invoke();
        }

        public virtual void Start(CharacterEffectServiceReferences target)
        {
            //Debug.Log($"Starting {Key} on {target.name}");
            _target = target;
        }

        public abstract void Proc();

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is IBaseEffect effect)
            {
                return effect.Key == Key;
            }
            return false;
        }

        protected abstract int Compare(IBaseEffect other);

        public abstract void CancelEffect();

        public int CompareTo(IBaseEffect other)
        {
            return Compare(other);
        }
    }

    public abstract class IBaseEffectFactory
    {
        public abstract IBaseEffect CreateEffect();
    }

}