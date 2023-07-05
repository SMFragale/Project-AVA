using UnityEngine;
using AVA.Stats;
using System.Collections;
using UnityEngine.Events;

namespace AVA.Combat {

    [RequireComponent(typeof(CharacterStats))]
    public class HPService : MonoBehaviour, IReadyCheck
    {
        private HitPoints health;
        private HitPoints shield;

        private CharacterStats characterStats;

        public UnityEvent OnHealthZero { get; private set;} = new UnityEvent();

        bool ready = false;

        // Start is called before the first frame update
        void Start()
        {
            characterStats = GetComponent<CharacterStats>();
            StartCoroutine(Init());
        }

        public bool isReady()
        {
            return ready;
        }

        public void OnMaxHealthUpdated(float maxValue) {
            if(health.Value > maxValue)
                health.Value = maxValue;
        }

        public void OnMaxDefenseUpdated(float maxValue) {
            if(shield.Value > maxValue)
                shield.Value = maxValue;
        }

        private IEnumerator Init() {
            Debug.Log("Waiting for character stats");
            yield return new WaitUntil(() => characterStats.isReady());
            health = new HitPoints(characterStats.GetStat(StatType.MaxHealth));
            shield = new HitPoints(0);

            health.AddOnChangedListener(CheckHealthAmount);
            shield.AddOnChangedListener(CheckShieldAmount);
            ready = true;

            characterStats.AddStatListener(StatType.MaxHealth, () =>
            {
                OnMaxHealthUpdated(characterStats.GetStat(StatType.MaxHealth));
            });
            characterStats.AddStatListener(StatType.Defense, () => 
            {
                OnMaxDefenseUpdated(characterStats.GetStat(StatType.Defense));
            });
            //Get max health from stats

            Debug.Log("Ready");
        }

        public void CheckHealthAmount() {
            Debug.Log("Health amount: " + health.Value);
        }

        public void CheckShieldAmount() {
            Debug.Log("Shield amount: " + shield.Value);
        }

        public float GetHealth()
        {
            return health.Value;
        }

        public void AddHealthListener(UnityAction listener)
        {
            health.AddOnChangedListener(listener);
        }

        public void RemoveHealthListener(UnityAction listener)
        {
            health.RemoveOnChangedListener(listener);
        }

        public void AddShieldListener(UnityAction listener)
        {
            shield.AddOnChangedListener(listener);
        }

        public void RemoveShieldListener(UnityAction listener)
        {
            shield.RemoveOnChangedListener(listener);
        }

        public float GetShield()
        {
            return shield.Value;
        }

        public void TakeDamage(float value)
        {
            float remaining = value;

            if(shield.Value > 0)
            {
                if(shield.Value >= remaining)
                {
                    shield.Value -= remaining;
                    return;
                }
                remaining -= shield.Value;
                shield.Value = 0;
            }   

            if(health.Value < remaining)
                health.Value = 0;
            else
                health.Value -= remaining;

            // ---
            if(health.Value <= 0)
                OnHealthZero?.Invoke();
        }

        public void HealDamage(float value)
        {
            if (characterStats.GetStat(StatType.MaxHealth) < health.Value + value)
                health.Value = characterStats.GetStat(StatType.MaxHealth);
            else
                health.Value += value;
        }

        public void AddShield(float value)
        {
            if (characterStats.GetStat(StatType.Defense) < shield.Value + value)
                shield.Value = characterStats.GetStat(StatType.Defense);
            else
                shield.Value += value;
        }

        
    }
}
