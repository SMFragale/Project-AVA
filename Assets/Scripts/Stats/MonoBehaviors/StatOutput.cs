using AVA.Core;
using UnityEngine;

namespace AVA.Stats
{
    /// <summary>
    /// Base class for all MonoBehaviour components that expose stat related methods. It requires a <see cref="StatsController"/> component which contains all the stat related methods.
    /// </summary>
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
