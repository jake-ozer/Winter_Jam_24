using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueItem", menuName = "Scriptable Objects/DialogueItem")]
public class DialogueItem : ScriptableObject
{
    public string npcName;
    [TextArea(3, 10)]
    public string[] sentences;

    private const int maxChar = 50;

    private void OnValidate()
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            if (sentences[i].Length > maxChar)
            {
                sentences[i] = sentences[i].Substring(0, maxChar);
            }
        }
    }
}
