using UnityEngine;

namespace Mud.Utilities
{
    public static class GameObjectUtility
    {
        public static T Create<T>(string name, Transform parent, Vector3 position, Quaternion rotation) where T: Component
        {
            var obj = new GameObject(name);
            obj.transform.SetPositionAndRotation(position, rotation);
            obj.transform.SetParent(parent);
            
            var component = obj.AddComponent<T>();
            
            return component;
        }
        
        public static T Create<T>(string name, Transform parent) where T : Component
        {
            return Create<T>(name, parent, Vector3.zero, Quaternion.identity);
        }
        
        public static T Create<T>(string name) where T : Component
        {
            return Create<T>(name, null, Vector3.zero, Quaternion.identity);
        }
        
        public static GameObject Create(string name, Transform parent, Vector3 position, Quaternion rotation) 
        {
            var obj = new GameObject(name);
            obj.transform.SetPositionAndRotation(position, rotation);
            obj.transform.SetParent(parent);

            return obj;
        }

        public static GameObject Create(string name, Transform parent)
        {
            return Create(name, parent, Vector3.zero, Quaternion.identity);
        }
        
        public static GameObject Create(string name)
        {
            return Create(name, null, Vector3.zero, Quaternion.identity);
        }
    }
}