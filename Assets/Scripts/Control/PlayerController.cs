using UnityEngine;
using UnityEngine.InputSystem;
using AVA.Combat;
using AVA.Movement;

namespace AVA.Control
{
    [RequireComponent(typeof(NavMeshMover))]
    [RequireComponent(typeof(IWeapon))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Input actions")]
        [SerializeField]
        private InputAction moveAction;
        [SerializeField]
        private InputAction lookAction;

        [SerializeField]
        private InputAction dashAction;

        [Space(10)]
        [Header("Movement modifiers")]
        [SerializeField]
        private float rotationSpeed = 10f;

        [SerializeField]
        private float speed = 1f;
        [SerializeField]
        private float dashSpeed = 100f;
        [SerializeField]
        private float dashDistance = 5f;

        private IWeapon weapon;

        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            dashAction.Enable();
        }

        private void Start()
        {
            weapon = GetComponent<IWeapon>();
            StartCoroutine(weapon.StartAttacking());
            dashAction.performed += ctx => DashTowardsMoveDirection();
            GetComponent<NavMeshMover>().SetNavSpeed(speed);
        }

        private Vector2 ReadLookInput() {
            return lookAction.ReadValue<Vector2>();
        }

        private Vector2 ReadMoveInput() {
            return moveAction.ReadValue<Vector2>();
        }

        private void Update()
        {
            LookTowards(ReadLookInput());
            if (!GetComponent<NavMeshMover>().IsDashing)
                MoveTowards(ReadMoveInput());

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

        private void DashTowardsMoveDirection()
        {
            var direction = new Vector3(ReadMoveInput().x, 0, ReadMoveInput().y).normalized;

            GetComponent<NavMeshMover>().DashTowards(direction, dashDistance, dashSpeed);
        }

    }
}
