using System;
using AVA.Combat;
using UnityEngine.Events;

public abstract class IBaseEffect : IComparable<IBaseEffect>
{
    public abstract string Key { get; }
    protected CombatTarget _target;
    protected CombatTarget _source;
    public UnityEvent OnEnd { get; private set; } = new UnityEvent();

    public void End()
    {
        OnEnd?.Invoke();
    }

    public IBaseEffect(CombatTarget source)
    {
        _source = source;
    }

    public virtual void Start(CombatTarget target)
    {
        //Debug.Log($"Starting {Key} on {target.name}");
        _target = target;
    }

    public abstract void Proc();

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is IBaseEffect effect)
        {
            return effect.Key == Key;
        }
        return false;
    }

    protected abstract int Compare(IBaseEffect other);

    public abstract void DisposeSelf();

    public int CompareTo(IBaseEffect other)
    {
        return Compare(other);
    }
}
