using AVA.State;
using UnityEngine;

namespace AVA.Combat
{
    /// <summary>
    /// Esto es un summary de esta clase weapon :v
    /// </summary>

    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 10)]
        protected float baseAttackRate = 0.5f;

        public float BaseAttackRate { get => baseAttackRate; }

        /// <summary>
        /// Este metodo ataca jajaj 
        /// </summary>
        /// <param name="direction">the direction in which to attack</param>
        /// <param name="characterState"> The reference to get the state of the character in the moment of attack</param>
        public abstract void Attack(Vector3 direction, CharacterState characterState);

    }
}