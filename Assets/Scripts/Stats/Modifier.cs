namespace AVA.Stats
{

    /// <summary>
    /// Represents a single stat modifier. The modifier can be either percentual or additive.
    /// </summary>
    /// <remarks> If a modifier is percentual, the  </remarks>
    public class Modifier
    {
        public StatType type { get; private set; }
        public float modifier { get; private set; }
        public bool isPercentual { get; private set; }

        public Modifier(StatType type, float modifier, bool isPercentual)
        {
            this.type = type;
            this.modifier = modifier;
            this.isPercentual = isPercentual;
        }
    }
}
