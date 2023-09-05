using UnityEngine;
using AVA.Combat;
using AVA.Movement;
using AVA.State;
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
    /// <item><see cref="MovementService"/></item>
    /// <item><see cref="PlayerInput"/></item>
    /// <item><see cref="PlayerAnimator"/></item>
    /// <item><see cref="CharacterState"/></item>
    /// </list>
    /// </summary>
    [RequireComponent(typeof(MovementService))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(CharacterState))]
    public class PlayerController : MonoWaiter
    {
        [Space(10)]
        [Header("Movement modifiers")]
        [SerializeField]
        private float rotationSpeed = 10f;

        [SerializeField]
        private float dashSpeed = 100f;

        [SerializeField]
        private float dashDistance = 5f;

        [SerializeField]
        private float dashCooldown = 5f;

        private Cooldown dashCooldownTimer;

        [Header("Combat")]
        [SerializeField]
        public Weapon weapon;
        protected bool isAttacking = true;

        private MovementService movementService { get => GetComponent<MovementService>(); }

        private PlayerAnimator playerAnimator { get => GetComponent<PlayerAnimator>(); }

        private PlayerInput playerInput { get => GetComponent<PlayerInput>(); }

        private CharacterState characterState { get => GetComponent<CharacterState>(); }


        private void Awake()
        {
            dependencies = new List<IReadyCheck> { characterState };
        }

        protected override void OnDependenciesReady()
        {
            StartCoroutine(StartAttacking());
            playerInput.SubscribeToDashEvent(DashTowardsMoveDirection);
            dashCooldownTimer = new Cooldown(dashCooldown);
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
            if (!movementService.IsDashing)
                movementService.SetNavSpeed(characterState.GetStateInstance().stats[Stats.StatType.Speed]);
            //TODO Update speed for the animation as well so the character's speed doesn't look out of place
            RotateView(playerInput.ReadLookInput());

            var moveInput = playerInput.ReadMoveInput();

            playerAnimator.UpdateAnimation(moveInput);
            playerAnimator.CharacterSpeed = movementService.GetNavVelocity().magnitude * 0.6f;

            if (!movementService.IsDashing)
                MoveTowards(moveInput);

            UpdateDash();
        }

        private void MoveTowards(Vector2 movement)
        {
            //Using the main camera might be an issue if we have multiple cameras
            var cameraTransform = Camera.main.transform;
            Vector3 direction = cameraTransform.forward * movement.y + cameraTransform.right * movement.x;
            direction = Vector3.ProjectOnPlane(direction, Vector3.up);

            movementService.MoveTo(transform.position + direction.normalized);
        }

        private void DashTowardsMoveDirection()
        {
            if (!dashCooldownTimer.IsReady)
                return;
            var input = playerInput.ReadMoveInput();
            var direction = new Vector3(input.x, 0, input.y).normalized;

            //make this direction relative to the camera
            var relativeDirection = Camera.main.transform.TransformDirection(direction);
            movementService.DashTowards(relativeDirection, dashDistance, dashSpeed * characterState.GetStateInstance().stats[Stats.StatType.Speed]);
            dashCooldownTimer.ResetCooldown();
        }

        private void UpdateDash()
        {
            if (dashCooldownTimer.IsReady)
                Debug.Log("Dash ready");
            else
                Debug.Log($"Dash on cooldown: {dashCooldownTimer.RemainingTime}");
        }

        internal void RotateView(Vector2 distance)
        {
            transform.Rotate(Vector3.up, distance.x * rotationSpeed);
        }
    }
}