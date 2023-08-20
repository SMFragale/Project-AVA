using System.Collections.Generic;
using AVA.Combat;
using UnityEngine;

[RequireComponent(typeof(CombatTarget))]
public class EffectService : MonoBehaviour
{
    private readonly HashSet<IBaseEffect> _effects = new();
    private CombatTarget _target => GetComponent<CombatTarget>();

    private void RemoveEffect(IBaseEffect effect)
    {
        _effects.Remove(effect);
        //TODO toca asegurarse que el efecto desaparezca por completo
    }

    public void AddEffect(IBaseEffect effect)
    {
        IBaseEffect existing;
        _effects.TryGetValue(effect, out existing);
        var comparison = existing?.CompareTo(effect);
        if (comparison.HasValue && comparison.Value == 1) // 1 -> existing is > than new effect
            return;
        else
            RemoveEffect(existing);

        _effects.Add(effect);
        effect.Start(_target);
    }

    private void FixedUpdate()
    {
        Debug.Log($"Effects count: {_effects.Count}");
    }
}
