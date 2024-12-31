using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float dialogueWaitPerChar;
    private int curSentence;
    private DialogueItem curDialogueItem;
    private StateManager stateManager;
    private bool feedingDialogue = false;


    private void Start()
    {
        dialogueText.text = "";
        stateManager = FindFirstObjectByType<StateManager>();

    }

    public void StartDialogue(DialogueItem di)
    {
        stateManager.ChangeState(StateManager.GameState.dialogue);
        curSentence = 0;
        curDialogueItem = di;
        StartCoroutine(FeedDialogue(curDialogueItem.sentences[curSentence]));
    }

    private void Update()
    {
        if(stateManager.GetCurState() == StateManager.GameState.dialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (feedingDialogue)
                {
                    feedingDialogue = false;
                    StopAllCoroutines();
                    dialogueText.text = curDialogueItem.sentences[curSentence];
                }
                else
                {
                    curSentence++;
                    //temp exit dialogue
                    if (curSentence >= curDialogueItem.sentences.Length)
                    {
                        stateManager.ChangeState(StateManager.GameState.roam);
                        return;
                    }
                    StartCoroutine(FeedDialogue(curDialogueItem.sentences[curSentence]));
                }
            }
        }
    }

    private IEnumerator FeedDialogue(string sentence)
    {
        dialogueText.text = "";
        feedingDialogue = true;
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(dialogueWaitPerChar);
        }
        feedingDialogue = false;
    }
}
