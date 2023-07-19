using UnityEngine.Events;

namespace AVA.Stats
{
    public class CharacterModifiers : StatOutput
    {
        public void AddModifiable(ModifierContainer mods)
        {
            statServiceInstance.AddModifiable(mods);
        }

        public void RemoveModifiable(ModifierContainer mod)
        {
            statServiceInstance.RemoveModifiable(mod);
        }

        public void RemoveAllModifiables()
        {
            statServiceInstance.RemoveAllModifiables();
        }

    }
}

