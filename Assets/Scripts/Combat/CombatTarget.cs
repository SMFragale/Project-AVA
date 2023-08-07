using UnityEngine;
using AVA.State;
using AVA.Stats;
using UnityEngine.Events;

namespace AVA.Combat
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(HPService))]
    [RequireComponent(typeof(CharacterState))]
    public class CombatTarget : MonoBehaviour
    {
        public UnityEvent<float> OnTakeDamage;

        public void TakeDamage(AttackInstance attackInstance)
        {
            var damage = CalculateDamage(attackInstance);
            var hPService = GetComponent<HPService>();
            hPService.TakeDamage(damage);
            OnTakeDamage.Invoke(damage);
        }

        private void OnTriggerEnter(Collider other)
        {
            AttackInstance instance = other.gameObject.GetComponent<Projectile>()?.attackInstance;
            if (instance != null)
            {
                TakeDamage(instance);
            }
        }

        private float CalculateDamage(AttackInstance attackInstance)
        {
            var defenderState = GetComponent<CharacterState>().GetStateInstance();
            float damage =
            attackInstance.sourceDamage
            * (attackInstance.attackerState.stats[StatType.Attack] / defenderState.stats[StatType.Defense])
            * attackInstance.multiplier.Calculate(attackInstance.attackerState, defenderState);
            return damage;
        }
    }
}

