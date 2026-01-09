using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rb;
    public SpriteRenderer sr;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    {
        
        float x = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector3(x * speed, rb.linearVelocity.y, 0f);

        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }
    }
}
