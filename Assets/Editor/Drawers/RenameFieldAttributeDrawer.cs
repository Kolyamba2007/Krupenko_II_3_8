using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(RenameFieldAttribute))]
public class RenameFieldAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool isArray = property.isArray;

        if (!isArray && attribute is RenameFieldAttribute fieldName)
            label.text = fieldName.Name;

        EditorGUI.PropertyField(position, property, label, true);
    }
}
#endif