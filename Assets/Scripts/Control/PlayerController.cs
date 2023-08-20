using UnityEngine;
using AVA.Combat;
using AVA.Movement;
using AVA.State;
using System;
using AVA.Core;
using System.Collections.Generic;
using System.Collections;

namespace AVA.Control
{
    /// <summary>
    /// Controller for the player. Comunicates with the <see cref="PlayerInput"/> and the corresponding components to make the player do an action.
    /// Responsible for: 
    /// <list type="bullet">
    /// <item>Moving the player</item>
    /// <item>Rotating the player</item>
    /// <item>Attacking</item>
    /// <item>Dashing</item>
    /// </list>
    /// Requires the following components:
    /// <list type="bullet">
    /// <item><see cref="NavMeshMover"/></item>
    /// <item><see cref="PlayerInput"/></item>
    /// <item><see cref="PlayerAnimator"/></item>
    /// <item><see cref="CharacterState"/></item>
    /// </list>
    /// </summary>
    [RequireComponent(typeof(NavMeshMover))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(CharacterState))]
    public class PlayerController : MonoWaiter
    {
        [Header("Input")]
        PlayerInput playerInput;

        [Space(10)]
        [Header("Movement modifiers")]
        [SerializeField]
        private float rotationSpeed = 10f;

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
        protected bool isAttacking = true;

        private CharacterState characterState { get => GetComponent<CharacterState>(); }

        private void Awake()
        {
            dependencies = new List<IReadyCheck> { characterState };
        }

        protected override void OnDependenciesReady()
        {
            StartCoroutine(StartAttacking());
            playerInput = GetComponent<PlayerInput>();
            playerInput.SubscribeToDashEvent(DashTowardsMoveDirection);
            animator = GetComponent<PlayerAnimator>();
        }

        public IEnumerator StartAttacking()
        {
            isAttacking = true;
            while (isAttacking)
            {
                weapon.Attack(transform.forward.normalized, characterState);
                yield return new WaitForSeconds(weapon.BaseAttackRate / characterState.GetCurrentStats()[Stats.StatType.AttackSpeed]);
            }
        }

        protected override void OnUpdate()
        {
            GetComponent<NavMeshMover>().SetNavSpeed(characterState.GetStateInstance().stats[Stats.StatType.Speed]);
            RotateView(playerInput.ReadLookInput());

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

            //make this direction relative to the camera
            var relativeDirection = Camera.main.transform.TransformDirection(direction);

            GetComponent<NavMeshMover>().DashTowards(relativeDirection, dashDistance, dashSpeed);
        }

        internal void RotateView(Vector2 distance)
        {
            transform.Rotate(Vector3.up, distance.x * rotationSpeed);
        }
    }
}