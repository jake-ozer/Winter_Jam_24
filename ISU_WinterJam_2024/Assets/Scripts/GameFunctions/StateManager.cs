using UnityEngine;

public class StateManager : MonoBehaviour
{
    private GameObject player;
    private GameState curState;
    public GameObject dialogueCanvas;

    public enum GameState
    {
        roam,
        dialogue
    }

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
        curState = GameState.roam;
    }

    void Update()
    {
        switch (curState)
        {
            case GameState.roam:
                player.GetComponent<PlayerMovement>().enabled = true;
                dialogueCanvas.SetActive(false);
                break;
            case GameState.dialogue:
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                dialogueCanvas.SetActive(true);
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
