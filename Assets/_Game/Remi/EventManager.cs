using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    public List<Events> Events;
    public Equipage Characters;
    public int ActualEvent;


    //Text//
    public TMP_Text Description;
    public TMP_Text Reponse1Text;
    public TMP_Text Reponse2Text;
    public void Start()
    {
        LaunchEvent();
    }
    public void LaunchEvent()
    {
        if (ActualEvent < Events.Count)
        {

            Description.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Description);
            Reponse1Text.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Reponse1);
            Reponse2Text.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Reponse2);
        }
        else
        {
            Description.text = "PLUS RIEN";
            Reponse1Text.text = "PLUS RIEN";
            Reponse2Text.text = "PLUS RIEN";
        }
    }

    public void RefreshUI()
    {
        Description.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Description);
        Reponse1Text.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Reponse1);
        Reponse2Text.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Reponse2);
    }


    public void ReponseInput(int Rep)
    {
        if (Rep == 1)
        {
            for (int i = 0; i < Events[ActualEvent].EventData.ConsequenceReponse1.Count; i++)
            {
                Consequence Cons = Events[ActualEvent].EventData.ConsequenceReponse1[i];
                Character Perso = FindCharacterByName(Cons.Name);
                Perso.Joy += Cons.JoyEffect;
            }
        }
        else
        {
            for (int i = 0; i < Events[ActualEvent].EventData.ConsequenceReponse2.Count; i++)
            {
                Consequence Cons = Events[ActualEvent].EventData.ConsequenceReponse2[i];
                Character Perso = FindCharacterByName(Cons.Name);
                Perso.Joy += Cons.JoyEffect;
            }
        }

        ActualEvent += 1;
        LaunchEvent();
    }

    public Character FindCharacterByName(string name)
    {
        for (int i = 0; i < Characters.Characters.Count; i++)
        {
            if (Characters.Characters[i].Name == name)
            {
                return Characters.Characters[i];
            }
        }
        Debug.Log("Pas trouvé");
        return null;
    }
}
