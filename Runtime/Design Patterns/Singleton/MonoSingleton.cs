using System;
using UnityEngine;

namespace Mud.DesignPatterns.Singleton
{
    /// <summary>
    /// Typical singleton. The logic is like with burgers, you can eat one, but if you eat a lot of it, it's not good for you.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton<T> where T : Component
    {
        private static T _instance;

        public static T Instance => GetInstance();

        protected virtual void Awake()
        {
            var settings = Attribute.GetCustomAttribute(typeof(T), typeof(DontDestroySingletonAttribute)) as DontDestroySingletonAttribute;
            if (settings == null)
            {
                settings = new DontDestroySingletonAttribute(false);
            }

            if (_instance == null)
            {
                _instance = this as T;
                if (settings.IsDontDestroy)
                    DontDestroyOnLoad(_instance.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private static T GetInstance()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}
