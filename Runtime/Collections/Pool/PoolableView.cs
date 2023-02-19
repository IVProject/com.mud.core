using Mud.ArchitecturalPatterns;
using UnityEngine;

namespace Mud.Collections.Pool
{
    public abstract class PoolableView : MonoBehaviour, IView, IPoolable<IObjectPool>
    {
        private IObjectPool _pool;
        
        public GameObject GameObject => gameObject;

        void IPoolable<IObjectPool>.OnSpawn(IObjectPool p)
        {
            OnSpawn();
            _pool = p;
            gameObject.SetActive(true);
        }

        void IBasePoolable.OnDespawn()
        {
            OnDespawn();
            _pool = null;
            gameObject.SetActive(false);
        }
        
        protected virtual void OnSpawn(){}
        protected virtual void OnDespawn(){}
        
        protected virtual void OnDispose(){}
        
        public void Dispose()
        {
            OnDispose();
            _pool?.Push(this);
        }
    }
}