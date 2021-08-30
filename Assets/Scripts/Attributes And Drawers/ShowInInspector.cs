using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Property)]
public class ShowInInspector : PropertyAttribute
{
    public ShowInInspector()
    {

    }
}

[CustomPropertyDrawer(typeof(ShowInInspector))]
public class ShowInInspectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.LabelField(position, "1231");
        EditorGUI.PropertyField(position, property);
    }
}