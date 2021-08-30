using System.Reflection;
using UnityEngine;

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

            var productNameInfo = target.GetType().GetProperty("ProductName", BindingFlags.Public | BindingFlags.Instance);
            string productName = productNameInfo.GetValue(target).ToString();

            var valueInfo = target.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
            float value = (float)valueInfo.GetValue(target);
            EditorGUI.ProgressBar(position, value, productName + $" ({value * 100}%)");
        }
    }
}
#endif