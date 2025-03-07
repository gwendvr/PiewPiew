using UnityEngine;
using UnityEditor;

public sealed class TagMaskFieldAttribute : PropertyAttribute { }
[CustomPropertyDrawer(typeof(TagMaskFieldAttribute))]
public class TagMaskFieldAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
    }
}
