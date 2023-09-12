using System.Collections.Generic;

namespace AVA.Stats
{

    /// <summary>
    /// Represents a container for <see cref="Modifier">s of a single stat.
    /// </summary>
    public class ModifierContainer
    {
        public Dictionary<StatType, Modifier> _modifiers;
        public Dictionary<StatType, Modifier> Modifiers { get => new Dictionary<StatType, Modifier>(_modifiers); }

        /// <summary>
        /// Creates a new modifier container from a dictionary of modifiers.
        /// </summary>
        /// <param name="modifiers">A dictionary of distinct modifiers</param>
        public ModifierContainer(Dictionary<StatType, Modifier> modifiers)
        {
            _modifiers = modifiers;
        }

        /// <returns>The modifier of the given type, or null if it doesn't exist.</returns>
        public Modifier GetModifierByType(StatType type)
        {
            if (_modifiers.ContainsKey(type))
                return _modifiers[type];
            else
                return null;
        }
    }
}
