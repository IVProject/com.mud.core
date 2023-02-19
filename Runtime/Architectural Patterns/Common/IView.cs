using System;
using UnityEngine;

namespace Mud.ArchitecturalPatterns
{
    public interface IView : IDisposable
    {
        public GameObject GameObject { get; }
    }
}