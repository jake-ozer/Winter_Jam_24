using UnityEngine;

public class DialogueOption : MonoBehaviour
{
    private string nextNodeGuid;

    public void AssignNextNodeGuid(string guid)
    {
        nextNodeGuid = guid;
    }

    public void Prompt_DM_ForNextOption()
    {
        FindFirstObjectByType<DialogueManager>().UserPickNextNodeGuid(nextNodeGuid);
    }
}
