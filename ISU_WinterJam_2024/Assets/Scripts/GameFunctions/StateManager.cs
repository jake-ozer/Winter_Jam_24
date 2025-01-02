using UnityEngine;

public class StateManager : MonoBehaviour
{
    private GameObject player;
    private GameState curState;
    public GameObject dialogueCanvas;
    public GameObject inventoryCanvas;
    private PlayerAnim playerAnim;

    public enum GameState
    {
        roam,
        dialogue,
        inventory_menu
    }

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
        playerAnim = FindFirstObjectByType<PlayerAnim>();
        curState = GameState.roam;
    }

    void Update()
    {
        switch (curState)
        {
            case GameState.roam:
                player.GetComponent<PlayerMovement>().enabled = true;
                dialogueCanvas.SetActive(false);
                inventoryCanvas.SetActive(false);
                FindFirstObjectByType<InventoryManager>().ClearDescription();
                playerAnim.enabled = true;
                break;
            case GameState.dialogue:
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                dialogueCanvas.SetActive(true);
                playerAnim.anim.SetBool("moving", false);
                playerAnim.enabled = false;
                break;
            case GameState.inventory_menu:
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                inventoryCanvas.SetActive(true);
                playerAnim.anim.SetBool("moving", false);
                playerAnim.enabled = false;
                break;
        }
    }

    public void ChangeState(GameState state)
    {
        curState = state;
    }

    public GameState GetCurState()
    {
        return curState;
    }
}
