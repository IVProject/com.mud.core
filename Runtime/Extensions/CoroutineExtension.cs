using System.Collections;
using UnityEngine;

namespace Mud
{
    public static partial class CoroutineExtension
    {
        public static Coroutine Start(this IEnumerator self)
        {
            return CoroutineRunner.Main.Start(self);
        }
        
        public static Coroutine Start(this IEnumerator self, MonoBehaviour script)
        {
            return script.StartCoroutine(self);
        }

        public static Coroutine StartAndStopAfter(this IEnumerator self, MonoBehaviour script, float duration)
        {
            var coroutine = script.StartCoroutine(self);
            script.ExecuteWithDelay(duration, () => script.StopCoroutine(coroutine));
            
            return coroutine;
        }

        public static void Stop(this Coroutine self)
        {
            CoroutineRunner.Main.Stop(self);
        }
        
        public static void Stop(this Coroutine self, MonoBehaviour script)
        {
            script.StopCoroutine(self);
        }

        public static void DelayedStop(this Coroutine self, MonoBehaviour script, float delay)
        {
            script.ExecuteWithDelay(delay, () => script.StopCoroutine(self));
        }
    }
}