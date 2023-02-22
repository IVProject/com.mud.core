namespace Mud.DesignPatterns
{
    /// <summary>
    /// Typical singleton. The logic is like with burgers, you can eat one, but if you eat a lot of it, it's not good for you.
    /// </summary>
    public abstract class Singleton<T> : ISingleton<T> where T : class, new()
    {
        private static T _instance;

        public static T Instance => GetInstance();

        private Singleton() { }
        
        private static T GetInstance()
        {
            if (_instance == null)
                _instance = new T();
            
            return _instance;
        }
    }
}