using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNodeData
{
    public string Guid;
    public string DialogueText;
    public string eventID;
    public Vector2 Position;
    public Dictionary<string, string> ChoicePreReqIDs = new Dictionary<string, string>();
}
