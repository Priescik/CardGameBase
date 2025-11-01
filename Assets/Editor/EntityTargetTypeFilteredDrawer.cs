using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(EntityTargetType), true)]
public class EntityTargetTypeFilteredDrawer : PropertyDrawer
{
    // The point of this drawer is to limit possible EntityTargetTypes
    // and disable the dropdown selection field when there is only one element to pick from
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = fieldInfo.GetCustomAttributes(typeof(AllowedEntityTargetTypesAttribute), false)
                            .FirstOrDefault() as AllowedEntityTargetTypesAttribute;

        EntityTargetType[] allowed = attr != null ? attr.AllowedValues : Enum.GetValues(typeof(EntityTargetType)).Cast<EntityTargetType>().ToArray();

        GUI.enabled = allowed.Length > 1; // disable if only one choice

        int index = Array.IndexOf(allowed, (EntityTargetType)property.enumValueIndex);
        string[] display = allowed.Select(v => v.ToString()).ToArray();

        index = EditorGUI.Popup(position, label.text, index >= 0 ? index : 0, display);

        property.enumValueIndex = (int)allowed[index];

        GUI.enabled = true; // reset GUI.enabled
    }
}
