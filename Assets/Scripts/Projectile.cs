using UnityEngine;

public class Projectile : MonoBehaviour
{

    // Maybe add an explosion effect here
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Shootable")) {
            Destroy(gameObject);
        }
    }
}
