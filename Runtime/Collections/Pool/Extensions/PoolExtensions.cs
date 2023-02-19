using System.Collections.Generic;

namespace Mud.Collections.Pool
{
    public static partial class PoolExtensions
    {
        /// <summary>
        /// Fetch specified number of consecutive elements.
        /// </summary>
        public static IReadOnlyList<T> Take<T>(this IObjectPool<T> self, int count) where T : class
        {
            List<T> result = new List<T>();
            for (int i = 0; i < count; i++)
                result.Add(self.Fetch());
            
            return result;
        }
        /// <summary>
        /// Fetch specified number of consecutive elements.
        /// </summary>
        public static IReadOnlyList<T> Take<Parametr, T>(this IObjectPool<Parametr,T> self, Parametr p, int count) where T : class
        {
            List<T> result = new List<T>();
            for (int i = 0; i < count; i++)
                result.Add(self.Fetch(p));
            
            return result;
        }
        /// <summary>
        /// Fetch all objects from the pool.
        /// </summary>
        public static IReadOnlyList<T> FetchAll<T>(this IObjectPool<T> self) where T : class
        {
            List<T> result = new List<T>();
            while (self.Count!=0)
                result.Add(self.Fetch());
            
            return result;
        }
        /// <summary>
        /// Fetch all objects from the pool.
        /// </summary>
        public static IReadOnlyList<T> FetchAll<Parameter, T>(this IObjectPool<Parameter, T> self, Parameter p) where T : class
        {
            List<T> result = new List<T>();
            while (self.Count != 0)
                result.Add(self.Fetch(p));

            return result;
        }
        /// <summary>
        /// Merging the target pool into the current one. When merging, objects from the target pool will go through the process of fetching up and pushing.
        /// </summary>
        public static void Merge<T>(this IObjectPool<T> self, IObjectPool<T> target) where T : class
        {
            while (target.Count!=0)
                self.Push(target.Fetch());
        }
        /// <summary>
        /// Merging the target pool into the current one. When merging, objects from the target pool will go through the process of fetching up and pushing.
        /// </summary>
        public static void Merge<Parametr, T>(this IObjectPool<Parametr,T> self, IObjectPool<Parametr,T> target) where T : class
        {
            while (target.Count != 0)
                self.Push(target.Fetch((Parametr) default));
        }
    }
}