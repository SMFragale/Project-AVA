using UnityEngine.Events;

namespace AVA.Core
{
    public class TimingEvents
    {
        public UnityEvent OnStart { get; } = new UnityEvent();
        public UnityEvent<TickInfo> OnTick { get; } = new UnityEvent<TickInfo>();
        public UnityEvent OnEnd { get; } = new UnityEvent();
        public UnityEvent<int> OnReset { get; } = new UnityEvent<int>();
        public UnityEvent OnCancel { get; } = new UnityEvent(); //Stop as in removed from the timing manager (destroyed, canceled, etc)

        public TimingEvents AddOnStart(UnityAction action)
        {
            OnStart.AddListener(action);
            return this;
        }

        public TimingEvents AddOnTick(UnityAction<TickInfo> action)
        {
            OnTick.AddListener(action);
            return this;
        }

        public TimingEvents AddOnEnd(UnityAction action)
        {
            OnEnd.AddListener(action);
            return this;
        }

        public TimingEvents AddOnReset(UnityAction<int> action)
        {
            OnReset.AddListener(action);
            return this;
        }

        public TimingEvents AddOnCancel(UnityAction action)
        {
            OnCancel.AddListener(action);
            return this;
        }
    }

    public class TickInfo
    {
        public float ElapsedTime { get; private set; }
        public float RemainingTime { get; private set; }
        public float Progress { get; private set; }

        public TickInfo(float elapsedTime, float remainingTime, float progress)
        {
            ElapsedTime = elapsedTime;
            RemainingTime = remainingTime;
            Progress = progress;
        }
    }
}
