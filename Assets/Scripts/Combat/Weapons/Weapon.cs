using AVA.State;
using UnityEngine;

namespace AVA.Combat
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]
        [Range(0, 10)]
        protected float baseAttackRate = 0.5f;

        public float BaseAttackRate { get => baseAttackRate; }

        public abstract void Attack(Vector3 direction, CharacterState characterState);

    }
}