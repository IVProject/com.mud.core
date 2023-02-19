using System;
using Mud.Collections;
using UnityEngine;

namespace Mud.ArchitecturalPatterns.MVC
{
    public abstract class BaseView<TContractModel>: MonoBehaviour, IView
    {
        protected TContractModel Model;
        private DisposablesCollection _disposables = new DisposablesCollection();
        
        public GameObject GameObject => gameObject;
        public bool IsInitialized => Model != null;

        public void Initialize(TContractModel model)
        {
            Model ??= model;
        }

        public void Dispose()
        {
            OnDispose();
            _disposables.Dispose();
        }

        protected virtual void OnDispose() { }

        protected T AddDisposable<T>(T disposable) where T : IDisposable
        {
            _disposables.Add(disposable);
            return disposable;
        }
    }
}