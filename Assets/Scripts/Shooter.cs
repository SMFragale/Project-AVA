using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Transform origin;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    AudioClip shootSound;

    [SerializeField]
    private float projectileSpeed = 10f;
    
    public void Shoot(Vector3 direction) {
        var projectile = Instantiate(projectilePrefab, origin.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(shootSound, origin.position);
        projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
    }
}
