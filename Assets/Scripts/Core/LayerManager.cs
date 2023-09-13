using UnityEngine;

namespace AVA.Core
{
    public static class LayerManager
    {
        public static LayerMask PierceLayer { get; private set; } = LayerMask.GetMask("Player", "Enemy");
        public static LayerMask EnvironmentLayer { get; private set; } = LayerMask.GetMask("Environment");

        public static LayerMask GroundLayer { get; private set; } = LayerMask.GetMask("Ground");

        public static bool IsInLayerMask(LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}