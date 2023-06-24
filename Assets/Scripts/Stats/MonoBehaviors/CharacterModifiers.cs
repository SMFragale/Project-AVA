using UnityEngine;

namespace AVA.Stats {
    public class CharacterModifiers : MonoBehaviour
    {
        private StatService statServiceInstance;

        void AddModifiable(ModifierContainer mods)
        {
            statServiceInstance.AddModifiable(mods);
        }

        void RemoveModifiable(ModifierContainer mod)            
        {
            statServiceInstance.RemoveModifiable(mod);
        }
    }
}

