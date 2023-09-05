namespace AVA.Core
{
    public class Cooldown
    {
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
            .AddOnEnd(() => IsReady = true)
            .AddOnTick((TickInfo tickInfo) => RemainingTime = tickInfo.RemainingTime)
            .AddOnCancel(() => IsReady = true);
            var timer = TimingManager.StartDelayTimer(CooldownTime, timingEvents);
            return true;
        }
    }
}