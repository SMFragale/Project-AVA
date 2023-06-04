using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 offset = new Vector3(0, 3.0f, -2.5f);
    public float lerpSpeed = 1f;

    private void LateUpdate()
    {
        //Smoothing
        Vector3 desiredPosition = lookAt.position + offset;
        //desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * lerpSpeed);
    }
}