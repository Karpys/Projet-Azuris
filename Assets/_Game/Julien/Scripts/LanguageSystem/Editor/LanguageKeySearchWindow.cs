using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LanguageKeySearchWindow : EditorWindow
{
    public static void OpenWindow()
    {
        LanguageKeySearchWindow window = new LanguageKeySearchWindow();
        window.titleContent = new GUIContent("Language Manager");

        Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect r = new Rect(mouse.x - 450, mouse.y + 5, 10, 10);
        window.ShowAsDropDown(r, new Vector2(500, 300));
    }

    private string value = "";
    private Vector2 scroll;
    private string[] files;

    private void OnGUI()
    {

        value = GUILayout.TextField(value);
        
        scroll = GUILayout.BeginScrollView(scroll);

        List<string> listOfAllKeys = new List<string>();
        
        int index = 0;

        files = LanguageSystem.GetLanguageName();
        
        foreach (string file in files)
        {
            Dictionary<string, string> localisedValue = LanguageSystem.GetAllLocalisedValue(index);

            foreach (var localVal in localisedValue)
            {
                if (!listOfAllKeys.Contains(localVal.Key))
                {
                    listOfAllKeys.Add(localVal.Key);
                }
            }
            index++;
        }

        foreach (string key in listOfAllKeys)
        {
            if (key.ToLower().Contains(value.ToLower()))
            {
                GUIStyle style = new GUIStyle(GUI.skin.textField);
                style.fixedHeight = 20;
                style.margin = new RectOffset(5, 5, 5, 5);
                EditorGUILayout.SelectableLabel(key, style);
            }
        }
        
        GUILayout.EndScrollView();
    }
}
