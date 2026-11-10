using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class BasicPlayerMovement : MonoBehaviour
{
    public LayerMask whatIsGround;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public float jumpSpeed;
    private Rigidbody2D myRigidBody;
    public float moveSpeed = 0f;
    public float dashSpeed;
    private bool doubleJump = false;
    public bool doubleJumpUnlocked = true;
    public bool dashUnlocked = true;
    private int state = 0;
    public float playerScale = 0.6f;

    private Animator myAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.linearVelocity.x));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (state == 0)
        {
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidBody.linearVelocity = new Vector2(moveSpeed, myRigidBody.linearVelocity.y);  
                transform.localScale = new Vector2(playerScale, playerScale);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidBody.linearVelocity = new Vector2(-moveSpeed, myRigidBody.linearVelocity.y);
                transform.localScale = new Vector2(-playerScale, playerScale);

            }
            else
            {
                myRigidBody.linearVelocity = new Vector2(0f, myRigidBody.linearVelocity.y);
            }
        }
    }

    void Update()
    {
        if (state == 0 || state == 1)
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

        if (state == 0)
        {
            if (Input.GetButtonDown("Dash") && dashUnlocked)
            {
                state = 1;
                StartCoroutine(Dash());
            }
        }
    }

    private IEnumerator Dash()
    {
        myRigidBody.linearVelocity = new Vector2((dashSpeed * transform.localScale.x), myRigidBody.linearVelocity.y);  
        yield return new WaitForSeconds(0.6f);
        state = 0;
    }
}
