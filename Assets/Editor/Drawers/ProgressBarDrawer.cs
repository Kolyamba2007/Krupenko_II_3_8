using System.Reflection;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

namespace Ziggurat.UI
{
    [CustomPropertyDrawer(typeof(ProgressBar))]
    public class ProgressBarDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetInfo = property.serializedObject.targetObject.GetType().GetField(property.name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            object target = targetInfo.GetValue(property.serializedObject.targetObject);

            var labelInfo = target.GetType().GetProperty("Label", BindingFlags.Public | BindingFlags.Instance);
            string labelText = labelInfo.GetValue(target).ToString();

            var valueInfo = target.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            float value = (float)Math.Round((float)valueInfo.GetValue(target), 1);
            EditorGUI.BeginChangeCheck();
            EditorGUI.ProgressBar(position, value, labelText + $" ({value * 100}%)");
            if (EditorGUI.EndChangeCheck())
            {
                EditorGUI.ProgressBar(position, value, labelText + $" ({value * 100}%)");
            }
        }
    }
}
#endif