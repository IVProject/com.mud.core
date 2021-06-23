using System;
using UnityEngine;

namespace Mud
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T m_Instance;

        public static T Instance => GetInstance();

        protected virtual void Awake()
        {
            var settings = Attribute.GetCustomAttribute(typeof(T), typeof(DontDestroySingletonAttribute)) as DontDestroySingletonAttribute;
            if (settings == null)
            {
                settings = new DontDestroySingletonAttribute(false);
            }

            if (m_Instance == null)
            {
                m_Instance = this as T;
                if (settings.IsDontDestroy)
                    DontDestroyOnLoad(m_Instance.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private static T GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();
                if (m_Instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    m_Instance = obj.AddComponent<T>();
                }
            }

            return m_Instance;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DontDestroySingletonAttribute : Attribute
    {
        public bool IsDontDestroy { get; private set; }

        public DontDestroySingletonAttribute(bool dontDestroy = true)
        {
            IsDontDestroy = dontDestroy;
        }
    }
}
