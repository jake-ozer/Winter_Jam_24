using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int allyCount;
    public TextMeshProUGUI allyText;
    private bool once = true;

    private void Update()
    {
        allyText.text = "Allies: " + allyCount.ToString() + "/5";

        if(allyCount >= 5 && once)
        {
            FindFirstObjectByType<SceneTransition>().ChangeScene("EndingSlide");
            once = false;
        }
    }

}
