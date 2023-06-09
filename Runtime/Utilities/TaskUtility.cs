using System;
using System.Threading.Tasks;

namespace Mud.Utilities
{
    public static class TaskUtility
    {
        public static async Task WaitUntil(Func<bool> predicate)
        {
            while (!predicate())
            {
                await Task.Yield();
            }
        }
        
        public static async Task WaitUntil(Func<bool> predicate, int intervalInMilliseconds)
        {
            while (!predicate())
            {
                await Task.Delay(intervalInMilliseconds);
            }
        }
    }
}