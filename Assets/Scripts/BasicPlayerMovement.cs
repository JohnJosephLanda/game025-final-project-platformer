using NUnit.Framework;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    public LayerMask whatIsGround;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public float jumpSpeed;
    private Rigidbody2D myRigidBody;
    public float moveSpeed = 0f;
    private bool doubleJump = false;
    public bool doubleJumpUnlocked = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myRigidBody.linearVelocity = new Vector2(moveSpeed, myRigidBody.linearVelocity.y);  
            transform.localScale = new Vector2(1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myRigidBody.linearVelocity = new Vector2(-moveSpeed, myRigidBody.linearVelocity.y);
            transform.localScale = new Vector2(-1f, 1f);

        }
        else
        {
            myRigidBody.linearVelocity = new Vector2(0f, myRigidBody.linearVelocity.y);
        }
    }

    void Update()
    {
        if (isGrounded)
            doubleJump = true;

        if (Input.GetButtonDown("Jump") && (isGrounded || (doubleJump && doubleJumpUnlocked)))
        {
            if (!isGrounded)
                doubleJump = false;
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, jumpSpeed);
        }
    }
}
