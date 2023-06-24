namespace AVA.Stats {
    public class Modifier
    {
        public StatType type;
        public float modifier;
        public bool isPercentual;

        public Modifier(StatType type, float modifier, bool isPercentual)
        {
            this.type = type;
            this.modifier = modifier;
            this.isPercentual = isPercentual;
        }
    }
}
