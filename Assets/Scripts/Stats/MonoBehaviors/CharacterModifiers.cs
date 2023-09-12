using UnityEngine.Events;

namespace AVA.Stats
{

    /// <summary>
    /// This component is responsible for exposing methods that can modify in-game stats.
    /// For a component that can read stats, see <see cref="CharacterStats"/>.
    /// To add this component, the gameobject must have a <see cref="StatsController"/> component as well.
    /// </summary>
    public class CharacterModifiers : StatOutput
    {

        /// <summary>
        /// Adds a set of modifiers to the <see cref="StatService"/> instance. 
        /// </summary>
        /// <param name="mods"> A set of modifiers. This set can add multiple modifiers to multiple stats, it can be used as a single stat container or as a package</param>
        /// <remarks> Modifiers added this way can be removed with the container </remarks>
        // TODO Test functionality of removing modifiers
        public void AddModifiable(ModifierContainer mods)
        {
            statServiceInstance.AddModifiable(mods);
        }

        //TODO Test functionality of removing modifiers
        /// <summary>
        /// Removes a set of modifiers from the <see cref="StatService"/> instance. 
        /// </summary>
        /// <param name="mod"> A set of modifiers to be removed</param>
        public void RemoveModifiable(ModifierContainer mod)
        {
            statServiceInstance.RemoveModifiable(mod);
        }

        /// <summary>
        /// Clears the list of modifiers present in the <see cref="StatService"/> instance. This method essentially will reset the stats to their base values.
        /// </summary>
        public void RemoveAllModifiables()
        {
            statServiceInstance.RemoveAllModifiables();
        }

    }
}

