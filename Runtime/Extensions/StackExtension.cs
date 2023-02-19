using System.Collections.Generic;

namespace Mud
{
    public static partial class StackExtension
    {
        public static void PushIfNew<T>(this Stack<T> source, T value)
        {
            
            if (source.Contains(value) == false)
                source.Push(value);
        }
    }
}