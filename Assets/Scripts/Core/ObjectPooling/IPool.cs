namespace AVA.Core
{
    public interface IPool<T> 
    { 
        T Pull();
        void Push(T obj);
        
    }

}
