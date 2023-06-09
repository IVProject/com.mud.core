using System;

namespace Mud.DesignPatterns
{
    public interface ISingleton: IDisposable
    {
        public static object Instance { get; }
    }
    
    public interface ISingleton<T>: ISingleton
    {
        public new static T Instance { get; }
    }
}