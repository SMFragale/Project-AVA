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
        if(effect == null)
        {
            Debug.LogError("Trying to remove a null effect");
            return;
        }
        _effects.Remove(effect);
        effect.DisposeSelf();
    }

    public void AddEffect(IBaseEffect effect)
    {
        _effects.TryGetValue(effect, out IBaseEffect existing);

        if (existing != null) //No encontre una forma de simplificar este if nested sin matar la logica o repetir pedazos de codigo
        {
            if(existing.CompareTo(effect) == 1)
                return;
            else
                RemoveEffect(existing);
        }
        _effects.Add(effect);
        effect.Start(_target);
    }

}
