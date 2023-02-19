using System;
using System.Collections.Generic;

namespace Mud.Collections.Pool
{
    public class DynamicPool<T> : IObjectPool<T> where T : class
    {
        private Stack<T> _container;
        private Func<T> _create;
        private Action<T> _spawn;
        private Action<T> _despawn;
        
        public int Count => _container.Count;

        public DynamicPool(Func<T> onCreate, Action<T> onSpawn, Action<T> onDespawn)
        {
            _container = new Stack<T>();
            _create = onCreate;
            _spawn = onSpawn;
            _despawn = onDespawn;
        }

        public T Fetch()
        {
            T obj;
            if (_container.Count > 0)
                obj = _container.Pop();
            else
                obj = _create();
            _spawn(obj);
            
            return obj;
        }

        public void Push(T obj)
        {
            _despawn(obj);
            _container.Push(obj);
        }

        void IObjectPool.Push(object obj) => Push((T) obj);
    }
}