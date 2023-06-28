using UnityEngine;
using AVA.Stats;
using System.Collections;
using AVA.Core;
using UnityEngine.Events;

namespace AVA.State {

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
            //Get max health from stats
        }

        public bool isReady()
        {
            return ready;
        }

        private IEnumerator Init() {
            Debug.Log("Waiting for character stats");
            yield return new WaitUntil(() => characterStats.isReady());
            health = new HitPoints(characterStats.GetStat(StatType.MaxHealth));
            shield = new HitPoints(0);

            health.AddOnChangedListener(CheckHealthAmount);
            shield.AddOnChangedListener(CheckShieldAmount);
            ready = true;
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

        public ObservableValue<float> GetObservableHealth()
        {
            return health;
        }

        public ObservableValue<float> GetObservableShield()
        {
            return shield;
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
            //TODO check si queda > maxHealth, si si dejarlo = maxHealth
            health.Value += value;
        }

        public void AddShield(float value)
        {
            shield.Value += value;
        }

        
    }
}
