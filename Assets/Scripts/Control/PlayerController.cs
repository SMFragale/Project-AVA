using UnityEngine;
using AVA.Combat;
using AVA.Movement;
using AVA.State;
using System;

namespace AVA.Control
{
    [RequireComponent(typeof(NavMeshMover))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(CharacterState))]
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

        [Space(10)]
        [Header("Animation")]
        [SerializeField]
        private PlayerAnimator animator;

        [Header("Combat")]
        [SerializeField]
        public Weapon weapon;

        private void Start()
        {
            StartCoroutine(weapon.StartAttacking());
            playerInput = GetComponent<PlayerInput>();
            playerInput.SubscribeToDashEvent(DashTowardsMoveDirection);
            GetComponent<NavMeshMover>().SetNavSpeed(speed);
            animator = GetComponent<PlayerAnimator>();
        }

        private void Update()
        {
            var lookDelta = playerInput.ReadLookInput();
            AddLookDelta(lookDelta);

            var moveInput = playerInput.ReadMoveInput();

            animator.UpdateAnimation(moveInput);
            if (!GetComponent<NavMeshMover>().IsDashing)
                MoveTowards(moveInput);
        }

        private Vector2 CalculateAnimationVector(Vector2 moveInput)
        {
            
            return moveInput;
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

        internal void AddLookDelta(Vector2 deltaPosition)
        {
            transform.Rotate(Vector3.up, deltaPosition.x);
        }
    }
}