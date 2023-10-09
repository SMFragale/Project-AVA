using AVA.State;
using UnityEngine;

namespace AVA.Combat
{
    public class RaycastWeapon : RangeWeapon
    {
        public override void Shoot(Vector3 direction, CharacterState characterState)
        {
            if (Physics.Raycast(origin.position, direction, out RaycastHit hit))
            {
                OnProjectileHit?.Invoke(new ProjectileHitInfo(hit.collider.gameObject, hit.point, hit.normal));
            }
        }
    }
}
