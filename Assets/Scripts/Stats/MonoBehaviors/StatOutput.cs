using UnityEngine;

namespace AVA.Stats
{
    [RequireComponent(typeof(StatsController))]
    public class StatOutput : MonoBehaviour, IReadyCheck
    {
        protected StatService statServiceInstance;

        public bool isReady()
        {
            return statServiceInstance != null;
        }

        public void Start()
        {
            statServiceInstance = GetComponent<StatsController>().statServiceInstance;
        }
    }
}
