using UnityEditor;
using UnityEngine;

namespace AVA.Core
{
    [System.Serializable]
    public class Timer : ITimer
    {
        protected TimingEvents _timingEvents;
        public TimingEvents TimingEvents => _timingEvents;
        [SerializeField] private GUID _id;
        public GUID ID => _id;
        public virtual bool IsFinished => _elapsedTime >= _duration;
        private float _elapsedTime;
        private readonly float _duration;

        public Timer(float duration, TimingEvents timingEvents)
        {
            _id = GUID.Generate();
            _elapsedTime = 0;
            _duration = duration;
            _timingEvents = timingEvents;
        }

        public virtual void Start()
        {
            _timingEvents.OnStart?.Invoke();
        }

        public virtual void Tick(float deltaTime)
        {
            _elapsedTime += deltaTime;
            _timingEvents.OnTick?.Invoke(new TickInfo(_elapsedTime, _duration - _elapsedTime, _elapsedTime / _duration));
            if (IsFinished)
                End();
        }

        public virtual void End()
        {
            _timingEvents.OnEnd?.Invoke();
        }

        public virtual void Reset()
        {
            _elapsedTime = 0;
        }

        public virtual void Cancel()
        {
            _timingEvents.OnCancel?.Invoke();
        }
    }

}