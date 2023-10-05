using UnityEngine;
using AVA.Core;
public class CooldownTester : MonoBehaviour
{
    
    [SerializeField] private CooldownButton dashButton;
    [SerializeField] private float dashCooldownTime;
    private Cooldown dashCooldownTimer;
    
    void Start()
    {
        UpdateCooldownTimer();
    }

    public void UpdateCooldownTimer()
    {
        dashCooldownTimer = new Cooldown(dashCooldownTime);
        dashButton.SetCooldown(dashCooldownTime);
        dashCooldownTimer.SubscribeOnTick(dashButton.UpdateCooldown);
    }

    public void StartCooldown()
    {
        
        if(dashCooldownTimer.ResetCooldown())
        {   
            dashButton.SetCooldown(dashCooldownTimer.CooldownTime);
            Debug.Log("Cooldown started");
        }
        else
            Debug.Log("Cooldown already running");
    }
}
