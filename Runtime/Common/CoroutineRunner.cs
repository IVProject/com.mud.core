using System;
using System.Collections;
using Mud.Utilities;
using UnityEngine;

namespace Mud
{
    /// <summary>
    /// Responsible for launching coroutines from anywhere.
    /// </summary>
    public sealed class CoroutineRunner : IDisposable
    {
        private static CoroutineRunner _mainRunner;
        private CoroutineContext _context;

        public static CoroutineRunner Main => _mainRunner ??= new CoroutineRunner("MainCoroutineRunner", true);

        public CoroutineRunner(string name, bool isDontDestroy)
        {
            _context = GameObjectUtility.Create<CoroutineContext>(name, null);
            if(isDontDestroy) _context.MakeDontDestroyOnLoad();
        }

        public CoroutineRunner(string name): this(name,false) { }
        
        public Coroutine Start(IEnumerator routine)
        {
            return _context.StartCoroutine(routine);
        }

        public void Stop(Coroutine routine)
        {
            _context.StopCoroutine(routine);
        }

        public void Dispose()
        {
            _context?.StopAllCoroutines();
            if (_context != null) GameObject.Destroy(_context.gameObject);
        }

        private class CoroutineContext: MonoBehaviour
        {
            public void MakeDontDestroyOnLoad()
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}