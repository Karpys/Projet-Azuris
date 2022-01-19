using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LanguageKeyAttribute))]
public class LanguageKeyDrawer : PropertyDrawer
{
    LanguageKeyAttribute languageKeyAttribute { get { return ((LanguageKeyAttribute)attribute); } }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, new GUIContent("Language Key"));
        var rect = new Rect(position.position, Vector2.one * 20);
        if (GUI.Button(rect, "S"))
        {
            LanguageKeySearchWindow.OpenWindow();
        }
        position.position += Vector2.right * 25;
        EditorGUI.PropertyField(position, property, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
