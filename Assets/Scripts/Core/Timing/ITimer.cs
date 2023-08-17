using UnityEditor;

namespace AVA.Core
{
    public interface ITimer
    {
        GUID ID { get; }
        TimingEvents TimingEvents { get; }

        bool IsFinished { get; }
        void Start();
        void Tick(float deltaTime);
        void End();
        void Reset();
    }
}
