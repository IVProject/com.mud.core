namespace Mud.Collections.Pool
{
    public interface IObjectPool
    {
        int Count {get;}
        void Push(object obj);
    }

    public interface IOnlyPushPool<T> : IObjectPool where T : class
    {
        /// <summary>
        /// Put an object in the pool.
        /// </summary>
        void Push(T obj);
    }

    public interface IObjectPool<T> : IOnlyPushPool<T> where T : class
    {
        /// <summary>
        /// Get an object to the pool.
        /// </summary>
        T Fetch();
    }

    public interface IObjectPool<Parametr, T> : IOnlyPushPool<T> where T : class
    {
        /// <summary>
        /// Get an object in the pool.
        /// </summary>
        T Fetch(Parametr p);
    }
}