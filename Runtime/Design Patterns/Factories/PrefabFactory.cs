using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Mud.DesignPatterns.Factories
{
    public class PrefabFactory<T> : IFactory<Object, T> where T : class
    {
        public event Action OnCreated;

        public T Create(Object prefab)
        {
            var obj = GameObject.Instantiate(prefab) as T;
            OnCreated?.Invoke();
            
            return obj;
        }
    }
}