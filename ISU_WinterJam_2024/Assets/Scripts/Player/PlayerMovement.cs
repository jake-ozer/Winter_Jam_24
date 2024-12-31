using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float xdir = Input.GetAxisRaw("Horizontal");
        float ydir = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(xdir, ydir);
        rb.linearVelocity = movement * speed * Time.deltaTime;
        
    }
}
