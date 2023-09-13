using AVA.State;
using UnityEngine;

namespace AVA.Combat
{
    /// <summary>
    /// Base class for weapons
    /// </summary>

    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 10)]
        protected float baseAttackRate = 0.5f;

        [SerializeField]
        [Range(0, 10000)]
        protected float baseAttackDamage = 1f;

        public float BaseAttackRate { get => baseAttackRate; }
        public float BaseAttackDamage { get => baseAttackDamage; }
        /// <summary>
        /// The attack method of the weapon
        /// </summary>
        /// <param name="direction">The direction in which to attack</param>
        /// <param name="characterState"> The reference to get the state of the character in the moment of the attack</param>
        public abstract void Attack(Vector3 direction, CharacterState characterState);

    }
}