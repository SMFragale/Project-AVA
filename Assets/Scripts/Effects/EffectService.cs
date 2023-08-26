using System.Collections.Generic;
using AVA.Combat;
using UnityEngine;
using AVA.Stats;
using AVA.State;
using AVA.Core;

namespace AVA.Effects {
    [RequireComponent(typeof(HPService))]
    [RequireComponent(typeof(CharacterModifiers))]
    [RequireComponent(typeof(CharacterState))]
    public class EffectService : MonoWaiter
    {
        private HPService _hpService => GetComponent<HPService>();
        private CharacterModifiers _characterModifiers => GetComponent<CharacterModifiers>();
        private CharacterState _characterState => GetComponent<CharacterState>();
        
        public CharacterEffectServiceReferences CESR { get; private set; }

        private readonly HashSet<IBaseEffect> _effects = new();

        void Awake()
        {
            dependencies = new() {_hpService, _characterModifiers, _characterState };
        }

        protected override void OnDependenciesReady()
        {
            Debug.Log(_hpService + " " + _characterModifiers + " " + _characterState);
            CESR = new CharacterEffectServiceReferences(_hpService, _characterModifiers, _characterState);
        }

        private void CancelEffect(IBaseEffect effect)
        {
            if(effect == null)
            {
                Debug.LogError("Trying to remove a null effect");
                return;
            }
            _effects.Remove(effect);
            effect.CancelEffect();
        }

        private void RemoveEffect(IBaseEffect effect)
        {
            if(effect == null)
            {
                Debug.LogError("Trying to remove a null effect");
                return;
            }
            _effects.Remove(effect);
        }

        public void AddEffect(IBaseEffectFactory effectFactory)
        {
            //Crearlo
            IBaseEffect effect = effectFactory.CreateEffect();

            if(!isReady())
            {
                Debug.LogError("Trying to add an effect to a not ready EffectService");
                return;
            }
            _effects.TryGetValue(effect, out IBaseEffect existing);

            if (existing != null) //No encontre una forma de simplificar este if nested sin matar la logica o repetir pedazos de codigo
            {
                if(existing.CompareTo(effect) == 1)
                    return;
                else
                    CancelEffect(existing);
            }
            _effects.Add(effect);
            effect.Start(CESR);
            effect.OnEnd.AddListener(() => RemoveEffect(effect));
        }

    }
}
