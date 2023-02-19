using System;
using System.Collections.Generic;
using System.Linq;

namespace Mud.Utilities
{
    public static class EnumUtility
    {
        public static IEnumerable<T> GetValues<T>() where T: Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IEnumerable<string> GetNames<T>() where T: Enum
        {
            return Enum.GetNames(typeof(T));
        }

        public static IEnumerable<(string Name, T Value)> GetNamesAndValues<T>() where T : Enum
        {
            return GetValues<T>().Select(x => (x.ToString(), x));
        }
    }
}