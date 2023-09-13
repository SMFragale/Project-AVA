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
        [field: SerializeField]
        public UnityEvent<float> OnTakeDamage { get; private set; } = new();

        [field: SerializeField]
        public UnityEvent<float> OnHealDamage { get; private set; } = new();

        [field: SerializeField]
        public UnityEvent<float> OnAddShield { get; private set; } = new();

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


        //TODO Create a heal instance to calculate healing done based on stats and any other necessary means
        public void HealDamage(float amount)
        {
            var hPService = GetComponent<HPService>();
            hPService.HealDamage(amount);
            OnHealDamage.Invoke(amount);
        }

        //TODO Create a shield instance to calculate shield added based on stats and any other necessary means
        public void AddShield(float amount)
        {
            var hPService = GetComponent<HPService>();
            hPService.AddShield(amount);
            OnAddShield.Invoke(amount);
        }

        //private void OnTriggerEnter(Collider other) //TODO this collision handling should be handled by the projectile, not the target. What if the projectile is a raycast? What if the damage intended is not coming from a projectile?
        //{
        //    Projectile projectile = other.gameObject.GetComponent<Projectile>();
        //    if (projectile != null)
        //    {
        //        var instance = projectile.AttackInstance;
        //        TakeDamage(instance);
        //    }
        //}

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

