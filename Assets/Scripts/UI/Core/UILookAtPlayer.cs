using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UILookAtPlayer : MonoBehaviour
{
    Transform lookAtTarget;

    private void Start()
    {
        lookAtTarget = FindObjectOfType<Camera>().transform;
    }

    private void Update()
    {
        transform.forward = lookAtTarget.forward;
    }
}
