using UnityEngine;

namespace AVA.Stats
{
    [RequireComponent(typeof(StatsController))]
    public class StatOutput : MonoBehaviour, IReadyCheck
    {
        protected StatService statServiceInstance
        {
            get
            {
                return GetComponent<StatsController>().statServiceInstance;
            }
        }

        public bool isReady()
        {
            return statServiceInstance != null;
        }
    }
}
