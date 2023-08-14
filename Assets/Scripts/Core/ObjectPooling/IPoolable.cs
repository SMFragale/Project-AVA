namespace AVA.Core
{
    public interface IPoolable<T>
    {
        void InitializePoolable(System.Action<T> onReleaseAction);

        void ReturnToPool();

    }
}