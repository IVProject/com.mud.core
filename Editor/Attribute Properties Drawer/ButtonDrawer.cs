using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Mud.Editor
{
    [CustomPropertyDrawer(typeof(ButtonFieldAttribute))]
    public class ButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string methodName = (attribute as ButtonFieldAttribute).MethodName;
            string name = (attribute as ButtonFieldAttribute).Name;
            Object target = property.serializedObject.targetObject;
            System.Type type = target.GetType();
            System.Reflection.MethodInfo method = type.GetMethod(methodName);
            if (method == null)
            {
                GUI.Label(position, "Method could not be found. Is it public?");
                return;
            }
            if (method.GetParameters().Length > 0)
            {
                GUI.Label(position, "Method cannot have parameters.");
                return;
            }
            if (GUI.Button(position, name))
            {
                method.Invoke(target, null);
            }
        }
    }
}
