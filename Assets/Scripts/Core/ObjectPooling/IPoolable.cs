namespace AVA.Core
{
    public interface IPoolable<T>
    {
        
        public PoolContainerType PoolType { get; protected set; }
        void InitializePoolable(System.Action<T> onReleaseAction);

        void ReturnToPool();

    }
}