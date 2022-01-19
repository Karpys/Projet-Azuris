using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LanguageEditorWindow : EditorWindow
{
    [MenuItem("Tool/Language")]
    static void InitWindow()
    {
        LanguageEditorWindow window = GetWindow<LanguageEditorWindow>();

        window.minSize = new Vector2(300, 200);

        window.titleContent = new GUIContent("Language Manager");

        LanguageSystem.Init();

        window.Show();
    }

    private string searchString = "";
    private Vector2 scrollPos;
    private string[] files;

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        
        files = LanguageSystem.GetLanguageName();

        GUIStyle AddButtonStyle = new GUIStyle(GUI.skin.button);
        AddButtonStyle.fixedWidth = 30;
        AddButtonStyle.fixedHeight = 30;
        AddButtonStyle.fontSize = 25;
        AddButtonStyle.padding = new RectOffset(5, 5, 0, 5);
        AddButtonStyle.margin = new RectOffset(5, 5, 5, 5);

        GUIStyle searchBarStyle = new GUIStyle(GUI.skin.textField);
        searchBarStyle.fixedHeight = 30;
        searchBarStyle.margin = new RectOffset(5, 5, 5, 5);
        searchBarStyle.alignment = TextAnchor.MiddleLeft;
        searchBarStyle.fontSize = 20;

        if (GUILayout.Button("+", AddButtonStyle))
        {
            for (int i = 0; i < files.Length; i++)
            {
                LanguageSystem.Add(i, "", "");
            }
        }
        
        searchString = GUILayout.TextField(searchString, searchBarStyle);
        GUILayout.EndHorizontal();

        scrollPos = GUILayout.BeginScrollView(scrollPos);

        Dictionary<string, List<string>> allTranslate = new Dictionary<string, List<string>>();

        int index = 0;

        GUILayout.BeginHorizontal();
        
        foreach (string file in files)
        {
            GUIStyle langName = new GUIStyle(GUI.skin.button);
            langName.margin = new RectOffset(index == 0 ? 300 : 5, 0, 10, 5);
            langName.fixedWidth = 400;
            langName.fontSize = 20;
            
            GUILayout.Label(file, langName);
            Dictionary<string, string> localisedValue = LanguageSystem.GetAllLocalisedValue(index);

            foreach (var localVal in localisedValue)
            {
                if (!allTranslate.ContainsKey(localVal.Key))
                {
                    allTranslate.Add(localVal.Key, new List<string>());
                }
                
                allTranslate[localVal.Key].Add(localVal.Value);
            }
            index++;
        }
        GUIStyle addFileButton = new GUIStyle(GUI.skin.button);
        addFileButton.margin = new RectOffset(5, 0, 10, 5);
        addFileButton.fixedWidth = 100;
        addFileButton.fontSize = 20;
        if (GUILayout.Button("+", addFileButton))
        {
            string path = EditorUtility.SaveFilePanelInProject("Create new lang file", "English", "json",
                "Please select a path and a name to create your new lang file", Application.dataPath + "/Resources/Lang");
            if (!string.IsNullOrEmpty(path))
            {
                LanguageSystem.CreateNewFile(path);
                LanguageSystem.Init();
            }
        }

        GUILayout.EndHorizontal();

        foreach (var localised in allTranslate)
        {
            if (localised.Key.Contains(searchString))
            {
                DrawLangText(localised.Key, localised.Value);
            }
        }


        GUILayout.EndScrollView();
    }

    private void DrawLangText(string key, List<string> translate)
    {
        GUILayout.BeginHorizontal();
        GUIStyle RemoveButtonStyle = new GUIStyle(GUI.skin.button);
        RemoveButtonStyle.fixedWidth = 30;
        RemoveButtonStyle.fixedHeight = 30;
        RemoveButtonStyle.fontSize = 25;
        RemoveButtonStyle.padding = new RectOffset(5, 5, 0, 5);
        RemoveButtonStyle.margin = new RectOffset(5, 5, 5, 5);

        if (GUILayout.Button("x", RemoveButtonStyle))
        {
            for (int i = 0; i < files.Length; i++)
            {
                LanguageSystem.Remove(i, key);
            }
        }

        GUIStyle searchBarStyle = new GUIStyle(GUI.skin.textField);
        searchBarStyle.fixedHeight = 30;
        searchBarStyle.fixedWidth = 255;
        searchBarStyle.margin = new RectOffset(5, 5, 5, 5);
        searchBarStyle.alignment = TextAnchor.MiddleLeft;

        string newKey = key;

        newKey = GUILayout.TextField(newKey, searchBarStyle);
        if (newKey != key)
        {
            int i = 0;
            foreach (string text in translate)
            {
                LanguageSystem.Edit(i, newKey, key, text);
                i++;
            }
        }

        int index = 0;
        foreach (string text in translate)
        {
            string newText = text;
            GUIStyle textStyle = new GUIStyle(GUI.skin.textArea);
            textStyle.fixedWidth = 400;
            textStyle.margin = new RectOffset(5, 0, 5, 5);
            textStyle.fixedHeight = 30;
            newText = GUILayout.TextField(newText, textStyle);

            if (newText != text)
            {
                LanguageSystem.Edit(index, newKey, key, newText);
            }
            index++;
        }

        GUILayout.EndHorizontal();
    }
}