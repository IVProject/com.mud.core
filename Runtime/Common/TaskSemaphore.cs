using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mud.Utilities;

namespace Mud
{
    public sealed class TaskSemaphore: IDisposable
    {
        private readonly List<Guid> _waitingList = new List<Guid>();
        private readonly object _lock = new object();
        private readonly int _maxCount;

        public TaskSemaphore(int maxCount)
        {
            if (maxCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxCount), "Max count must be greater than zero.");
                    
            _maxCount = maxCount;
        }

        public TaskSemaphore(): this(1) { }

        public async Task<Guid> Wait()
        {
            Guid token;

            lock (_lock)
            {
                token = Guid.NewGuid();
                _waitingList.Add(token);
            }

            await TaskUtility.WaitUntil(() => _waitingList.Take(_maxCount).Contains(token));

            return token;
        }

        public void Release(Guid id)
        {
            if (_waitingList.Any(t => t == id))
            {
                lock (_lock) _waitingList.Remove(id);
            }
        }

        public void Dispose()
        {
            lock (_lock) _waitingList.Clear();
        }
    }
}