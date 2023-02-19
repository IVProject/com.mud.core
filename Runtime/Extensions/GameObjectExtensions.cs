using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mud
{
    public static partial class GameObjectExtensions
    {
        public static T ProvideComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent(out T component) ? component : gameObject.AddComponent<T>();
        }
        
        /// <summary>
        /// Detection of components around the GameObject. Notes: will be detected if there is a collider.
        /// </summary>
        public static IEnumerable<T> DetectComponents<T>(this GameObject gameObject, float radius) where T: Component
        {
            var position = gameObject.transform.position;
            var hitColliders = Physics.OverlapSphere(position, radius)
                .Where(value => value.gameObject != gameObject).ToList();
            
            var result = new List<T>();
            hitColliders.ForEach(value =>
            {
                var component = value.GetComponent<T>();
                if(component!=null)
                    result.Add(component);
            });
            
            return result;
        }
    }
}
