using System.Collections;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float dialogueWaitPerChar;
    public GameObject diaogueOptionsParent;
    public GameObject optionPrefab;
    private int curSentence;
    private DialogueContainer curDialogueContainer;
    private string curNodeGuid;
    private StateManager stateManager;
    private bool feedingDialogue = false;
    private bool choosingOption = false;
    private NPC_Dialogue curNpc;
    public TextMeshProUGUI nameText;

    private void Start()
    {
        dialogueText.text = "";
        stateManager = FindFirstObjectByType<StateManager>();

    }

    public void StartDialogue(DialogueContainer dc, string npcNodeGuid, NPC_Dialogue npc)
    {
        stateManager.ChangeState(StateManager.GameState.dialogue);
        nameText.text = npc.NPC_name;
        curDialogueContainer = dc;
        curNodeGuid = npcNodeGuid;
        curNpc = npc;
        StartCoroutine(FeedDialogue(dc.DialogueNodeData.Find(x => x.Guid == curNodeGuid).DialogueText));
        //load dialogue options, if any
        if(dc.DialogueNodeData.Find(x => x.Guid == curNodeGuid).eventID != "pass")
        {
            LoadDialogueOptions();
        }
    }

    private void LoadDialogueOptions()
    {
        if (curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).eventID != "pass" && !choosingOption)
        {
            choosingOption = true;

            var compatibleNodeLinks = curDialogueContainer.NodeLinks.Where(x => x.BaseNodeGuid == curNodeGuid).ToList();
            foreach (var link in compatibleNodeLinks)
            {
                GameObject option = Instantiate(optionPrefab);
                option.GetComponent<DialogueOption>().AssignNextNodeGuid(link.TargetNodeGuid);
                option.transform.SetParent(diaogueOptionsParent.transform);
                option.transform.Find("OptionText").GetComponent<TextMeshProUGUI>().text = link.PortName;
            }
        }
    }

    private void Update()
    {
        if(stateManager.GetCurState() == StateManager.GameState.dialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadDialogueOptions();

                if (feedingDialogue)
                {
                    feedingDialogue = false;
                    StopAllCoroutines();
                    dialogueText.text = curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).DialogueText;
                    
                }
                else
                {
                    //if its a pass node, go straight to next one. else, wait for user click input
                    if(curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).eventID == "pass")
                    {
                        curNodeGuid = PassToNextNodeGuid(curNodeGuid);
                        StartCoroutine(FeedDialogue(curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).DialogueText));
                    }

                    //temp exit dialogue
                    if (curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).eventID == "end")
                    {
                        curNpc.currentNodeGuid = curNodeGuid;
                        curNodeGuid = "";
                        curDialogueContainer = null;
                        stateManager.ChangeState(StateManager.GameState.roam);
                        return;
                    }
                    
                }
            }
        }
    }

    private string PassToNextNodeGuid(string curGuid)
    {
        DialogueNodeData curNodeData = curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid);
        NodeLinkData nld = curDialogueContainer.NodeLinks.Find(x => x.BaseNodeGuid == curGuid);
        string nextGuid = nld.TargetNodeGuid;

        return nextGuid;
    }

    public void UserPickNextNodeGuid(string guid)
    {
        choosingOption = false;
        curNodeGuid = guid;
        for (int i = 0; i < diaogueOptionsParent.transform.childCount; i++)
        {
            Destroy(diaogueOptionsParent.transform.GetChild(i).gameObject);
        }
        StartCoroutine(FeedDialogue(curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).DialogueText));
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
