using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreManager : MonoBehaviour
{

    public static BarreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    [SerializeField] private List<GameObject> barres;
    [SerializeField] private Equipage Equipage;

    public void UpdateUI()
    {
        for (int i = 0; i < barres.Count; i++)
        {
            float maxValue = ((RectTransform)barres[i].transform).rect.width;

            RectTransform barre = (RectTransform) barres[i].transform.GetChild(0);
            
            if (i == 0)
            {
                barre.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp((Equipage.Characters[0].Joy * maxValue) / 100f, 0, maxValue));
            }
            if (i == 1)
            {
                barre.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp((Equipage.Characters[1].Joy * maxValue) / 100f, 0, maxValue));
            }
            if (i == 2)
            {
                barre.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp((Equipage.Characters[3].Joy * maxValue) / 100f, 0, maxValue));
            }
            if (i == 3)
            {
                barre.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp((Equipage.Characters[4].Joy * maxValue) / 100f, 0, maxValue));
            }
        }
    }

    public void HideBarre(int index)
    {
        barres[index].SetActive(false);
    }

}
