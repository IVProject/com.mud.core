using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mud
{
    public sealed class JobScheduler: IDisposable
    {
        private List<ScheduledJob> _jobRegistry;
        private CancellationTokenSource _cts;
        
        public IEnumerable<JobNote> JobNotes { get; private set; }
        public bool IsRun => _cts != null;

        public JobScheduler()
        {
            _jobRegistry = new List<ScheduledJob>();
        }

        public JobScheduler(IEnumerable<JobNote> jobNotes): base()
        {
            JobNotes = jobNotes;
            foreach (var jobNote in jobNotes)
                _jobRegistry.Add(new ScheduledJob(jobNote));
        }

        public void RegisterOrSubscribe(Action action, TimeSpan delay)
        {
            var name = action.Method.Name;
            var exists =  _jobRegistry.Exists(j => j.Note.Name == name);;

            if (exists == false)
            {
                _jobRegistry.Add(new ScheduledJob(action, delay));
                OnChangeJobRegistry();
                return;
            }
            
            var index = _jobRegistry.FindIndex(j => j.Note.Name == name);
            _jobRegistry[index].Action = action;
        }

        public void Unregister(string name)
        {
            var job = _jobRegistry.FirstOrDefault(j => j.Note.Name == name);
            if (job == default) return;
            _jobRegistry.Remove(job);
            OnChangeJobRegistry();
        }

        public async Task Run()
        {
            if (_cts != null) return;
            _cts = new CancellationTokenSource();
            var interval = _jobRegistry.Min(j => j.Note.Interval);
            await Task.Run(async () =>
            {
                while (_cts.IsCancellationRequested==false)
                {
                    foreach (var job in _jobRegistry)
                        job.Invoke();
                    await Task.Delay(interval);
                }
            }, _cts.Token);
        }

        public void Stop()
        {
            _cts?.Cancel();
            _cts = null;
        }

        private void OnChangeJobRegistry()
        {
            JobNotes = _jobRegistry.Select(j => j.Note);
            if (IsRun) return;
            Stop();
            Run();
        }
        
        public void Dispose()
        {
            _jobRegistry = null;
            _cts?.Dispose();
        }
        
        private class ScheduledJob
        {
            public JobNote Note;
            public Action Action;

            public ScheduledJob(JobNote note)
            {
                Note = note;
            }

            public ScheduledJob(Action action, TimeSpan interval)
            {
                Action = action;
                Note = new JobNote(action.Method.Name, DateTime.UtcNow, interval);
            }

            public void Invoke()
            {
                var count = GetCallCount();
                if (count==0)
                    return;
                
                for (int i = 0; i < count; i++)
                    Action?.Invoke();
                
                var compensationTime = Note.Interval * count;
                Note.LastInvoke = Note.LastInvoke.Add(compensationTime);
            }
            
            private int GetCallCount()
            {
                var elapsedTime = DateTime.UtcNow - Note.LastInvoke;
                return (int) (elapsedTime.Ticks / Note.Interval.Ticks);
            }
        }
        
        [Serializable]
        public class JobNote
        {
            public JobNote(string name, DateTime lastInvoke, TimeSpan interval)
            {
                Name = name;
                LastInvoke = lastInvoke;
                Interval = interval;
            }

            public string Name { get; }
            public DateTime LastInvoke { get; set; }
            public TimeSpan Interval { get; }
        }
    }
}