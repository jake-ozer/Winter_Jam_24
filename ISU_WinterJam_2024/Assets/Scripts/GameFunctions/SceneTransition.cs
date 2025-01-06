using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    public Animator anim;
    private bool canTrans = true;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeScene(string levelName)
    {
        if (canTrans)
        {
            canTrans = false;
            StartCoroutine(FadeTrans(levelName));
        }
    }

    private IEnumerator FadeTrans(string levelName)
    {
        //fade in
        anim.SetTrigger("FadeOut");
        //load scene

        yield return new WaitUntil(() => anim.gameObject.GetComponent<Image>().color.a == 1);
        SceneManager.LoadScene(levelName);
        //fade out
        anim.SetTrigger("FadeIn");
        canTrans = true;
    }
}
