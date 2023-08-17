using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace AVA.Core
{
    public class TimingManager : MonoBehaviour
    {
        [SerializeField] private readonly List<ITimer> timers = new List<ITimer>();
        public UnityEvent OnTick { get; private set; } = new UnityEvent();

        private float timer = 0f;

        public GUID StartDelayTimer(float delay, TimingEvents timingEvents)
        {
            var timer = new Timer(delay, timingEvents);
            StartTimer(timer);
            return timer.ID;
        }

        public GUID StartOverTimeTimer(float duration, TimingEvents timingEvents, int numberOfResets)
        {
            var timer = new ResetableTimer(duration, timingEvents, numberOfResets);
            StartTimer(timer);
            return timer.ID;
        }

        private void StartTimer(ITimer timer)
        {
            timer.Start();
            timers.Add(timer);
        }

        public void Tick(float deltaTime)
        {
            timer = 0f;
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                var timer = timers[i];
                if (timer.IsFinished)
                {
                    timers.RemoveAt(i);
                }
                else
                    timer.Tick(deltaTime);
            }

            OnTick.Invoke();
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            Tick(Time.fixedDeltaTime);
        }
    }

}