using UnityEngine;
using UnityEditor;

public sealed class TagMaskFieldAttribute : PropertyAttribute { }


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(TagMaskFieldAttribute))]

public class TagMaskFieldAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
    }
}

#endif