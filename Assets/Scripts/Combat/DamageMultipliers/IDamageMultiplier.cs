using AVA.State;

namespace AVA.Combat
{
    public interface IDamageMultiplier
    {
        float Calculate(CharacterStateInstance attacker, CharacterStateInstance defender);
    }
}
