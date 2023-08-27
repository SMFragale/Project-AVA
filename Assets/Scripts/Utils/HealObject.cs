using System.Collections;
using System.Collections.Generic;
using AVA.Effects;
using UnityEngine;

public class HealObject : MonoBehaviour
{
    [SerializeField] private float healAmount = 10f;

    private HealBaseEffectFactory healBaseEffect;
    
    private Collider _collider => GetComponent<Collider>();

    private void Awake()
    {
        _collider.isTrigger = true;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HealObject OnTriggerEnter: " + other.name);
        healBaseEffect = new HealBaseEffectFactory(healAmount);
        EffectService effectService = other.GetComponent<EffectService>();
        effectService?.AddEffect(healBaseEffect);
        if(effectService != null)
            Destroy(gameObject);
    }

}
