using UnityEngine;

namespace AVA.Control
{
    /// <summary>
    /// Class that handles the animation of the player
    /// </summary>
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        /// <summary>
        /// Updates the animation variables of the player:
        /// <list type="bullet">
        /// <item>InputX</item>
        /// <item>InputY</item>
        /// </list>
        /// </summary>
        /// <param name="direction">The direction in which the player is moving</param>
        public void UpdateAnimation(Vector2 direction)
        {
            animator.SetFloat("InputX", direction.x);
            animator.SetFloat("InputY", direction.y);
        }
    }

}
