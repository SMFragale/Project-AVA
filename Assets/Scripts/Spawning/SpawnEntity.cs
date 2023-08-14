using UnityEngine;

namespace AVA.Spawning
{
    [System.Serializable]
    public class SpawnEntity
    {
        [SerializeField]
        private GameObject prefab;

        [Range(0, 100)]
        [SerializeField]
        private int priority;
        [SerializeField]
        private int currencyCost;

        public GameObject Prefab { get => prefab; }
        public int Priority { get => priority; }
        public int CurrencyCost { get => currencyCost; }

        //To string method
        public override string ToString()
        {
            return $"SpawnEntity: {prefab.name})";
        }

    }
}
