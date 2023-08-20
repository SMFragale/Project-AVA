using UnityEngine;
using AVA.Stats;
using UnityEngine.Events;
using AVA.Core;
using System.Collections.Generic;

namespace AVA.Combat
{
    /// <summary>
    /// Service that handles the health and shield of a character
    /// </summary>
    [RequireComponent(typeof(CharacterStats))]
    public class HPService : MonoWaiter
    {
        private HitPoints health;
        private HitPoints shield;

        private CharacterStats characterStats
        {
            get => GetComponent<CharacterStats>();
        }

        /// <summary>
        /// Event invoked when the health of the character reaches zero
        /// </summary> 
        public UnityEvent OnHealthZero { get; private set; } = new UnityEvent();

        private void Awake()
        {
            dependencies = new List<IReadyCheck>() { characterStats };
        }

        protected override void OnDependenciesReady()
        {
            health = new HitPoints(characterStats.GetStat(StatType.MaxHealth));
            shield = new HitPoints(0);

            characterStats.AddStatListener(StatType.MaxHealth, () =>
            {
                OnMaxHealthUpdated(characterStats.GetStat(StatType.MaxHealth));
            });
            characterStats.AddStatListener(StatType.Defense, () =>
            {
                OnMaxDefenseUpdated(characterStats.GetStat(StatType.Defense));
            });
            //Get max health from stats
        }

        /// <summary>
        /// Updates the health hitpoints when the max health is changed. Attached in OnDependenciesReady to a listener to MaxHealth changed in stats.
        /// <summary>
        /// <param name="maxValue">The new max value of the health</param>
        public void OnMaxHealthUpdated(float maxValue)
        {
            if (health.Value > maxValue)
                health.Value = maxValue;
        }

        /// <summary>
        /// Updates the shield hitpoints when the max defense is changed. Attached in OnDependenciesReady to a listener to MaxDefense changed in stats.
        /// <summary>
        /// <param name="maxValue">The new max value of the shield</param>
        public void OnMaxDefenseUpdated(float maxValue)
        {
            if (shield.Value > maxValue)
                shield.Value = maxValue;
        }

        /// <summary>
        /// Returns the health hitpoints
        /// </summary>
        /// <returns>The health hitpoints as a float</returns>
        public float GetHealth()
        {
            return health.Value;
        }

        /// <summary>
        /// Adds a listener to the health hitpoints
        /// </summary>
        /// <param name="listener">The UnityAction to execute when health changes</param>
        public void AddHealthListener(UnityAction listener)
        {
            health.AddOnChangedListener(listener);
        }

        /// <summary>
        /// Removes a listener to the health hitpoints
        /// </summary>
        /// <param name="listener">The UnityAction to remove</param>
        public void RemoveHealthListener(UnityAction listener)
        {
            health.RemoveOnChangedListener(listener);
        }

        /// <summary>
        /// Adds a listener to the shield hitpoints
        /// </summary>
        /// <param name="listener">The UnityAction to execute when shield changes</param>
        public void AddShieldListener(UnityAction listener)
        {
            shield.AddOnChangedListener(listener);
        }

        /// <summary>
        /// Removes a listener to the shield hitpoints
        /// </summary>
        /// <param name="listener">The UnityAction to remove</param>
        public void RemoveShieldListener(UnityAction listener)
        {
            shield.RemoveOnChangedListener(listener);
        }

        /// <summary>
        /// Returns the shield hitpoints
        /// </summary>
        /// <returns>The shield hitpoints as a float</returns>
        public float GetShield()
        {
            return shield.Value;
        }

        /// <summary>
        /// Takes damage from an attack instance. First takes damage from the shield and then from the health if the shield is depleted.
        /// </summary>
        /// <param name="attackInstance">The attack instance</param>
        public void TakeDamage(float value)
        {
            if (value <= 0) return;
            float remaining = value;

            if (shield.Value > 0)
            {
                if (shield.Value >= remaining)
                {
                    shield.Value -= remaining;
                    return;
                }
                remaining -= shield.Value;
                shield.Value = 0;
            }

            if (health.Value < remaining)
                health.Value = 0;
            else
                health.Value -= remaining;

            // ---
            if (health.Value <= 0)
                OnHealthZero?.Invoke();
        }

        /// <summary>
        /// Heals damage to the health hitpoints
        /// </summary>
        /// <param name="value">The value to heal</param>
        public void HealDamage(float value)
        {
            if (value <= 0) return;
            if (characterStats.GetStat(StatType.MaxHealth) < health.Value + value)
                health.Value = characterStats.GetStat(StatType.MaxHealth);
            else
                health.Value += value;
        }

        /// <summary>
        /// Heals damage to the shield hitpoints
        /// </summary>
        /// <param name="value">The value to heal</param>
        public void AddShield(float value)
        {
            if (value <= 0) return;
            if (characterStats.GetStat(StatType.Defense) < shield.Value + value)
                shield.Value = characterStats.GetStat(StatType.Defense);
            else
                shield.Value += value;
        }
    }
}
