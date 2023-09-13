using AVA.State;

namespace AVA.Combat
{
    /// <summary>
    /// Interface for damage multipliers. It calculates the multiplier based on the attcacker and defender stats.
    /// </summary>
    public interface IDamageMultiplier
    {
        /// <summary>
        /// Calculates the damage multiplier
        /// </summary>
        /// <param name="attacker">The attacker's <see cref="AVA.State.CharacterStateInstance">CharacterStateInstance</see></param>
        /// <param name="defender">The defender's <see cref="AVA.State.CharacterStateInstance">CharacterStateInstance</see></param>
        float Calculate(CharacterStateInstance attacker, CharacterStateInstance defender);
    }
}
