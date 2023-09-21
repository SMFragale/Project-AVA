using AVA.State;
using UnityEngine;
using UnityEngine.Events;

namespace AVA.Combat
{
    public abstract class RangeWeapon : Weapon
    {
        [SerializeField]
        protected Transform origin;

        public Vector3 Origin => origin.position;

        public UnityEvent OnShoot = new();

        public override void Attack(Vector3 direction, CharacterState characterState)
        {
            Shoot(direction, characterState);
            OnShoot?.Invoke();
        }

        public abstract void Shoot(Vector3 direction, CharacterState characterState);
    }
}