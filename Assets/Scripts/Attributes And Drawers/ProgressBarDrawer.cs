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
            var targetInfo = property.serializedObject.targetObject.GetType().GetField(property.name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            object target = targetInfo.GetValue(property.serializedObject.targetObject);

            var visibleInfo = target.GetType().GetProperty("Visible", BindingFlags.Public | BindingFlags.Instance);
            bool visible = (bool)visibleInfo.GetValue(target);

            var productNameInfo = target.GetType().GetProperty("ProductName", BindingFlags.Public | BindingFlags.Instance);
            string productName = productNameInfo.GetValue(target).ToString();

            var valueInfo = target.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
            float value = (float)valueInfo.GetValue(target);
            EditorGUI.ProgressBar(position, value, productName + $" ({value * 100}%)");
        }
    }
}
#endif