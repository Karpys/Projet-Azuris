using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public List<Events> Events;
    public Equipage Characters;
    public int ActualEvent;

    public Animator AnimUi;
    public GAMESTATE State;


    //Text//
    public TMP_Text Description;
    public TMP_Text Reponse1Text;
    public TMP_Text Reponse2Text;
    public Image SpriteCharacter;
    public void Start()
    {
        State = GAMESTATE.OPEN;
        StartCoroutine(LaunchEvent(0));
    }
    /*public void LaunchEvent()
    {
        State = GAMESTATE.OPEN;
        AnimUi.Play("OpenAnim");
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
    }*/

    public IEnumerator LaunchEvent(float delay)
    {
        yield return new WaitForSeconds(delay);
        State = GAMESTATE.OPEN;
        AnimUi.Play("OpenAnim");
        if (ActualEvent < Events.Count)
        {

            Description.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Description);
            Reponse1Text.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Reponse1);
            Reponse2Text.text = LanguageSystem.TryGetTextByKey(Events[ActualEvent].EventData.Reponse2);
            SpriteCharacter.sprite = Events[ActualEvent].EventData.Character;
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
        State = GAMESTATE.CLOSE;
        AnimUi.Play("CloseAnim");
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
        StartCoroutine(LaunchEvent(2));
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

    public enum GAMESTATE
    {
        IDLE,
        OPEN,
        CLOSE,
        TRAVEL,
    }
}
