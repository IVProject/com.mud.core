using System;
using System.Collections;
using UnityEngine;

namespace Mud
{
    public static partial class MonoBehaviourExtension
    {
        public static Coroutine ExecuteWithDelay(this MonoBehaviour self, float delay, Action action)
        {
            IEnumerator Execute()
            {
                yield return new WaitForSeconds(delay);
                action?.Invoke();
            }

            return self.StartCoroutine(Execute());
        }
    }
}