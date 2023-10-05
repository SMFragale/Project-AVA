 using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AVA.Core;
using AVA.Control;

public class CooldownButton : MonoBehaviour
{
    
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private Image _ghostCooldownImage;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private TMP_Text _cooldownText;
    [SerializeField] private Button _button;

    private bool _isOnCooldown;
    private float _cooldownTimer;

    public void SetCooldown(float cooldownTime)
    {
        _cooldownTimer = cooldownTime;
        _isOnCooldown = true;
        _button.interactable = false;
    }
    public void UpdateCooldown(TickInfo info)
    {
        if (!_isOnCooldown)
            return;
        
        _cooldownTimer = info.RemainingTime;
        _cooldownText.text = Mathf.CeilToInt(_cooldownTimer).ToString();
        _cooldownImage.fillAmount = info.Progress;
        if (_cooldownTimer <= 0)
        {
            _isOnCooldown = false;
            _button.interactable = true;
            _cooldownText.text = "";
            _cooldownImage.fillAmount = 1;
        }
    }
    public void SetIcon(Sprite icon, bool setGhost = true)
    {
        _cooldownImage.sprite = icon;
        if (setGhost)
            SetGhostIcon(icon);
    }
    public void SetBackground(Sprite background)
    {
        _buttonImage.sprite = background;
    }
    public void SetGhostIcon(Sprite ghostIcon)
    {
        _ghostCooldownImage.sprite = ghostIcon;
    }
}
