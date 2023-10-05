using System;

namespace AVA.Core
{
    public class Cooldown
    {
        private Action _onStart;
        private Action _onEnd;
        private Action<TickInfo> _onTick;


        public float CooldownTime { get; set; }

        public float RemainingTime { get; private set; }

        public bool IsReady { get; set; } = true;

        public Cooldown(float cooldownTime)
        {
            CooldownTime = cooldownTime;
        }

        public bool ResetCooldown()
        {
            if (!IsReady)
                return false;
            TimingEvents timingEvents = new TimingEvents()
            .AddOnStart(() => IsReady = false)
            .AddOnStart(() => _onStart?.Invoke())
            .AddOnEnd(() => IsReady = true)
            .AddOnEnd(() => _onEnd?.Invoke())
            .AddOnTick((TickInfo tickInfo) => RemainingTime = tickInfo.RemainingTime)
            .AddOnTick((TickInfo tickInfo) => _onTick?.Invoke(tickInfo))
            .AddOnCancel(() => IsReady = true);
            var timer = TimingManager.StartDelayTimer(CooldownTime, timingEvents);
            return true;
        }

        public void SubscribeOnStart(Action action)
        {
            _onStart += action;
        }

        public void SubscribeOnEnd(Action action)
        {
            _onEnd += action;
        }
        public void SubscribeOnTick(Action<TickInfo> action)
        {
            _onTick += action;
        }
    }
}