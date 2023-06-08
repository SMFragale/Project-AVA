using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshMover))]
[RequireComponent(typeof(ConstantShooter))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputAction moveAction;
    [SerializeField]
    private InputAction lookAction;

    [SerializeField]
    private float rotationSpeed = 10f;

    private ConstantShooter constantShooter;

    private void OnEnable() {
        moveAction.Enable();
        lookAction.Enable();
    }

    private void Start() {
        constantShooter = GetComponent<ConstantShooter>();
        Debug.Log(constantShooter);
        StartCoroutine(constantShooter.StartShooting());
    }

    private void Update() {
        var movement = moveAction.ReadValue<Vector2>();
        var look = lookAction.ReadValue<Vector2>();

        LookTowards(look);
        MoveTowards(movement);

        Debug.DrawLine(transform.position, transform.position + new Vector3(movement.x, 0, movement.y), Color.red);
        Debug.DrawLine(transform.position, transform.position + new Vector3(look.x, 0, look.y), Color.blue);
    }
 
    private void LookTowards(Vector2 look)
    {
        var targetDirection = new Vector3(look.x, 0, look.y);
        var rotationAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
        
        transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime * rotationSpeed);
    }

    private void MoveTowards(Vector2 movement)
    {
        //Using the main camera might be an issue if we have multiple cameras
        var cameraTransform = Camera.main.transform;
        Vector3 direction = cameraTransform.forward * movement.y + cameraTransform.right * movement.x;
        direction = Vector3.ProjectOnPlane(direction, Vector3.up);

        GetComponent<NavMeshMover>().MoveTo(transform.position + direction.normalized);
    }

}
