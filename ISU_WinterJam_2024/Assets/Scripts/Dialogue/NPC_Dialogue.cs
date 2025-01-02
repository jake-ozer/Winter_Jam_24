using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public DialogueContainer dialogueContainer;
    public bool canEnterDialogue;
    public string currentNodeGuid;
    public string NPC_name;

    private void Start()
    {
        //skip the "Next" node and go to the first dialogue
        currentNodeGuid = dialogueContainer.NodeLinks[0].TargetNodeGuid;
    }

    public void TriggerDialogue()
    {
        if (canEnterDialogue)
        {
            FindFirstObjectByType<DialogueManager>().StartDialogue(dialogueContainer, currentNodeGuid, this);
        }
    }
}
