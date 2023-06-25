using UnityEngine;

namespace AVA.Stats
{
    [RequireComponent(typeof(StatsController))]
    public class StatOutput : MonoBehaviour
    {
        protected StatService statServiceInstance;
        public bool isReady { get { return statServiceInstance != null; } }

        public void Start()
        {
            statServiceInstance = GetComponent<StatsController>().statServiceInstance;
        }
    }
}
