using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReadOnly : PropertyAttribute
{
    public ReadOnly()
    {

    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnly))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property);
    }
}
#endif