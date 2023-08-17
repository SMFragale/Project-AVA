using UnityEngine;
using AVA.Core;
using UnityEditor;
using AVA.Combat;

namespace AVA.Test.Core
{
    [RequireComponent(typeof(TimingManager))]
    public class TimingClient : MonoWaiter
    {
        private TimingManager timingService;

        [SerializeField]
        private HPService hPService;

        void Awake()
        {
            timingService = GetComponent<TimingManager>();
            dependencies = new() { hPService };
        }

        protected override void OnDependenciesReady()
        {
            DamageOverTimeTest();
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
                    timer2 = timingService.StartOverTimeTimer(2, events2, 3);
                }
            );
            var timer = timingService.StartDelayTimer(2, events);
        }

        private void DamageOverTimeTest()
        {
            var events = new TimingEvents()
            .AddOnReset((int r) => hPService.TakeDamage(5 * r));
            timingService.StartOverTimeTimer(2, events, 5);
        }
    }
}
