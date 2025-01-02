using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject gfxObj;
    private GameObject player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            //flip sprite to correct side
            gfxObj.transform.rotation = player.transform.position.x < gfxObj.transform.position.x ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            GetComponent<NPC_Dialogue>().TriggerDialogue();
        }
    }
}
