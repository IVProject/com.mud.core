using System;
using System.Collections.Generic;
using Mud.DesignPatterns.Factories;

namespace Mud.Collections.Pool
{
    public abstract class BaseObjectPool<T> : IOnlyPushPool<T>, ICleanable where T : class, IBasePoolable
    { 
        protected readonly Stack<T> _container;
        protected readonly Action<T> _destroy;
        protected readonly Func<T> _create;
        protected readonly PoolSettings _poolSettings;

        public BaseObjectPool(Func<T> onCreate, Action<T> onDestroy, PoolSettings poolSettings = default)
        {
            _create = onCreate;
            _destroy = onDestroy;
            _poolSettings = poolSettings ?? new PoolSettings();
            _container = new Stack<T>(_poolSettings.InitialSize);
        }

        public BaseObjectPool(IFactory<T> creator, Action<T> onDestroy, PoolSettings poolSettings = default) 
            : this(creator.Create, onDestroy, poolSettings) { }

        public int Count => _container.Count;
        
        public void Clear()
        {
            while (_container.Count != 0)
                _destroy?.Invoke(_container.Pop());
        }
        
        public void Push(T obj)
        {
            if (_container.Count >= _poolSettings.MaxSize)
            {
                _destroy?.Invoke(obj);
                return;
            }
            
            obj.OnDespawn();
            if (_container.Contains(obj) == false)
                _container.Push(obj);
        }

        void IObjectPool.Push(object obj) => Push((T) obj);
    }
}