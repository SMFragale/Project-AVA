
using System;
using AVA.Core;
using AVA.Effects;
using UnityEditor;
using UnityEngine;

public class NukeUlti : MonoBehaviour
{
    [Header("Ulti Settings")]
    [SerializeField] float timeToCast = 3;

    [Header("Instant damage settings")]
    [SerializeField] float damageRange = 5;
    [SerializeField] float intantDamage = 30;

    [Header("DoT settings")]
    [SerializeField] float dotRange = 5;
    [SerializeField] float dotTimeout = 15;
    [SerializeField] float applyDoTInterval = 0.5f;
    [SerializeField] float burnDamage = 0.25f;
    [SerializeField] float burnTimeInterval = 0.2f;
    [SerializeField] int burnCount = 100;


    GUID castTimer;
    GUID dotTimer;

    DamageBaseEffectFactory damageEffectFactory;
    DamageOverTimeEffectFactory dotEffectFactory;

    void Awake()
    {

        damageEffectFactory = new DamageBaseEffectFactory(intantDamage);
        dotEffectFactory = new DamageOverTimeEffectFactory(burnDamage, burnTimeInterval, burnCount);
    }

    public void Start()
    {
        var castEvents = new TimingEvents()
        .AddOnStart(() => Debug.Log("Start to cast Ulti"))
        .AddOnEnd(() => Debug.Log("End Cast Ulti"))
        .AddOnEnd(() =>
            {
                Cast();
            }
        );
        castTimer = TimingManager.StartDelayTimer(timeToCast, castEvents);
    }

    private void ApplyDoTEffect()
    {
        //Get all enemies in range of the dotCollider
        
        foreach (var enemy in Physics.OverlapSphere(transform.position, dotRange))
        {
            var enemyEffectService = enemy.GetComponent<EffectService>();
            if (enemyEffectService != null && enemyEffectService.gameObject != this.gameObject)
            {
                enemyEffectService.AddEffect(dotEffectFactory);
            }
        }
    }

    private void ApplyDamageEffect()
    {
        //Get all enemies in range of the damageCollider
        foreach (var enemy in Physics.OverlapSphere(transform.position, damageRange))
        {
            var enemyEffectService = enemy.GetComponent<EffectService>();
            if (enemyEffectService != null && enemyEffectService.gameObject != this.gameObject )
            {
                enemyEffectService.AddEffect(damageEffectFactory);
            }
        }
    }

    private void Cast()
    {  
        ApplyDamageEffect();

        var dotEvents = new TimingEvents()
        .AddOnStart(() => ApplyDoTEffect())
        .AddOnReset((int r) => ApplyDoTEffect())
        .AddOnEnd(() => Destroy(gameObject));


        dotTimer = TimingManager.StartOverTimeTimer(applyDoTInterval, dotEvents, (int)(dotTimeout/applyDoTInterval));

    }

    //Draw gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, dotRange);
    }
}
