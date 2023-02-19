using System;

namespace Mud.DesignPatterns.Factories
{
    public interface IFactory
    {
        event Action OnCreated;
    }

    public interface IFactory<out T> : IFactory where T : class
    {
        T Create();
    }

    public interface IFactory<in Parametr, out T> : IFactory where T : class
    {
        T Create(Parametr parametr);
    }

    public interface IFactory<in Parametr1, in Parametr2, out T> : IFactory where T : class
    {
        T Create(Parametr1 parametr, Parametr2 parametr2);
    }

    public interface IFactory<in Parametr1, in Parametr2, in Parametr3, out T> : IFactory where T : class
    {
        T Create(Parametr1 parametr1, Parametr2 parametr2, Parametr3 parametr3);
    }
}