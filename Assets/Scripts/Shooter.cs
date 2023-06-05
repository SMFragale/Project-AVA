using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Transform origin;

    [SerializeField]
    GameObject projectilePrefab;


    
    public void Shoot(Vector3 direction) {
        //In the future this should be done within a pool
        var projectile = Instantiate(projectilePrefab, origin.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().ShootProjectile(direction);
        Destroy(projectile, 5f);
    }
}
