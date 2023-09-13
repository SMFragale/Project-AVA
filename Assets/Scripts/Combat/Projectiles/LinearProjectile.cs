
using UnityEngine;

namespace AVA.Combat
{
    /// <summary>
    /// Projectile that moves in a straight line
    /// </summary>
    public class LinearProjectile : ObjectProjectile
    {
        protected override void OnUpdate()
        {
            if (Direction != Vector3.zero)
                transform.position += projectileSpeed * Time.deltaTime * Direction;
        }

    }

}