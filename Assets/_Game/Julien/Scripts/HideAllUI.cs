using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideAllUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> toHide;
    [SerializeField] private TextMeshProUGUI text;

    public void ShowHideUI()
    {
        bool toggle = !toHide[0].activeSelf;
        for (int i = 0; i < toHide.Count; i++)
        {
            toHide[i].SetActive(toggle);
        }

        text.text = toggle ? LanguageSystem.TryGetTextByKey("button.drive") : LanguageSystem.TryGetTextByKey("button.dialogue");
    }
}
