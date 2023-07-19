using System.Collections.Generic;
using AVA.Stats;

[System.Serializable]
public class SerializableModifierContainer
{
    public SerializableModifier[] serializableModifiers;

    public Dictionary<StatType, Modifier> GenerateModifiers() {
        Dictionary<StatType, Modifier> result = new Dictionary<StatType, Modifier>();
        foreach (SerializableModifier modifier in serializableModifiers) {

            StatType type = StatType.enumToType[modifier.type];

            result.Add(type, new Modifier(type, modifier.modifier, modifier.isPercentual));
        }
        return result;
    }
}

[System.Serializable]
public class SerializableModifier
{
    public StatType.Enum type;
    public float modifier;
    public bool isPercentual;
    

}
