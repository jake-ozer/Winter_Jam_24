using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueNode : Node
{
    public string GUID;
    public string dialogueText;
    public bool entryPoint = false;
    public string eventID;
    //public Dictionary<string, string> choicePreReqIDs = new Dictionary<string, string>();
    public SerializedDictionary<string, string> choicePreReqIDs = new SerializedDictionary<string, string>();
}
