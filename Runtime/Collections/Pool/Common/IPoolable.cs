namespace Mud.Collections.Pool
{
    public interface IBasePoolable
    {
        /// <summary>
        /// Works when the object is pushed in the pool.
        /// </summary>
        void OnDespawn();
    }

    public interface IPoolable : IBasePoolable
    {
        /// <summary>
        /// Works when the object is fetched from the pool.
        /// </summary>
        void OnSpawn();
    }

    public interface IPoolable<Parametr> : IBasePoolable
    {
        /// <summary>
        /// Works when the object is fetched from the pool.
        /// </summary>
        void OnSpawn(Parametr p);
    }
}
