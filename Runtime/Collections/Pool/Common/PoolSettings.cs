using System;

namespace Mud.Collections.Pool
{
    [Serializable]
    public class PoolSettings
    {
        public int MaxSize = Int32.MaxValue;
        public int InitialSize = 0;
    }
}