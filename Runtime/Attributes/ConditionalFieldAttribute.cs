using System;
using System.Linq;
using UnityEngine;

namespace Mud
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ConditionalFieldAttribute : PropertyAttribute
    {
        public string FieldToCheck { get; private set; }
        public object[] ComparedValues { get; private set; }

        public ConditionalFieldAttribute(string fieldToCheck, params object[] comparedValues)
        {
            FieldToCheck = fieldToCheck;
            ComparedValues = comparedValues;
        }
        
        public ConditionalFieldAttribute(string fieldToCheck, params bool[] comparedValues)
        {
            FieldToCheck = fieldToCheck;
            ComparedValues = comparedValues.Select(b => (object) b).ToArray();;
        }
    }
}
