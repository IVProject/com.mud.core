using System;
using Mud.ArchitecturalPatterns;
using Mud.DesignPatterns.Factories;

namespace Mud.Collections.Pool
{
    public class ViewPool<T> : BaseObjectPool<T>, IObjectPool<T> where T : class, IPoolable<IObjectPool>, IView
    {
        public ViewPool(Func<T> onCreate, Action<T> onDestroy, PoolSettings poolSettings = default) : base(onCreate, onDestroy, poolSettings)
        {
        }

        public ViewPool(IFactory<T> creator, Action<T> onDestroy, PoolSettings poolSettings = default) : base(creator, onDestroy, poolSettings)
        {
        }

        public T Fetch()
        {
            if (_container.TryPop(out T obj) == false || obj == null)
                obj = _create();

            obj.OnSpawn(this);

            return obj;
        }
    }
}