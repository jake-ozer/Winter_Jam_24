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
        if (!CheckIfPass(curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).eventID) && !choosingOption)
        {
            choosingOption = true;

            var compatibleNodeLinks = curDialogueContainer.NodeLinks.Where(x => x.BaseNodeGuid == curNodeGuid).ToList();
            foreach (var link in compatibleNodeLinks)
            {
                //only create option if prereq is met, (or there is no pre-req)
                if (string.IsNullOrEmpty(link.PreReqID) || FindFirstObjectByType<PrerequisiteManager>().CheckPrerequisite(link.PreReqID))
                {
                    GameObject option = Instantiate(optionPrefab);
                    option.GetComponent<DialogueOption>().AssignNextNodeGuid(link.TargetNodeGuid);
                    option.transform.SetParent(diaogueOptionsParent.transform);
                    option.transform.Find("OptionText").GetComponent<TextMeshProUGUI>().text = link.PortName;
                }
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
                    //handle any events that might occur
                    HandleEvents(curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).eventID);

                    //if its a pass node, go straight to next one. else, wait for user click input
                    if (CheckIfPass(curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).eventID))
                    {
                        curNodeGuid = PassToNextNodeGuid(curNodeGuid);
                        StartCoroutine(FeedDialogue(curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).DialogueText));
                    }

                    //exit dialogue
                    if (CheckIfEnd(curDialogueContainer.DialogueNodeData.Find(x => x.Guid == curNodeGuid).eventID))
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

    //method to parse out comma seperated event IDs and then execute those (ignore pass, end)
    private void HandleEvents(string IDs)
    {
        if (!string.IsNullOrEmpty(IDs))
        {
            IDs = IDs.Trim();
            string[] tokens = IDs.Split(',');
            foreach (string token in tokens)
            {
                if (token.Trim() != "pass" && token.Trim() != "end")
                {
                    //handle event
                    Debug.Log("there is an event to handle: " + token.Trim());
                }
            }
        }
    }

    //method to check whether a set of comma seperated event IDs has a pass
    private bool CheckIfPass(string IDs)
    {
        string[] tokens = IDs.Split(',');
        foreach (string token in tokens)
        {
            if (token.Trim() == "pass")
            {
                return true;
            }
        }
        return false;
    }

    //method to check whether a set of comma seperated event IDs has an end
    private bool CheckIfEnd(string IDs)
    {
        string[] tokens = IDs.Split(',');
        foreach (string token in tokens)
        {
            if (token.Trim() == "end")
            {
                return true;
            }
        }
        return false;
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
