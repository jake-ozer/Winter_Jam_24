using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;

    private float lastHorizMove = 0f;
    private float lastVertMove = 0f; 

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (dir.x != 0 || dir.y != 0)
        {
            lastHorizMove = dir.x;
            lastVertMove = dir.y;
        }

        anim.SetFloat("HorizMove", lastHorizMove);
        anim.SetFloat("VertMove", lastVertMove);
    }
}
