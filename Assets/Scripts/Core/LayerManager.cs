using UnityEngine;

namespace AVA.Core
{
    public static class LayerManager
    {
        public static LayerMask pierceLayer { get; private set; } = LayerMask.GetMask("Player", "Enemy");
        public static LayerMask environmentLayer { get; private set; } = LayerMask.GetMask("Environment");

        public static bool IsInLayerMask(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}