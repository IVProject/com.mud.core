using System;
using Mud.Collections;

namespace Mud.ArchitecturalPatterns.MVP
{
    public abstract class BasePresenter<TViewContract> : IDisposable where TViewContract : IView
    {
        protected TViewContract View;

        private DisposablesCollection _disposables = new DisposablesCollection(); 

        public BasePresenter(TViewContract view) => View = view;
        
        public void Dispose()
        {
            OnDispose();
            _disposables.Dispose();
            View?.Dispose();
        }
        
        protected virtual void OnDispose(){ }

        protected T AddDisposable<T>(T disposable) where T : IDisposable
        {
            _disposables.Add(disposable);
            return disposable;
        }
    }
}