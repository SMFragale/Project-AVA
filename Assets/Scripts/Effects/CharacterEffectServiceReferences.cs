
using AVA.Combat;
using AVA.State;
using AVA.Stats;

namespace AVA.Effects {
    public class CharacterEffectServiceReferences 
    {
        public CharacterEffectServiceReferences(HPService hpService, CharacterModifiers characterModifiers, CharacterState characterState)
        {
            HPService = hpService;
            CharacterModifiers = characterModifiers;
            CharacterState = characterState;
        }
        public HPService HPService { get; private set; }
        public CharacterModifiers CharacterModifiers { get; private set; }
        public CharacterState CharacterState { get; private set; }
    }
}
