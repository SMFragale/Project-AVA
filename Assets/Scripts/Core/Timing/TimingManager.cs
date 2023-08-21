using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace AVA.Core
{
    public class TimingManager : MonoBehaviour
    {
        [SerializeField] private static readonly List<ITimer> timers = new List<ITimer>();
        public static UnityEvent OnTick { get; private set; } = new UnityEvent();

        private float timer = 0f;

        public static GUID StartDelayTimer(float delay, TimingEvents timingEvents)
        {
            var timer = new Timer(delay, timingEvents);
            StartTimer(timer);
            return timer.ID;
        }

        public static GUID StartOverTimeTimer(float duration, TimingEvents timingEvents, int numberOfResets)
        {
            var timer = new ResetableTimer(duration, timingEvents, numberOfResets);
            StartTimer(timer);
            return timer.ID;
        }

        private static void StartTimer(ITimer timer)
        {
            timer.Start();
            timers.Add(timer);
        }

        public static void CancelTimer(GUID timerGUID)
        {
            if(timerGUID == null) return;
            
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                var timer = timers[i];
                if (timer.ID == timerGUID)
                {
                    timer.Cancel();
                    timers.RemoveAt(i);
                    return;
                }
            }
            Debug.LogWarning($"Trying to stop a timer with GUID {timerGUID} but no timer with that GUID was found.");
        }

        private void Tick(float deltaTime)
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