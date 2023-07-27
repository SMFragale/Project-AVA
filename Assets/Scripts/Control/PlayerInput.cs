using UnityEngine;
using UnityEngine.InputSystem;

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

    public void SubscribeToDashEvent(System.Action action)
    {
        dashAction.performed += ctx => action();
    }

    public void UnsubscribeToDashEvent(System.Action action)
    {
        dashAction.performed -= ctx => action();
    }

    public void SubscribeToUltimateEvent(System.Action action)
    {
        ultimateAction.performed += ctx => action();
    }

    public void UnsubscribeToUltimateEvent(System.Action action)
    {
        ultimateAction.performed -= ctx => action();
    }

    public Vector2 ReadLookInput()
    {
        return lookAction.ReadValue<Vector2>();
    }

    public Vector2 ReadMoveInput()
    {
        return moveAction.ReadValue<Vector2>().normalized;
    }
}
