using System;
using Mud.Collections;
using UnityEngine;

namespace Mud.ArchitecturalPatterns.MVP
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        private DisposablesCollection _disposables = new DisposablesCollection();
        
        public GameObject GameObject => gameObject;

        public void Dispose()
        {
            OnDispose();
            _disposables.Dispose();
        }

        protected virtual void OnDispose(){ }

        protected T AddDisposable<T>(T disposable) where T : IDisposable
        {
            _disposables.Add(disposable);
            return disposable;
        }
    }
}