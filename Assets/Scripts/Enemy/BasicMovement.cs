using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    private bool groundAhead;
    private bool wallAhead;
    public Transform wallCheck;
    public float wallCheckRaduis; 
    private int direction = 1;
    private Rigidbody2D myRigidBody;
    public float moveSpeed;
    public float myScale = 1;
    [SerializeField] private bool isInMainMenu = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isInMainMenu) // cuts out movement when in main menu
        {
            groundAhead = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            wallAhead = Physics2D.OverlapCircle(wallCheck.position, wallCheckRaduis, whatIsGround);
            if (!groundAhead || wallAhead)
                direction *= -1;
            myRigidBody.linearVelocity = new Vector2((direction * moveSpeed), myRigidBody.linearVelocity.y);
            transform.localScale = new Vector2((myScale * direction), myScale);
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
