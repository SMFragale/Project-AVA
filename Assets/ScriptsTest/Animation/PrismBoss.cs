using DG.Tweening;
using UnityEngine;

public class PrismBoss : MonoBehaviour
{
    [SerializeField]
    private Transform body;

    [SerializeField]
    private Transform leftArm;

    [SerializeField]
    private Transform rightArm;

    [SerializeField]
    private Transform lookAt;

    private Tween idle;
    private Tween attack;
    private float duration = 10f;

    private Vector3 startPosition;
    private Vector3 startRotation;

    private void Start()
    {
        idle = Idle();
        attack = Attack();
        startPosition = transform.position;
        startRotation = transform.rotation.eulerAngles;
    }

    public void ToggleIdleAnimation()
    {
        if (idle.IsPlaying())
        {
            idle.Pause();
            ResetPosition();
        }
        else
        {
            idle.Play();
        }
    }

    public void ResetRotation()
    {
        body.DORotate(startRotation, 1f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutCirc);
        leftArm.DORotate(startRotation, 1f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutExpo);
        rightArm.DORotate(startRotation, 1f, RotateMode.WorldAxisAdd).SetEase(Ease.InOutExpo);
    }

    public void ResetPosition()
    {
        transform.DOMove(startPosition, 1f).SetEase(Ease.Linear);
    }

    public void TriggerAttackAnimation(float duration)
    {
        this.duration = duration;
        if (!attack.IsPlaying())
            attack.Restart();
    }

    private Tween Attack()
    {
        var leftArmRotation = leftArm.DORotate(new Vector3(0, 0, -3600), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutExpo);
        var rightArmRotation = rightArm.DORotate(new Vector3(0, 0, 3600), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutExpo);
        var bodyRotation = body.DORotate(new Vector3(0, 0, 720), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutCubic);
        //Join into a single tween
        return DOTween.Sequence().Join(leftArmRotation).Join(rightArmRotation).Join(bodyRotation).Pause().SetAutoKill(false);
    }

    public Tween Idle()
    {
        //Make a looping hover animation using the default transform and DOTween
        return transform.DOMoveY(transform.position.y - 0.2f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
