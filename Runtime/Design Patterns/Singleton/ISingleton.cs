namespace Mud.DesignPatterns
{
    public interface ISingleton
    {
        public static object Instance { get; }
    }
    
    public interface ISingleton<T>: ISingleton
    {
        public new static T Instance { get; }
    }
}