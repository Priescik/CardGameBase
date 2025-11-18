using Codice.Client.BaseCommands.Merge.Restorer;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TargetMode))]
public class TargetModeDrawer : PropertyDrawer
{
    const float toggleWidth = 10f;
    const float spacing = 2f;
    float lineHeight = EditorGUIUtility.singleLineHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lines = 1;
        if (property.isExpanded)
        {
            var useProp = property.FindPropertyRelative("_use");
            if (useProp.boolValue)
                lines += 4; // _use, _filterSide, _filterType, _random, _otherConditions
            // for _otherConditions ...
        }

        return lineHeight * lines + spacing * (lines - 1);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);

        // NEW

        Rect labelPosition = new Rect(position.x, position.y, position.width, lineHeight);

        Rect useRect = EditorGUI.PrefixLabel(
            labelPosition,
            EditorGUIUtility.GetControlID(FocusType.Passive),
            label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var useProp = property.FindPropertyRelative("_use");
        EditorGUI.PropertyField(useRect, useProp, GUIContent.none);
        //EditorGUI.PropertyField(useRect, useProp, new GUIContent(useProp.displayName));

        // OLD
        //Rect foldoutRect = new Rect(position.x, position.y, position.width, lineHeight);
        //property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);

        float addY = lineHeight;

        void DrawToggleField(string boolName, string fieldName)
        {
            var boolProp = property.FindPropertyRelative(boolName);
            Rect toggleRect = new(position.x, position.y + addY, lineHeight, lineHeight);
            EditorGUI.PropertyField(toggleRect, boolProp, new GUIContent(boolProp.displayName));

            //using (new EditorGUI.DisabledScope(!boolProp.boolValue))
            if (boolProp.boolValue)
            {
                //Rect secondaryPosition = EditorGUI.PrefixLabel(position, label); 
                var fieldProp = property.FindPropertyRelative(fieldName);
                Rect fieldRect = new Rect(position.x + toggleRect.xMax/2 + spacing, toggleRect.y, position.width - toggleRect.width - spacing, lineHeight);
                // why does this weird Rect calculation work?
                EditorGUI.PropertyField(fieldRect, fieldProp, new GUIContent(" ")); // this is stupid
            }

            addY += lineHeight + spacing;
        }

        if (property.isExpanded)
        {
            //// OLD
            //EditorGUI.indentLevel++;
            //var useProp = property.FindPropertyRelative("_use");
            //Rect useRect = new(position.x, position.y + addY, toggleWidth, lineHeight);
            //EditorGUI.PropertyField(useRect, useProp, new GUIContent(useProp.displayName));
            //addY += lineHeight + spacing;
            if (useProp.boolValue)
            {
                DrawToggleField("_filterSide", "_side");
                DrawToggleField("_filterType", "_type");
                DrawToggleField("_random", "_randomCount");
                // always visible
                using (new EditorGUI.DisabledScope(true))
                    EditorGUI.PropertyField(new(position.x, position.y + addY, position.width, lineHeight), property.FindPropertyRelative("_otherConditions"));
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }
}
