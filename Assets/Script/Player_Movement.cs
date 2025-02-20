using UnityEngine;

// Add this for input!
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 1f;
    [SerializeField] float jumpheight = 3f;
    float direction = 0;
    bool isGrounded = false;
    int jumplimit = 2;

    bool isFacingRight = true;

    Animator anim;

    // Start is called before the first frame update
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);

        if((isFacingRight && direction == -1) || (!isFacingRight && direction == 1))
        {
            flip();
        }
    }

    void OnMove (InputValue value) // Exact Spelling needed!
    {
        float v = value.Get<float>();
        direction = v;
    }

    void Move (float dir)
    {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        anim.SetBool("Is_Running", dir != 0);
    }

    void OnJump ()
    {
        if (isGrounded)
            jumplimit = 2;
        if (jumplimit > 0)
            Jump();
            jumplimit --;
   
    }
    void Jump ()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpheight);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {


    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (Vector2.Angle(collision.GetContact(i).normal, Vector2.up) < 45f)
                {
                    isGrounded = true;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

    }

    private void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1f;
        transform.localScale = newLocalScale;

    }

}
