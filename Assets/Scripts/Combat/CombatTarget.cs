using UnityEngine;
using AVA.State;
using AVA.Stats;
using UnityEngine.Events;

namespace AVA.Combat
{
    /// <summary>
    /// Component that makes a GameObject a target for combat
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(HPService))]
    [RequireComponent(typeof(CharacterState))]
    public class CombatTarget : MonoBehaviour
    {
        public UnityEvent<float> OnTakeDamage;
        public CharacterStateInstance StateInstance => GetComponent<CharacterState>().GetStateInstance();

        /// <summary>
        /// Takes damage from an attack instance
        /// </summary>
        /// <param name="attackInstance">The attack instance</param>
        public float TakeDamage(AttackInstance attackInstance)
        {
            var damage = CalculateDamage(attackInstance);
            var hPService = GetComponent<HPService>();
            hPService.TakeDamage(damage);
            OnTakeDamage.Invoke(damage);
            return damage;
        }

        public void HealDamage(float amount)
        {
            var hPService = GetComponent<HPService>();
            hPService.HealDamage(amount);
        }

        public void AddShield(float amount)
        {
            var hPService = GetComponent<HPService>();
            hPService.AddShield(amount);
        }

        private void OnTriggerEnter(Collider other) //TODO this collision handling should be handled by the projectile, not the target. What if the projectile is a raycast? What if the damage intended is not coming from a projectile?
        {
            AttackInstance instance = other.gameObject.GetComponent<Projectile>()?.attackInstance;
            if (instance != null)
            {
                TakeDamage(instance);
            }
        }

        /// <summary>
        /// Calculates the damage of an attack instance
        /// </summary>
        /// <param name="attackInstance">The attack instance</param>
        /// <returns>The damage of the attack instance</returns>
        private float CalculateDamage(AttackInstance attackInstance)
        {
            var defenderState = StateInstance;
            float damage =
            attackInstance.sourceDamage
            * (attackInstance.attackerState.stats[StatType.Attack] / defenderState.stats[StatType.Defense]) //TODO check this formula -> Esta linea deberia parte del multiplier.calculate
            * attackInstance.multiplier.Calculate(attackInstance.attackerState, defenderState);
            return damage;
        }
    }
}

