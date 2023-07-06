using AVA.State;

namespace AVA.Combat
{
    public class AttackInstance
    {
        public CharacterStateInstance attackerState { get; private set; }
        public float sourceDamage { get; private set; }
        public IDamageMultiplier multiplier { get; private set; }

        public AttackInstance(CharacterStateInstance attackerState, float sourceDamage, IDamageMultiplier multiplier)
        {
            this.attackerState = attackerState;
            this.sourceDamage = sourceDamage;
            this.multiplier = multiplier;
        }

        //Create ToString
        public override string ToString()
        {
            return "attackerState: " + attackerState.ToString() + "Source Damage: " + sourceDamage + "Multiplier: " + multiplier.ToString();
        }

    }

}