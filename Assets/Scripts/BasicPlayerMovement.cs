using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SceneManagement;

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
    public float playerScale = 0.4f;
    public TextMeshProUGUI sanityText;

    private int sanity = 100;
    private float accerate = 0.5f;

    private Animator myAnim;
    private SpriteRenderer mySprite;

    private float knockbackDuration;
    private float knockbackAmount;
    public float iFrames = 0;

    public Image deathScreen;
    private Collider2D myCollider;
    private bool death;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
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
                if (myRigidBody.linearVelocityX < moveSpeed)
                    myRigidBody.linearVelocity = new Vector2((Mathf.Abs(myRigidBody.linearVelocityX) + accerate), myRigidBody.linearVelocity.y);  
                else 
                    myRigidBody.linearVelocity = new Vector2(moveSpeed, myRigidBody.linearVelocity.y);
                transform.localScale = new Vector2(playerScale, playerScale);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                if (myRigidBody.linearVelocityX > -moveSpeed)
                    myRigidBody.linearVelocity = new Vector2((-Mathf.Abs(myRigidBody.linearVelocityX) - accerate), myRigidBody.linearVelocity.y);  
                else 
                    myRigidBody.linearVelocity = new Vector2(-moveSpeed, myRigidBody.linearVelocity.y);
                transform.localScale = new Vector2(-playerScale, playerScale);

            }
            else if (state != 3)
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

        if (iFrames > 0)
            iFrames -= Time.deltaTime;
        sanityText.text = "Sanity: " + sanity;
    }

    private IEnumerator Dash()
    {
        myRigidBody.linearVelocity = new Vector2((dashSpeed * transform.localScale.x), myRigidBody.linearVelocity.y);  
        yield return new WaitForSeconds(0.6f);
        state = 0;
    }
    
    public void TakeDamage(int damage)
    {
        sanity -= damage;
        iFrames = 1f;
        if (sanity <= 0)
            Death();
    }

    public void Death()
    {
        death = true;
        state = 3;
        StartCoroutine(Dieing());
        myCollider.enabled = false;
        myRigidBody.gravityScale = 0;
        transform.localScale = new Vector2(-playerScale, playerScale);
        myRigidBody.linearVelocity = new Vector2(-40, 2);
    }






    public bool ItemUnlock(ItemEnum item)
    {
        if (item == ItemEnum.doubleJump)
        {
            return EnableDoubleJump();
        }
        if (item == ItemEnum.dash)
        {
            return EnableDash();
        }
        
        return false;
    }




    private bool EnableDoubleJump()
    {
        doubleJumpUnlocked = true;
        return doubleJumpUnlocked;
    }
    
    private bool EnableDash()
    {
        dashUnlocked = true;
        return dashUnlocked;
    }



    private bool DisableDoubleJump()
    {
        doubleJumpUnlocked = false;
        return !doubleJumpUnlocked; // should be true if code works
    }
    
    private bool DisableDash()
    {
        dashUnlocked = false;
        return !dashUnlocked; // should be true if code works
    }

    public void startKnockback(float duration, float amount, int where)
    {
        knockbackDuration = duration;
        knockbackAmount = amount;
        state = 2;
        mySprite.color = new Color(0.9f, 0.5f, 0.5f, 1);
        StartCoroutine(knockBack(where));
    }

    private IEnumerator knockBack(int direction)
    {
        if (!isGrounded)
            knockbackAmount *= 2;
        myRigidBody.linearVelocity = new Vector2((direction * knockbackAmount), 3);  
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(knockbackDuration * 0.25f);
            myRigidBody.linearVelocity = new Vector2((myRigidBody.linearVelocity.x - (knockbackAmount * 0.125f * direction) ), myRigidBody.linearVelocity.y);  
        }
        if (!death)
            state = 0;
        mySprite.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator Dieing()
    {
        for (int i = 0; i < 16; i++)
        {
            deathScreen.color = new Color(0, 0, 0, (i / 10.0f));
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}