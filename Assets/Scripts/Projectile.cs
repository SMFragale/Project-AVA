using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Shootable")) {
            Destroy(gameObject);
        }
    }
}
