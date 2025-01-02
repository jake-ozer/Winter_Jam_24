using UnityEngine;

public class MenuController : MonoBehaviour
{
    private bool menuUp = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuUp)
        {
            FindFirstObjectByType<StateManager>().ChangeState(StateManager.GameState.inventory_menu);
            menuUp = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuUp)
        {
            FindFirstObjectByType<StateManager>().ChangeState(StateManager.GameState.roam);
            menuUp = false;
        }
    }
}
