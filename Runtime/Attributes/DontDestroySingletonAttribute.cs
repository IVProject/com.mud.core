using System;

namespace Mud
{
    /// <summary>
    /// The instance will not be deleted after the scene is unloaded.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DontDestroySingletonAttribute : Attribute
    {
        public bool IsDontDestroy { get; private set; }

        public DontDestroySingletonAttribute(bool dontDestroy = true)
        {
            IsDontDestroy = dontDestroy;
        }
    }
}