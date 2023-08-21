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
            var effect = new HealOverTimeEffect(combatTarget, 10, 0.5f, 5); //Total 50

            var effect2 = new DamageBaseEffect(15, combatTarget);

            var damageEvents = new TimingEvents()
            .AddOnReset((int r) => effectService.AddEffect(effect2))
            .AddOnReset((int r) => Debug.Log($"DamageOverTime reset-> Remaining Resets: {r}") );
            
            var events = new TimingEvents()
            .AddOnStart(() => effectService.AddEffect(effect))
            .AddOnStart(() => Debug.Log("Added heal effect"))
            .AddOnEnd(() => TimingManager.StartOverTimeTimer(2, damageEvents, 5))
            .AddOnEnd(() => Debug.Log("Added damage effect"));
            TimingManager.StartDelayTimer(3f, events);
        }
    }
}
