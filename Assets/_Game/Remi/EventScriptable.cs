using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Event", order = 1)]
public class EventScriptable : ScriptableObject
{
    public List<Dialogue> Description;
    public string Reponse1;
    public string Reponse2;
    public Sprite Character;
    public List<Consequence> ConsequenceReponse1;
    public List<Consequence> ConsequenceReponse2;
}

[System.Serializable]
public class Dialogue
{
    public string Name;
    public string Text;
}