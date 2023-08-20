using UnityEngine;

namespace AVA.Stats
{
    /// <summary>
    /// This component is responsible receiving the <see cref="BaseStatsSO"/> from the editor and holding the <see cref="StatService"/> instance.
    /// </summary>
    public class StatsController : MonoBehaviour
    {
        [SerializeField]
        private BaseStatsSO baseStats;

        public StatService statServiceInstance { get; private set; }

        private void Awake()
        {
            statServiceInstance = new StatService(baseStats);
        }
    }
}