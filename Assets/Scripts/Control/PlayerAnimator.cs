using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void UpdateAnimation(Vector2 direction) {
        Debug.Log(direction);
        animator.SetFloat("InputX", direction.x);
        animator.SetFloat("InputY", direction.y);
    }
}
