using UnityEngine;

public class NPC_Anim : MonoBehaviour
{
    public Sprite idle_1;
    public Sprite idle_2;
    public float cycleTime;
    private float timer;
    private bool onIdle1;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = idle_1;
        onIdle1 = true;
        timer = cycleTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = onIdle1 ? idle_2 : idle_1;
            onIdle1 = !onIdle1;
            timer = cycleTime;
        }
    }
}
