using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mud
{
    public class ButtonFieldAttribute : PropertyAttribute
    {
        public string MethodName { get; }
        public string Name { get; set; }
        public ButtonFieldAttribute(string methodName)
        {
            MethodName = methodName;
            if (Name == null)
                Name = MethodName;
        }
    }
}
