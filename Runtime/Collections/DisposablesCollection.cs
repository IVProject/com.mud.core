using System;
using System.Collections;
using System.Collections.Generic;

namespace Mud.Collections
{
    /// <summary>
    /// Allows you to group a whole bunch of IDisposables together and delete them all at once.
    /// </summary>
    public sealed class DisposablesCollection: ICollection<IDisposable>, IDisposable
    {
        private readonly object _locker = new object();
        private List<IDisposable> _disposables;

        public int Count => GetCount();
        public bool IsReadOnly => false;

        public DisposablesCollection() => _disposables = new List<IDisposable>();
        
        public DisposablesCollection(int capacity) => _disposables = new List<IDisposable>(capacity);
        
        public DisposablesCollection(IDisposable[] disposables) => _disposables = new List<IDisposable>(disposables);
        
        public DisposablesCollection(IEnumerable<IDisposable> disposables) => _disposables = new List<IDisposable>(disposables);
        
        public IEnumerator<IDisposable> GetEnumerator()
        {
            lock (_locker)
            {
                return _disposables.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IDisposable item)
        {
            lock (_locker)
            {
                _disposables.Add(item);
            }
        }

        public bool Remove(IDisposable item)
        {
            lock (_locker)
            {
                return _disposables.Remove(item);
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _disposables.Clear();
            }
        }

        public bool Contains(IDisposable item)
        {
            lock (_locker)
            {
                return _disposables.Contains(item);
            }
        }
        
        public void CopyTo(IDisposable[] array, int arrayIndex)
        {
            lock (_locker)
            {
                _disposables.CopyTo(array, arrayIndex);
            }
        }

        public void Dispose()
        {
            lock (_locker)
            {
                foreach (var disposable in _disposables)
                    disposable?.Dispose();
                _disposables.Clear();
                _disposables = null;
            }
        }

        private int GetCount()
        {
            lock (_locker)
            {
                return _disposables.Count;
            }
        }
    }
}