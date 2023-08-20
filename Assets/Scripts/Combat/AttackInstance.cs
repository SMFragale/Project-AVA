using AVA.State;

namespace AVA.Combat
{
    /// <summary>
    /// Class that holds the information of an attack
    /// </summary>
    public class AttackInstance
    {
        public CharacterStateInstance attackerState { get; private set; }
        public float sourceDamage { get; private set; }
        public IDamageMultiplier multiplier { get; private set; }

        /// <summary>
        /// Constructor for the AttackInstance class
        /// </summary>
        /// <param name="attackerState">The attacker's <see cref="AVA.State.CharacterStateInstance">CharacterStateInstance</see></param>
        /// <param name="sourceDamage">The source damage of the attack</param>
        /// <param name="multiplier">The <see cref="AVA.Combat.IDamageMultiplier">IDamageMultiplier</see> of the attack</param>
        public AttackInstance(CharacterStateInstance attackerState, float sourceDamage, IDamageMultiplier multiplier)
        {
            this.attackerState = attackerState;
            this.sourceDamage = sourceDamage;
            this.multiplier = multiplier;
        }

        /// <summary>
        /// Returns a string with the information of the attack
        /// </summary>
        /// <returns>String with the information of the attack</returns>
        public override string ToString()
        {
            return "attackerState: " + attackerState.ToString() + "Source Damage: " + sourceDamage + "Multiplier: " + multiplier.ToString();
        }

    }

}