using UnityEngine;

namespace AVA.Core
{
    public class ResetableTimer : Timer
    {
        public int _numberOfResets { get; private set; }
        private bool _infinite;
        private int _resetCount = 0;
        public override bool IsFinished => base.IsFinished && _resetCount == _numberOfResets;

        public ResetableTimer(float duration, TimingEvents timingEvents, int numberOfResets, bool infinite = false) : base(duration, timingEvents)
        {
            _numberOfResets = numberOfResets;
            _infinite = infinite;
        }
        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            if (_resetCount < _numberOfResets && base.IsFinished)
            {
                Reset();
            }
        }

        public override void Reset()
        {
            base.Reset();
            _resetCount++;
            _timingEvents.OnReset?.Invoke(_numberOfResets - _resetCount);
        }
    }
}
