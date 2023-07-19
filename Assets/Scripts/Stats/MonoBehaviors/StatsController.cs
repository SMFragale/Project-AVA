using UnityEngine;

namespace AVA.Stats
{
    public class StatsController : MonoBehaviour
    {
        [SerializeField]
        private BaseStatsSO baseStats;

        public StatService statServiceInstance { get; private set; }

        private void Awake() {
            statServiceInstance = new StatService(baseStats);
        }
    }
}