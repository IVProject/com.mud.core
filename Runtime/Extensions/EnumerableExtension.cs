using System;
using System.Collections.Generic;
using System.Linq;

namespace Mud
{
    public static partial class EnumerableExtension
    {
        public static T GetRandomOrDefault<T>(this IEnumerable<T> source, Random random)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (random == null) throw new ArgumentNullException("random");

            var count = source.Count();
            if (count == 0)
                return default(T);

            var index = random.Next(0, count);
            return source.ElementAtOrDefault(index);
        }

        public static T GetRandomOrDefault<T>(this IEnumerable<T> source, int seed)
        {
            var random = new Random(seed);
            return GetRandomOrDefault(source, random);
        }

        public static T GetRandomOrDefault<T>(this IEnumerable<T> source)
        {
            var random = new Random();
            return GetRandomOrDefault(source, random);
        }
        
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, params Func<T, bool>[] predicates)
        {
            foreach (var predicate in predicates)
                source = source.Where(predicate);
        
            return source;
        }
    }
}