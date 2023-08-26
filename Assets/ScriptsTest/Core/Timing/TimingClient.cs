using UnityEngine;
using AVA.Core;
using UnityEditor;
using AVA.Combat;
using AVA.Effects;
using AVA.Stats;
using AVA.State;

namespace AVA.Test.Core
{
    [RequireComponent(typeof(TimingManager))]
    public class TimingClient : MonoWaiter
    {
        [SerializeField]
        private HPService _hPService;

        [SerializeField]
        private CharacterModifiers _characterModifiers;

        [SerializeField]
        private CharacterState _characterState;

        [SerializeField]
        private EffectService _effectService;

        private CharacterEffectServiceReferences _cesr;

        void Awake()
        {
            dependencies = new() { _hPService, _characterModifiers, _characterState, _effectService };
        }

        protected override void OnDependenciesReady()
        {
            _cesr = new(_hPService, _characterModifiers, _characterState);
            TestChainTimers();
            DamageOverTimeTest();
            HealBaseEffectTest();
            AddShieldBaseEffectTest();
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
            .AddOnReset((int r) => _hPService.TakeDamage(5 * r));
            TimingManager.StartOverTimeTimer(2, events, 5);
        }

        private void HealBaseEffectTest()
        {
            _cesr.HPService.TakeDamage(60);
            var effect = new HealOverTimeEffectFactory( 10, 0.5f, 5); //Total 50

            var effect2 = new DamageBaseEffectFactory(15);

            var damageEvents = new TimingEvents()
            .AddOnReset((int r) => _effectService.AddEffect(effect2))
            .AddOnReset((int r) => Debug.Log($"DamageOverTime reset-> Remaining Resets: {r}"));
            var events = new TimingEvents()
            .AddOnStart(() => _effectService.AddEffect(effect))
            .AddOnStart(() => Debug.Log("Added heal effect"))
            .AddOnEnd(() => TimingManager.StartOverTimeTimer(2, damageEvents, 5))
            .AddOnEnd(() => Debug.Log("Added damage effect"));
            TimingManager.StartDelayTimer(3f, events);
        }

        private void AddShieldBaseEffectTest()
        {
            var effect = new AddShieldBaseEffectFactory(10);
            var events = new TimingEvents()
            .AddOnStart(() => Debug.Log("Start"))
            .AddOnReset((int resets) => _effectService.AddEffect(effect))
            .AddOnReset((int r) => Debug.Log($"Reset-> Remaining Resets: {r}"))
            .AddOnEnd(() => Debug.Log("End"));
            TimingManager.StartOverTimeTimer(2, events, 5);
        }
    }
}
