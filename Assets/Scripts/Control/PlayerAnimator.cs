using UnityEngine;

namespace AVA.Control
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        public void UpdateAnimation(Vector2 direction)
        {
            animator.SetFloat("InputX", direction.x);
            animator.SetFloat("InputY", direction.y);
        }
    }

}
