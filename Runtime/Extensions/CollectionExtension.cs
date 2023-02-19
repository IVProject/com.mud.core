using System.Collections.Generic;

namespace Mud
{
    public static partial class CollectionExtension
    {
        public static void AddIfNew<T>(this ICollection<T> source, T value)
        {
            if (source.Contains(value) == false)
                source.Add(value);
        }
    }
}