using System;
using Mud.DesignPatterns.Factories;

namespace Mud.Collections.Pool
{
    public class ObjectPool<T> : BaseObjectPool<T>, IObjectPool<T> where T: class, IPoolable
    {
        public ObjectPool(Func<T> onCreate, Action<T> onDestroy, PoolSettings poolSettings = default) : base(onCreate, onDestroy, poolSettings)
        {
        }

        public ObjectPool(IFactory<T> creator, Action<T> onDestroy, PoolSettings poolSettings = default) : base(creator, onDestroy, poolSettings)
        {
        }

        public T Fetch()
        {
            if (_container.TryPop(out T obj) == false || obj == null)
                obj = _create();
            
            obj.OnSpawn();

            return obj;
        }
    }

    public class ObjectPool<Parametr, T> : BaseObjectPool<T>, IObjectPool<Parametr, T> where T : class, IPoolable<Parametr>
    {
        public ObjectPool(Func<T> onCreate, Action<T> onDestroy, PoolSettings poolSettings = default) : base(onCreate, onDestroy, poolSettings)
        {
        }

        public ObjectPool(IFactory<T> creator, Action<T> onDestroy, PoolSettings poolSettings = default) : base(creator, onDestroy, poolSettings)
        {
        }

        public T Fetch(Parametr p)
        {
            if (_container.TryPop(out T obj) == false || obj == null)
                obj = _create();
            
            obj.OnSpawn(p);

            return obj;
        }
    }
}
