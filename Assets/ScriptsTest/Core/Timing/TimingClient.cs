using UnityEngine;
using AVA.Core;
using UnityEditor;
using AVA.Combat;

namespace AVA.Test.Core
{
    [RequireComponent(typeof(TimingManager))]
    public class TimingClient : MonoWaiter
    {
        [SerializeField]
        private HPService hPService;

        [SerializeField]
        private CombatTarget combatTarget;

        [SerializeField]
        private EffectService effectService;

        void Awake()
        {
            dependencies = new() { hPService };
        }

        protected override void OnDependenciesReady()
        {
            HealBaseEffectTest();
        }

        private void TestChainTimers()
        {
            GUID timer2;
            var events = new TimingEvents()
            .AddOnStart(() => Debug.Log("Start"))
            .AddOnEnd(() => Debug.Log("End"))
            .AddOnEnd(() =>
                {
                    var events2 = new TimingEvents()
                    .AddOnStart(() => Debug.Log("Start2"))
                    .AddOnReset((int remainingResets) => Debug.Log($"Reset-> Remaining Resets: {remainingResets}"));
                    timer2 = TimingManager.StartOverTimeTimer(2, events2, 3);
                }
            );
            var timer = TimingManager.StartDelayTimer(2, events);
        }

        private void DamageOverTimeTest()
        {
            var events = new TimingEvents()
            .AddOnReset((int r) => hPService.TakeDamage(5 * r));
            TimingManager.StartOverTimeTimer(2, events, 5);
        }

        private void HealBaseEffectTest()
        {
            hPService.TakeDamage(60);
            var effect = new HealBaseEffect(2, combatTarget);
            effectService.AddEffect(effect);

            var effect2 = new HealBaseEffect(10, combatTarget);

            //TODO The timers for the effect are ticking even when the effect is removed from the effects list. Add a way to remove the timers when the object is destroyed
            var events = new TimingEvents()
            .AddOnEnd(() => effectService.AddEffect(effect2))
            .AddOnEnd(() => Debug.Log("Added damage effect"));
            TimingManager.StartDelayTimer(5, events);
        }
    }
}
