using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mud.Editor
{
    [CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
    public class ConditionalFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ConditionalFieldAttribute conditionalField = attribute as ConditionalFieldAttribute;
            var fieldToCheck = property.serializedObject.FindProperty(conditionalField.FieldToCheck);
            var fieldValue = FetchValueFrom(fieldToCheck);
            
            
            
            if (conditionalField.ComparedValues.Any(data => data.Equals(fieldValue)) == false)
                return;

            EditorGUI.PropertyField(position, property);
        }

        private object FetchValueFrom(SerializedProperty property)
        {
            var type = property.serializedObject.targetObject.GetType();
            var field = type.GetField(property.propertyPath);
            if(field==null)
                field = type.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
            
            var value = field.GetValue(property.serializedObject.targetObject);

            return value;
        }
    }
}