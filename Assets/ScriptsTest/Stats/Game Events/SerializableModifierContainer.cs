using System.Collections.Generic;
using AVA.Stats;

[System.Serializable]
public class SerializableModifierContainer
{
    public SerializableModifier[] serializableModifiers;

    public Dictionary<StatType, Modifier> GenerateModifiers() {
        Dictionary<StatType, Modifier> result = new Dictionary<StatType, Modifier>();
        foreach (SerializableModifier modifier in serializableModifiers) {
            result.Add(modifier.type, new Modifier(modifier.type, modifier.modifier, modifier.isPercentual));
        }
        return result;
    }
}

[System.Serializable]
public class SerializableModifier : Modifier
{
    public SerializableModifier(StatType type, float modifier, bool isPercentual) : base(type, modifier, isPercentual)
    {
    }
}
