using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextLocaliser : MonoBehaviour
{
    [LanguageKey] public string key;

    private void Start()
    {
        if (TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI textMeshProUGUI))
        {
            textMeshProUGUI.text = LanguageSystem.TryGetTextByKey(key);
        }
        else if (TryGetComponent<Text>(out Text textUI))
        {
            textUI.text = LanguageSystem.TryGetTextByKey(key);
        }
        else if (TryGetComponent<TextMesh>(out TextMesh textMesh))
        {
            textMesh.text = LanguageSystem.TryGetTextByKey(key);
        }
        else if (TryGetComponent<TextMeshPro>(out TextMeshPro textMeshPro))
        {
            textMeshPro.text = LanguageSystem.TryGetTextByKey(key);
        }
    }
}
