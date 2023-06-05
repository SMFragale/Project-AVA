using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class ConstantShooter : MonoBehaviour
{
    Shooter shooter;

    [SerializeField]
    [Range(0, 10)]
    private float fireRate = 0.5f;
    private bool isShooting = false;

    private void Awake() {
        isShooting = true;
    }

    private void Start() {
        shooter = GetComponent<Shooter>();
    }

    //Make a method that is a couroutine that will shoot every fireRate seconds (use WaitForSeconds) use the shooter.Shoot
    public IEnumerator StartShooting()
    {
        isShooting = true;
        while (isShooting)
        {
            shooter.Shoot(transform.forward.normalized);
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void StopShooting() {
        isShooting = false;
    }

}
