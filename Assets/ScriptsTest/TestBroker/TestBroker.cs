using UnityEngine;
using AVA.Stats;
using AVA.Combat;
using AVA.Core;
using System.Collections;
using System.Collections.Generic;

public class TestBroker : MonoBehaviour, IReadyCheck
{
    [SerializeField]
    private CharacterStats characterStats;
    [SerializeField]
    private HPService hpService;
    [SerializeField]
    private CharacterModifiers characterModifiers;
    [SerializeField]
    private int modifierCounter;

    private List<ModifierContainer> modifiers;

    private bool ready = false;

    private void Start()
    {
        // Wait for each service to be ready
        StartCoroutine(Init());
        modifiers = new List<ModifierContainer>();
        modifierCounter = 0;
    }

    private IEnumerator Init()
    {
        // Wait for each service to be ready
        yield return new WaitUntil(() => characterStats.isReady());
        yield return new WaitUntil(() => hpService.isReady());
        yield return new WaitUntil(() => characterModifiers.isReady());
        ready = true;
    }

    public bool isReady()
    {
        return ready;
    }

    public void TakeDamage(float damage)
    {
        hpService.TakeDamage(damage);
    }

    public void HealDamage(float damage)
    {
        hpService.HealDamage(damage);
    }

    public void AddShield(float value)
    {
        hpService.AddShield(value);
    }

    public void AddModifiable(SerializableModifierContainer mods)
    {
        var modifiable = new ModifierContainer(mods.GenerateModifiers());
        characterModifiers.AddModifiable(modifiable);
        modifiers.Add(modifiable);
        modifierCounter++;
    }

    public void RemoveModifierContainer(int index)
    {
        if (index >= modifierCounter || index < 0) return;
        characterModifiers.RemoveModifiable(modifiers[index]);
        modifiers.RemoveAt(index);
        modifierCounter--;
    }

    public void RemoveAllModifiers()
    {
        characterModifiers.RemoveAllModifiables();
        modifiers.Clear();
        modifierCounter = 0;
    }

}
