using System;

namespace Mud.ArchitecturalPatterns.MVC
{
    public interface IController<TContractModel> : IDisposable
    {
        bool IsInitialized { get; }
        void Initialize(TContractModel model);
    }
}