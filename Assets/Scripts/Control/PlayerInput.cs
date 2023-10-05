using UnityEngine;
using UnityEngine.InputSystem;
namespace AVA.Control
{
    /// <summary>
    /// Class that handles the input of the player
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        [Header("Input actions")]
        [SerializeField]
        private InputAction moveAction;
        [SerializeField]
        private InputAction lookAction;

        [SerializeField]
        private InputAction dashAction;

        [SerializeField]
        private InputAction ultimateAction;


        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            dashAction.Enable();
            ultimateAction.Enable();
        }

        /// <summary>
        /// Adds a listener to the dash input
        /// </summary>
        /// <param name="action">The System.Action to be performed when the dash input is triggered</param>
        public void SubscribeToDashEvent(System.Action action)
        {
            dashAction.performed += ctx => action();
        }

        /// <summary>
        /// Removes a listener to the dash input
        /// </summary>
        /// <param name="action">The System.Action to be removed from the dash input</param>
        public void UnsubscribeToDashEvent(System.Action action)
        {
            dashAction.performed -= ctx => action();
        }

        /// <summary>
        /// Adds a listener to the ultimate input
        /// </summary>
        /// <param name="action">The System.Action to be performed when the ultimate input is triggered</param>
        public void SubscribeToUltimateEvent(System.Action action)
        {
            ultimateAction.performed += ctx => action();
        }

        /// <summary>
        /// Removes a listener to the ultimate input
        /// </summary>
        /// <param name="action">The System.Action to be removed from the ultimate input</param>
        public void UnsubscribeToUltimateEvent(System.Action action)
        {
            ultimateAction.performed -= ctx => action();
        }

        /// <summary>
        /// Returns the input of the look action
        /// </summary>
        /// <returns>The input of the look action as a Vector2</returns>
        public Vector2 ReadLookInput()
        {
            return lookAction.ReadValue<Vector2>();
        }

        /// <summary>
        /// Returns the input of the move action
        /// </summary>
        /// <returns>The input of the move action as a Vector2</returns>
        public Vector2 ReadMoveInput()
        {
            return moveAction.ReadValue<Vector2>().normalized;
        }
    }
}