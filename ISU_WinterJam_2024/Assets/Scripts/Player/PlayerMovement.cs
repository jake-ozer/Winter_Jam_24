using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float xdir = Input.GetAxisRaw("Horizontal");
        float ydir = Input.GetAxisRaw("Vertical");
        movement = new Vector2(xdir, ydir);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * speed * Time.deltaTime;
    }
}
