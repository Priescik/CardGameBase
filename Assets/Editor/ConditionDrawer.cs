using Codice.Client.BaseCommands.Merge.Restorer;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Codice.CM.Common.CmCallContext;

[CustomPropertyDrawer(typeof(EffectCondition))]
public class ConditionDrawer : PropertyDrawer
{
    const float toggleWidth = 10f;
    const float spacing = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var obj = property.managedReferenceValue;
        if (obj != null)
            label = new GUIContent(obj.GetType().Name);


        Rect contentRect = EditorGUI.PrefixLabel(
            position,
            EditorGUIUtility.GetControlID(FocusType.Passive),
            new GUIContent(label));

        //int indent = EditorGUI.indentLevel;
        //EditorGUI.indentLevel = 0;

        SerializedProperty invertProp = property.FindPropertyRelative("InvertCheck");
        EditorGUI.PropertyField(contentRect, invertProp, new GUIContent(invertProp.displayName));

        EditorGUI.EndProperty();
    }
}
