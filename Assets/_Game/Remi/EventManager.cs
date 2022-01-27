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
    public float Speed;
    public GAMESTATE State;

    public int ActualDescription;
    public Dialogue ActualDialogue;

    //Event Leave//
    public List<Events> JazzEvent;
    public List<Events> MecaEvent;
    public List<Events> MedecinEvent;
    public List<Events> NavigateurEvent;
    public List<Events> RouteDroite;
    public List<Events> RouteGauche;

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
        ActualDescription = 0;
        if (ActualEvent < Events.Count)
        {
            ActualDialogue = Events[ActualEvent].EventData.Description[ActualDescription];
            Description.text = ActualDialogue.Text;
            Reponse1Text.text = "Prochain Dialogue";
            Reponse2Text.text = "Prochain Dialogue";
            SpriteCharacter.sprite = FindCharacterByName(ActualDialogue.Name).Visual;
        }
        else
        {
            Description.text = "PLUS RIEN";
            Reponse1Text.text = "PLUS RIEN";
            Reponse2Text.text = "PLUS RIEN";
        }
    }

    public void NextDescription(int rep)
    {

        if (ActualDescription == Events[ActualEvent].EventData.Description.Count - 1)
        {
            ReponseInput(rep);
            ActualDescription += 1;
            return;
        }else if (ActualDescription == Events[ActualEvent].EventData.Description.Count)
        {
            return;
        }

        ActualDescription += 1;
        ActualDialogue = Events[ActualEvent].EventData.Description[ActualDescription];
        Description.text = ActualDialogue.Text;
        SpriteCharacter.sprite = FindCharacterByName(ActualDialogue.Name).Visual;

        if (ActualDescription == Events[ActualEvent].EventData.Description.Count-1)
        {
            Reponse1Text.text = Events[ActualEvent].EventData.Reponse1;
            Reponse2Text.text = Events[ActualEvent].EventData.Reponse2;
        }

    }


    public void ReponseInput(int Rep)
    {
        
        if (Rep == 1)
        {
            for (int i = 0; i < Events[ActualEvent].EventData.ConsequenceReponse1.Count; i++)
            {
                Consequence Cons = Events[ActualEvent].EventData.ConsequenceReponse1[i];
                Character Perso = FindCharacterByName(Cons.Dialogue.Name);
                ActualDialogue = Events[ActualEvent].EventData.ConsequenceReponse1[0].Dialogue;
                Perso.Joy += Cons.JoyEffect;
                Description.text = ActualDialogue.Text;
                SpriteCharacter.sprite = FindCharacterByName(ActualDialogue.Name).Visual;
                SpecialEvent(Perso.Name);
            }
        }
        else
        {
            for (int i = 0; i < Events[ActualEvent].EventData.ConsequenceReponse2.Count; i++)
            {
                Consequence Cons = Events[ActualEvent].EventData.ConsequenceReponse2[i];
                Character Perso = FindCharacterByName(Cons.Dialogue.Name);
                
                ActualDialogue = Events[ActualEvent].EventData.ConsequenceReponse2[0].Dialogue;
                Perso.Joy += Cons.JoyEffect;
                Description.text = ActualDialogue.Text;
                SpriteCharacter.sprite = FindCharacterByName(ActualDialogue.Name).Visual;
                SpecialEvent(Perso.Name);
            }
        }
        Reponse1Text.text = "";
        Reponse2Text.text = "";
        BarreManager.instance.UpdateUI();
        CheckEquipage();
        Events.RemoveAt(0);
        StartCoroutine(CloseEvent(Speed));
    }

    public void SpecialEvent(string name)
    {
        if (name == "RouteDroite")
        {
            AddEvent(RouteDroite);
            RouteDroite.Clear();
        }
        else if (name == "RouteGauche")
        {
            AddEvent(RouteGauche);
            RouteGauche.Clear();
        }else if (name == "MortJazz")
        {
            BarreManager.instance.HideBarre(1);
        }else if (name == "Defaite")
        {
            Debug.Log("Defaite");
        }
    }

    public void CheckEquipage()
    {
        if (Characters.Characters[1].Joy <= 0)
        {
            AddEvent(JazzEvent);
            JazzEvent.Clear();
        }else if (Characters.Characters[0].Joy <= 0)
        {
            AddEvent(NavigateurEvent);
            NavigateurEvent.Clear();
        }
        else if (Characters.Characters[3].Joy <= 0)
        {
            AddEvent(MecaEvent);
            MecaEvent.Clear();
        }
        else if (Characters.Characters[4].Joy <= 0)
        {
            AddEvent(MedecinEvent);
            MedecinEvent.Clear();
        }
    }

    public void AddEvent(List<Events> Event)
    {
        foreach (Events events in Event)
        {
            Events.Insert(1,events);
        }
    }

    public IEnumerator CloseEvent(float delay)
    {
        yield return new WaitForSeconds(delay);
        State = GAMESTATE.CLOSE;
        AnimUi.Play("CloseAnim");
        StartCoroutine(LaunchEvent(delay * 2));
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
        Debug.Log("Pas trouv�");
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
