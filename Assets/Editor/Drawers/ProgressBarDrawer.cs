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
            string labelText = property.FindPropertyRelative("Label").stringValue;
            float value = property.FindPropertyRelative("_value").floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.ProgressBar(position, 0f, labelText + $" ({0 * 100}%)");
            if (EditorGUI.EndChangeCheck())
            {
                EditorGUI.ProgressBar(position, 0f, labelText + $" ({0 * 100}%)");
            }
        }
    }
}
#endif