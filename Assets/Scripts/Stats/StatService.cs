using System.Collections.Generic;

namespace AVA.Stats
{
    public class StatService
    {
        private List<ModifierContainer> modifierContainers;

        private BaseStats baseStats;

        public StatService(BaseStatsSO baseStats)
        {
            this.baseStats = new BaseStats(baseStats);
            this.modifierContainers = new List<ModifierContainer>();
        }

        public float CalculateStat(StatType type)
        {
            float baseValue = baseStats.GetStat(type);
            float finalValue = baseValue;

            foreach (ModifierContainer mc in modifierContainers)
            {
                Modifier mod = mc.GetModifierByType(type);
                if (mod == null)
                    continue;
                if (mod.isPercentual)
                {
                    finalValue += baseValue * mod.modifier;
                }
                else
                {
                    finalValue += mod.modifier;
                }
            }
            return finalValue;
        }

        public float GetBaseStat(StatType type)
        {
            return baseStats.GetStat(type);
        }

        public List<StatType> GetStatTypes()
        {
            return baseStats.GetStatTypes();
        }

        public void AddModifiable(ModifierContainer mod)
        {
            modifierContainers.Add(mod);
        }

        public void RemoveModifiable(ModifierContainer mod)
        {
            modifierContainers.Remove(mod);
        }

        public void UpdateBaseStat(StatType type, float value)
        {
            baseStats.SetStat(type, value);
        }

    }
}
