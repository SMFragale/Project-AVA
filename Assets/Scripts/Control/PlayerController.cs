using UnityEngine;
using AVA.Combat;
using AVA.Movement;

namespace AVA.Control
{
    [RequireComponent(typeof(NavMeshMover))]
    [RequireComponent(typeof(IWeapon))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Input")]
        PlayerInput playerInput;

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

        [Space(10)]
        [Header("Animation")]
        [SerializeField]
        private PlayerAnimator animator;


        private void Start()
        {
            weapon = GetComponent<IWeapon>();
            StartCoroutine(weapon.StartAttacking());
            playerInput = GetComponent<PlayerInput>();
            playerInput.SubscribeToDashEvent(DashTowardsMoveDirection);
            GetComponent<NavMeshMover>().SetNavSpeed(speed);
            animator = GetComponent<PlayerAnimator>();
        }

        private void Update()
        {
            var lookDirection = playerInput.ReadLookInput();
            LookTowards(lookDirection);

            var moveInput = playerInput.ReadMoveInput();

            Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
            //if (!GetComponent<NavMeshMover>().IsDashing)
            //    MoveTowards(moveInput);


            var animationVector = transform.forward * moveInput.y + transform.right * moveInput.x;
            animationVector = Vector3.ProjectOnPlane(animationVector, Vector3.up);
            Debug.DrawLine(transform.position, transform.position + animationVector.normalized, Color.blue);
            animator.UpdateAnimation(new Vector2(animationVector.x, animationVector.z));

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
            var input = playerInput.ReadMoveInput();
            var direction = new Vector3(input.x, 0, input.y).normalized;

            GetComponent<NavMeshMover>().DashTowards(direction, dashDistance, dashSpeed);
        }

    }
}
