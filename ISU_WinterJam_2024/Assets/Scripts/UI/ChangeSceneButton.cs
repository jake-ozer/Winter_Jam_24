using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        FindFirstObjectByType<SceneTransition>().ChangeScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
