using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleJSON;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class LanguageSystem : MonoBehaviour
{
    static TextAsset[] languageFile;

    public static int selectedLanguage = 0;
    private static Dictionary<int, string> languageTitle = new Dictionary<int, string>();

    private static bool isInit = false;

    private static Dictionary<int, JSONNode> language = new Dictionary<int, JSONNode>();

    public static void Init()
    {
        languageFile = Resources.LoadAll<TextAsset>("Lang");
        language = new Dictionary<int, JSONNode>();
        languageTitle = new Dictionary<int, string>();

        int index = 0;
        foreach (TextAsset file in languageFile)
        {
            JSONNode node = JSON.Parse(file.text);
            language.Add(index, node);
            languageTitle.Add(index, file.name);
            index++;
        }

        isInit = true;
    }

    public static string TryGetTextByKey(string key)
    {
        if (!isInit) Init();

        string value = key;

        if (!string.IsNullOrEmpty(language[selectedLanguage][key].Value))
        {
            value = language[selectedLanguage][key].Value;
        }

        return value;
    }

    public static string[] GetLanguageName()
    {
        if (!isInit) Init();
        return languageTitle.Values.ToArray();
    }

    public static Dictionary<string, string> GetAllLocalisedValue(int file)
    {
        if (!isInit) Init();
        
        Dictionary<string, string> values = new Dictionary<string, string>();
        foreach (var textId in language[file])
        {
            values.Add(textId.Key, textId.Value.Value);
        }

        return values;
    }

#if UNITY_EDITOR
    public static void Add(int file, string key, string value)
    {
        JSONNode jsonNode = language[file];
        jsonNode.Add(key, new JSONString(value));

        File.WriteAllText(AssetDatabase.GetAssetPath(languageFile[file]), jsonNode.ToString());
        AssetDatabase.Refresh();
    }

    public static void Remove(int file, string key)
    {
        JSONNode jsonNode = language[file];
        jsonNode.Remove(key);

        File.WriteAllText(AssetDatabase.GetAssetPath(languageFile[file]), jsonNode.ToString());
        AssetDatabase.Refresh();
    }

    public static void Edit(int file, string key, string oldKey, string value)
    {
        Remove(file, oldKey);
        Add(file, key, value);
    }

    public static void CreateNewFile(string path)
    {
        JSONNode jsonNode = language[0];

        foreach (var node in jsonNode)
        {
            node.Value.Value = "";
        }

        
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }
        
        File.WriteAllText(path, jsonNode.ToString());
        AssetDatabase.Refresh();
    }
#endif
}