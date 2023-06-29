using UnityEngine;
using AVA.Stats;
using System.Collections.Generic;

namespace AVA.Tests.Stats {
    public class TestModifiers : MonoBehaviour
    {
        [SerializeField]
        private CharacterModifiers characterModifiers;

        private List<ModifierContainer> modifiers = new List<ModifierContainer>();

        public void AddModifiable(SerializableModifierContainer mods)
        {
            var modifiable = new ModifierContainer(mods.GenerateModifiers());
            characterModifiers.AddModifiable(modifiable);
            modifiers.Add(modifiable);
        }

        public void RemoveAllModifiers() {
            foreach (ModifierContainer mod in modifiers)
            {
                characterModifiers.RemoveModifiable(mod);
            }
            modifiers.Clear();
        }
    }
}

