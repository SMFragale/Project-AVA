using System.Collections.Generic;

namespace AVA.Stats {
    public class ModifierContainer 
    {
        public Dictionary<StatType, Modifier> modifiers { get; private set; }

        public ModifierContainer(Dictionary<StatType, Modifier> modifiers)
        {
            this.modifiers = modifiers;
        }

        public Modifier GetModifierByType(StatType type)
        {
            if(modifiers.ContainsKey(type))
                return modifiers[type];
            else
                return null;
        }
    }
}
