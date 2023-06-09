using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Mud
{
    public static class TaskExtension
    {
        public static IEnumerator AsCoroutine(this Task task)
        {
            var runnedTask = Task.Run(async () => await task);
            yield return new WaitUntil(() => task.IsCompleted);
        }
        
        public static IEnumerator AsCoroutine(this Task task, CancellationToken cancellationToken)
        {
            var runnedTask = Task.Run(async () => await task, cancellationToken);
            yield return new WaitUntil(() => task.IsCompleted);
        }

        public static IEnumerator AsCoroutine<T>(this Task<T> task, System.Action<T> resultHandler)
        {
            var runnedTask = Task.Run(async () => await task);
            yield return new WaitUntil(() => runnedTask.IsCompleted);

            resultHandler(task.Result);
        }
        
        public static IEnumerator AsCoroutine<T>(this Task<T> task, CancellationToken cancellationToken, System.Action<T> resultHandler)
        {
            var runnedTask = Task.Run(async () => await task, cancellationToken);
            yield return new WaitUntil(() => runnedTask.IsCompleted);

            resultHandler(task.Result);
        }
    }
}